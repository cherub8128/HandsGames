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
            //쌀
            if (Game01_BarleyRiceEvents1.timing == 2)
            {
                // 연속터치 방지
                if (Time.time - lastClickTime < 1) return;
                lastClickTime = Time.time;

                // 캐치 소리 플레이
                SoundManager.Instance.playSFX(5);

                // 쌀 패널티 무효화
                Game01_BarleyRiceEvents1.isCatch = true;

                // 득점 처리
                Status.Instance.setScore(1);

                // 캐치 애니메이션 동작
                GrabHand.SetBool("isScoreUp", true);
                temp = RockHand.speed;
                RockHand.speed = 0;
                Effector.SetTrigger("ScoreUp");
                var effect = Instantiate(HighLight);
                effect.transform.localScale = new Vector3(0.2f, 0.26f, 1f);
                effect.transform.position = new Vector3(0f, -0.59f);

                // 콤보 처리
                Status.Instance.setCombo(isCombo);
                isCombo = true;
                TextAnimation.SetTrigger("Start");

            }

            // 하트
            else if(Game01_BarleyRiceEvents1.timing == 3)
            {
                // 하트 회복 애니메이션
                Effector.SetTrigger("Cure");

                // 라이프 회복
                Status.Instance.dealingLife(4);
            }

            // 발
            else if(Game01_BarleyRiceEvents1.timing == 4)
            {
                // 득점 패널티
                Status.Instance.setScore(-1);

                // 캐치 애니메이션 동작
                GrabHand.SetBool("isScoreUp", true);
                temp = RockHand.speed;
                RockHand.speed = 0;
                Effector.SetTrigger("ScoreUp");
                var effect = Instantiate(HighLight);
                effect.transform.localScale = new Vector3(0.2f, 0.26f, 1f);
                effect.transform.position = new Vector3(0f, -0.59f);
                // 콤보 처리
                isCombo = false;
            }

            // 보리 패널티
            else
            {
                // 라이프 차감
                Status.Instance.dealingLife(-4);
                
                // 콤보 처리
                isCombo = false;
            }

        }
    }
}
