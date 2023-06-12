using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndingCollider : MonoBehaviour
{
    public ClearSceneUIManager clearScene;
    private void OnTriggerStay(Collider other)
    {
        print(1);
        if(other.gameObject.CompareTag("EndingCreditCollider"))
        {
            clearScene.StopCoroutine(clearScene.UpdateCoroutine);
        }
    }
}
