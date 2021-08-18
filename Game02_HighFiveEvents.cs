using UnityEngine;

public class Game02_HighFiveEvents : MonoBehaviour
{
    [SerializeField] Animator HighfiveMyHandAnim;
    [SerializeField] Animator HighfiveOpHandAnim;

    [SerializeField] Animator ComboTextAnim;
    [SerializeField] Collider2D HighfiveOpHandCollider;
    [SerializeField] Camera MainCamera;
    
    private TouchEvents HandTouchEvents;
    private bool isTouchable = false;
    private bool isCombo = false;
    private bool passed = false;
    private float speed;
    private int opStatus;

    public void newRandomOpHand()
    {
        if ((opStatus==0 ||opStatus==2) && passed) isCombo = false;
        SoundManager.Instance.playSFX(2);
        opStatus = setRandomInt();
        HighfiveOpHandAnim.SetInteger("Random", opStatus);
        if (opStatus==0 || opStatus==2) passed = true;
    }
    public void setHandSpeed()
    {
        
        float level = Mathf.Min(1f,Status.Instance.Score/100f);
        speed = Random.Range(level/2, level);
        HighfiveOpHandAnim.SetFloat("Speed", speed);
        HighfiveMyHandAnim.SetFloat("Speed", speed);
        SoundManager.Instance.modifyBGM(speed+0.64f);
    }

    public void setTouchableTrue()
    {
        isTouchable = true;
    }
    public void setTouchableFalse()
    {
        isTouchable = false;
    }

    private void Start()
    {
        TouchEvents.TouchUp += myHandTouchUp;
        TouchEvents.TouchSwipeDown += myHandTouchSwipeDown;
        TouchEvents.TouchSwipeUp += myHandTouchSwipeUp;
        TouchEvents.TouchDown += myHandTouchDown;
        for(int i=0;i<3;i++) SoundManager.Instance.insertSFX(i,i);
    }

    private void Update()
    {
    }
    private void OnDestroy()
    {
        TouchEvents.TouchUp -= myHandTouchUp;
        TouchEvents.TouchSwipeDown -= myHandTouchSwipeDown;
        TouchEvents.TouchSwipeUp -= myHandTouchSwipeUp;
        TouchEvents.TouchDown -= myHandTouchDown;
    }
    private int setRandomInt()
    {
        float ran = Random.Range(0f, 1f);
        if (ran<=0.4f) return 0;
        else if (0.4f<ran&&ran<0.6f) return 1;
        else return 2;
    }

    private void rockHand()
    {

        if(opStatus==2)
        {
            SoundManager.Instance.playSFX(2);
            HighfiveMyHandAnim.SetTrigger("Rock");
            Status.Instance.setScore(1);

            passed = false;
            Status.Instance.setCombo(isCombo);
            isCombo = true;
            ComboTextAnim.SetTrigger("Start");
        } 
        else 
        {
            SoundManager.Instance.playSFX(0);
            HighfiveMyHandAnim.SetTrigger("Rock0");
            Status.Instance.dealingLife(-1);
            isCombo = false;
        }
    }

    private void myHandTouchDown(object sender, PositionEventArgs e)
    {
    }

    private void myHandTouchSwipeDown(object sender, PositionEventArgs e)
    {
        var pos = MainCamera.ScreenToWorldPoint(e.BeganPosition);
        if (HighfiveOpHandCollider.OverlapPoint(pos)) rockHand();
    }
    private void myHandTouchSwipeUp(object sender, PositionEventArgs e)
    {
        var pos = MainCamera.ScreenToWorldPoint(e.BeganPosition);
        if (HighfiveOpHandCollider.OverlapPoint(pos)) rockHand();
    }
    private void myHandTouchUp(object sender, PositionEventArgs e)
    {
        if (isTouchable) isTouchable = false;
        else return;
        var pos = MainCamera.ScreenToWorldPoint(e.EndedPosition);
        HighfiveMyHandAnim.SetTrigger("Touch");
        if (HighfiveOpHandCollider.OverlapPoint(pos))
        {
            if(opStatus==0)
            {
                SoundManager.Instance.playSFX(1);
                HighfiveMyHandAnim.SetTrigger("Shot");
                Status.Instance.setScore(1);
                passed = false;
                Status.Instance.setCombo(isCombo);
                isCombo = true;
                ComboTextAnim.SetTrigger("Start");
            }
            else if(opStatus>0)
            {
                SoundManager.Instance.playSFX(0);
                HighfiveMyHandAnim.SetTrigger("Oops");
                Status.Instance.dealingLife(-1);
                isCombo = false;
            }
        }
        
    }
    
}
