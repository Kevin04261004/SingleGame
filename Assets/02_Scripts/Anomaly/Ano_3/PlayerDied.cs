using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDied : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.Died(GameManager.CauseOfDeath.violationRule, "규칙 3: 창고 안엔 무엇이");
        }
    }
}
