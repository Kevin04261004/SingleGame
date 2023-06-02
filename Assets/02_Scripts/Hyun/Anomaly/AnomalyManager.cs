using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : Singleton<AnomalyManager>
{
    [Tooltip("게임에 등장하는 이상현상 목록(Prefab)")]
    public Anomaly[] anomalyList;
    /// <summary>
    /// 현재 발생되고 있는 이상현상 리스트<br/>
    /// 
    /// TODO<br/>
    /// Anomaly가 해결될 때 List에서 제거하는 작업 필요.
    /// 또는 굳이 발생하고있는 Anomaly목록을 볼 필요가 없다면 제거해도 무방
    /// </summary>
    [ReadOnly] public List<Anomaly> effectiveAnomalys = new List<Anomaly>();
    /// <summary>
    /// TODO<br/>
    /// 이미 발생하고 있는 Anomaly인지 확인하고 중복실행되지 않게 하는 작업 필요.
    /// List int등을 활용할 수 있음
    /// </summary>
    /// <param name="index"></param>
    [ContextMenu("ExecuteRandomAnomalyFromList")]
    public void ExecuteAnomaly(int index = -1)
    {
        Anomaly anomaly = null;
        if (index == -1)
        {
            int randIdx = Random.Range(0, anomalyList.Length);
            anomaly = Instantiate(anomalyList[randIdx]);
        }
        else
        {
            anomaly = Instantiate(anomalyList[index]);
        }
        effectiveAnomalys.Add(anomaly);
    }
    private void Start()
    {
        ExecuteAnomaly();
    }
}
