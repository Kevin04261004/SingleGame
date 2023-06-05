using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_14 : Phenomenon
{
    [SerializeField] float needStayTime = 1f;
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
