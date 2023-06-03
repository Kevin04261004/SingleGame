using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ �ش� Collider�� �ӹ��� �ð��� üũ�ϱ� ���� ��ü
/// </summary>
public class Area_DetectPlayer : MonoBehaviour
{
    public delegate void PlayerInArea(float time);
    public event PlayerInArea event_time_playerInArea;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            event_time_playerInArea?.Invoke(Time.deltaTime);
        }
    }
}
