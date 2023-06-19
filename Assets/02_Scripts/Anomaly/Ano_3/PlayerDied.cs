using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDied : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.Died(GameManager.CauseOfDeath.violationRule, "��Ģ 3: â�� �ȿ� ������");
        }
    }
}
