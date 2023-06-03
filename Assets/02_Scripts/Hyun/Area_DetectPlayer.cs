using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 해당 Collider에 머무는 시간을 체크하기 위한 객체
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
