using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : Singleton<AnomalyManager>
{
    /// <summary>
    /// 현재 발생되고 있는 이상현상 리스트
    /// </summary>
    public List<Anomaly> effectiveAnomalys = new List<Anomaly>();

    public void AddAnomaly(int index = -1)
    {

    }
}
