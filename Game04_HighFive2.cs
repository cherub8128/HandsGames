using UnityEngine;

public class Game04_HighFive2 : MonoBehaviour
{
    [SerializeField]public Animator Me;
    [SerializeField]public Animator Op;
    [SerializeField]public Animator Effect;
    private int picked = -1;
    private float speed = 1f;
    private string[] triggerlist = {"paper","paper_down","rock_up","rock_down","rock","cross","indexfinger"};
    private void Start()
    {
        Time.timeScale = 0;
        TouchEvents.setIsStateIn(true);
        TouchEvents.TouchUp += myHandTouchUp;
        TouchEvents.TouchSwipeDown += myHandTouchSwipeDown;
        TouchEvents.TouchSwipeUp += myHandTouchSwipeUp;
        TouchEvents.TouchSwipeLeft += myHandTouchSwipeLeft;
        TouchEvents.TouchSwipeRight += myHandTouchSwipeRight;
        TouchEvents.TouchSwipeDown2 += myHandTouchSwipeDown2;
        TouchEvents.TouchSwipeUp2 += myHandTouchSwipeUp2;
        SoundManager.Instance.playBGM();
        SoundManager.Instance.insertSFX(0,0);
        SoundManager.Instance.insertSFX(1,1);
    }
    private void OnDestroy() {
        TouchEvents.TouchUp -= myHandTouchUp;
        TouchEvents.TouchSwipeDown -= myHandTouchSwipeDown;
        TouchEvents.TouchSwipeUp -= myHandTouchSwipeUp;
        TouchEvents.TouchSwipeLeft -= myHandTouchSwipeLeft;
        TouchEvents.TouchSwipeRight -= myHandTouchSwipeRight;
        TouchEvents.TouchSwipeDown2 -= myHandTouchSwipeDown2;
        TouchEvents.TouchSwipeUp2 -= myHandTouchSwipeUp2;
    }
    private void myHandTouchUp(object sender, PositionEventArgs e)
    {
        if (Time.timeScale == 0) return;
        Me.SetTrigger(triggerlist[5]);
        scoring(5);
    }
    private void myHandTouchSwipeDown(object sender, PositionEventArgs e)
    {
        if (Time.timeScale == 0) return;
        Me.SetTrigger(triggerlist[3]);
        scoring(2);
    }
    private void myHandTouchSwipeUp(object sender, PositionEventArgs e)
    {
        if (Time.timeScale == 0) return;
        Me.SetTrigger(triggerlist[2]);
        scoring(3);
    }
    private void myHandTouchSwipeLeft(object sender, PositionEventArgs e)
    {
        if (Time.timeScale == 0) return;
        Me.SetTrigger(triggerlist[6]);
        scoring(6);
    }
    private void myHandTouchSwipeRight(object sender, PositionEventArgs e)
    {
        if (Time.timeScale == 0) return;
        Me.SetTrigger(triggerlist[4]);
        scoring(4);
    }
    private void myHandTouchSwipeUp2(object sender, PositionEventArgs e)
    {
        if (Time.timeScale == 0) return;
        Me.SetTrigger(triggerlist[0]);
        scoring(1);
    }
    private void myHandTouchSwipeDown2(object sender, PositionEventArgs e)
    {
        if (Time.timeScale == 0) return;
        Me.SetTrigger(triggerlist[1]);
        scoring(0);
    }
    private void scoring(int i)
    {
        if (picked%7==i)
        {
            if (i!=6) Effect.SetTrigger("correct");
            if (i==0 || i==1) SoundManager.Instance.playSFX(0);
            else SoundManager.Instance.playSFX(1);
            speed += 0.01f;
            Status.Instance.setScore(1);
            Status.Instance.setCombo(true);
            Effect.SetFloat("speed",speed+0.5f);
            Me.SetFloat("speed",speed+0.5f);
            Op.SetFloat("speed",speed);
            SoundManager.Instance.modifyBGM(speed);
        }
        else
        {
            Status.Instance.dealingLife(-1);
            Status.Instance.setCombo(false);
        }
    }
    private void setRandomOpAnim()
    {
        picked = picked+1;
        if (picked>120) picked = Random.Range(0,7);
        Op.SetTrigger(triggerlist[picked%7]);
    }
}
