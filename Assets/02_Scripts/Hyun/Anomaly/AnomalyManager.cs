using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : Singleton<AnomalyManager>
{
    [Tooltip("게임에 등장하는 이상현상 목록(Prefab)")]
    public Anomaly[] anomalyList;
    /// <summary>
    /// 현재 발생되고 있는 이상현상 리스트
    /// </summary>
    public List<Anomaly> effectiveAnomalys = new List<Anomaly>();

    public void ExecuteAnomaly(int index = -1)
    {

    }
}
