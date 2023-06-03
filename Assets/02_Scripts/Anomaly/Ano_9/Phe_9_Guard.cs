using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_9_Guard : Phenomenon
{
    [SerializeField] float needStayTime = 10f;
    [SerializeField, ReadOnly] float counter_nst = -1;
    [SerializeField] Area_DetectPlayer prefab_detectZone;
    Area_DetectPlayer detectZone;

    protected override void PhenomenonEnd()
    {
        detectZone.event_time_playerInArea -= CheckStayTime;
        Destroy(detectZone.gameObject);
    }

    protected override void PhenomenonStart()
    {
        detectZone = Instantiate(prefab_detectZone);
        detectZone.event_time_playerInArea += CheckStayTime;
        counter_nst = needStayTime;
    }

    protected override void Solution()
    {
        // Ano9의 경우 현상이 Guard하나라 상관 없지만
        // 현상이 추가될 경우 event를 빨리 빼거나 bool isAlreadyDone을 두어야 함
        if (counter_nst <= float.Epsilon)
        {
            TryFixThisPhenomenon();
        }
    }

    void CheckStayTime(float deltaTime)
    {
        counter_nst -= deltaTime;
        Solution();
    }
}
