using UnityEngine;
using UnityEngine.UI;

public class Game01_BarleyRiceEvents1 : MonoBehaviour
{
    [SerializeField] SpriteRenderer RockHand;
    [SerializeField] Sprite Foot;
    [SerializeField] Sprite HeartHand;
    [SerializeField] Sprite Rock;
    [SerializeField] Text TimingText;
    [SerializeField] Animator TextAnimation;
    public static int timing;
    public static bool isCatch = true;

    private string[] texts = {"보리","보리!","쌀!", "하트", "발"};

    private void Start()
    {
        for(int i=0;i<texts.Length+1;i++) SoundManager.Instance.insertSFX(i,i);
    }

    public void setTimingAndSpeed()
    {
        isCatch = true;
        int now = Status.Instance.Score +1;
        int take = Random.Range(0,now);
        float speed = take/100f;
        if (now<3) timing = Random.Range(2,6)%3;
        else {
            RockHand.sprite = Rock;
            float temp = (float)take/now;
            if (temp > 0.9f)
            {
                RockHand.sprite = Foot;
                timing = 4;
            }
            else if (temp > 0.84f)
            {
                timing = 3;
                RockHand.sprite = HeartHand;
            }
            else if (temp > 0.5f)
            {
                timing = 2;
                isCatch = false;
            }
            else if (temp > 0.4f) timing = 1;
            else timing = 0;
        }
        GetComponent<Animator>().SetFloat("Speed",speed);
        TextAnimation.SetFloat("Speed",speed);
        SoundManager.Instance.modifyBGM(speed+0.64f);
    }

    public void showText()
    {
        SoundManager.Instance.playSFX(timing);
        TextAnimation.SetTrigger("Start");
        TimingText.text = texts[timing];
    }

    public void hideText()
    {
        if (!isCatch)
        {
            Game01_BarleyRiceEvents2.isCombo = false;
            Status.Instance.dealingLife(-1);
            isCatch = true;
        }
        TextAnimation.SetTrigger("End");
    }

}
