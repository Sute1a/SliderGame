using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostEffector : MonoBehaviour
{
    private float duration = 0.3f;

    private bool isApplyEffected;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" && isApplyEffected == false)
        {
            isApplyEffected = true;

            Camera.main.gameObject.GetComponent<FrostEffectController>().
                UpdateFrostAmount(duration);

            Destroy(gameObject, 0.5f);
        }   
    }
    
}
