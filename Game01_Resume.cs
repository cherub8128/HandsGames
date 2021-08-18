using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game01_Resume : MonoBehaviour
{
    [SerializeField] Animator grabHand;
    [SerializeField] Animator rockHand;

    void resume()
    {  
        Destroy(GameObject.FindGameObjectWithTag("Effect"));
        rockHand.speed = Game01_BarleyRiceEvents2.temp;
        grabHand.SetBool("isScoreUp", false);
    }
}
