using UnityEngine;

//TODO 소리 넣기
public class Game03_PenStab : MonoBehaviour
{
    [SerializeField] GameObject Pen;
    [SerializeField] Animator ComboTextAnim;
    [SerializeField] Collider2D[] StabPos;
    private Vector2 PenPos;
    private Animator PenAnim;
    private float speed = 1;
    private int[] collectstabs = { 0, 1, 0, 2, 0, 3, 0, 4, 0, 5, 0, 4, 0, 3, 0, 2, 0, 1};
    private int order, comp = 0;
    private bool isCollect = false;
    public void SetOrder()
    {
        order = (order+1)%18;
        isCollect = false;
    }

    private void Start()
    {
        PenAnim = Pen.GetComponent<Animator>();
        for(int i=0;i<2;i++) SoundManager.Instance.insertSFX(i,i);
        TouchEvents.TouchDown += stabPen;
    }
    private void Update()
    {
    }
    private void OnDestroy()
    {
        TouchEvents.TouchDown -= stabPen;
    }
    private void stabPen(object sender, PositionEventArgs e)
    {
        PenPos = Pen.transform.position;
        SoundManager.Instance.playSFX(0);
        int stabNum = stabOverlap(PenPos);
        PenAnim.SetTrigger("Stab");
        if (stabNum==0 && order==0)
        {
            isCollect = true;
            comp = 1;
            PenAnim.SetTrigger("Start");
        }
        else if (stabNum == -1)
        {
            SoundManager.Instance.playSFX(1);
            isCollect = false;
            Status.Instance.dealingLife(-1);
        }
        else if (stabNum == collectstabs[order])
        {
            isCollect = true;
            speed += 0.01f;
            comp = (comp+1)%9;
            PenAnim.SetFloat("Speed", speed);
            SoundManager.Instance.modifyBGM(speed);
            if (comp==0) Status.Instance.setScore(1);
        }
        else 
        {
            isCollect = false;
        }
        Status.Instance.setCombo(isCollect);
        ComboTextAnim.SetTrigger("Start");
    }
    private int stabOverlap(Vector2 penPos)
    {
        int r = -1;
        for (int i = 0; i < 6; i++)
        {
            if (StabPos[i].OverlapPoint(PenPos)) r = i;
        }
        return r;
    }
}
