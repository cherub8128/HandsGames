using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game01_BarleyRiceEvents2 : MonoBehaviour
{
    [SerializeField] Animator GrabHand;
    [SerializeField] Animator RockHand;
    [SerializeField] Animator Effector;
    [SerializeField] Animator TextAnimation;
    [SerializeField] GameObject HighLight;

    Collider2D[] Colliders = new Collider2D[1];
    ContactFilter2D ContactFilter = new ContactFilter2D();

    static public bool isCombo = false;
    static public float temp;
    private float lastClickTime = -999;
    

    void OnMouseDown()
    {
        if (GrabHand.GetCurrentAnimatorStateInfo(0).IsName("Grap")) return;
        GrabHand.SetTrigger("click");
        if (GetComponent<Collider2D>().OverlapCollider(ContactFilter,Colliders)>0 )
        {
            //��
            if (Game01_BarleyRiceEvents1.timing == 2)
            {
                // ������ġ ����
                if (Time.time - lastClickTime < 1) return;
                lastClickTime = Time.time;

                // ĳġ �Ҹ� �÷���
                SoundManager.Instance.playSFX(5);

                // �� �г�Ƽ ��ȿȭ
                Game01_BarleyRiceEvents1.isCatch = true;

                // ���� ó��
                Status.Instance.setScore(1);

                // ĳġ �ִϸ��̼� ����
                GrabHand.SetBool("isScoreUp", true);
                temp = RockHand.speed;
                RockHand.speed = 0;
                Effector.SetTrigger("ScoreUp");
                var effect = Instantiate(HighLight);
                effect.transform.localScale = new Vector3(0.2f, 0.26f, 1f);
                effect.transform.position = new Vector3(0f, -0.59f);

                // �޺� ó��
                Status.Instance.setCombo(isCombo);
                isCombo = true;
                TextAnimation.SetTrigger("Start");

            }

            // ��Ʈ
            else if(Game01_BarleyRiceEvents1.timing == 3)
            {
                // ��Ʈ ȸ�� �ִϸ��̼�
                Effector.SetTrigger("Cure");

                // ������ ȸ��
                Status.Instance.dealingLife(4);
            }

            // ��
            else if(Game01_BarleyRiceEvents1.timing == 4)
            {
                // ���� �г�Ƽ
                Status.Instance.setScore(-1);

                // ĳġ �ִϸ��̼� ����
                GrabHand.SetBool("isScoreUp", true);
                temp = RockHand.speed;
                RockHand.speed = 0;
                Effector.SetTrigger("ScoreUp");
                var effect = Instantiate(HighLight);
                effect.transform.localScale = new Vector3(0.2f, 0.26f, 1f);
                effect.transform.position = new Vector3(0f, -0.59f);
                // �޺� ó��
                isCombo = false;
            }

            // ���� �г�Ƽ
            else
            {
                // ������ ����
                Status.Instance.dealingLife(-4);
                
                // �޺� ó��
                isCombo = false;
            }

        }
    }
}
