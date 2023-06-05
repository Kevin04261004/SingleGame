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
    [ReadOnly] public List<Anomaly> effectiveAnomalys = new List<Anomaly>();
    public bool everyAnoIsExecuting => (effectiveAnomalys.Count == anomalyList.Length);
    public bool ExecuteAnomaly(int index = -1)
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
        if (!CheckDuplicateFromEffectiveList(anomaly) && anomaly.CheckExecuteCondition())
        {
            anomaly.Init();
            effectiveAnomalys.Add(anomaly);
            return true;
        }
        else
        {
            Destroy(anomaly.gameObject);
            return false;
        }
    }
    public void RemoveAnomalyFromEffectiveList(Anomaly instance)
    {
        if (instance == null)
        {
            Debug.LogWarning("instance는 null일 수 없습니다. 이미 제거된 Anomaly일 수 있습니다.");
            return;
        }
        for (int i = 0; i < effectiveAnomalys.Count; i++)
        {
            if (instance == effectiveAnomalys[i])
            {
                effectiveAnomalys.RemoveAt(i);
                return;
            }
        }
        Debug.LogError($"{instance}는 실행중인 Anomaly목록에 존재하지 않습니다.\nAnomaly의 생성 및 처리는 AnomalyManager가 진행해야 합니다.");
    }
    /// <summary>
    /// Anomaly 이름을 활용해 이미 Scene에서 실행되고 있는 Anomaly인지 확인
    /// </summary>
    /// <param name="anomaly"></param>
    /// <returns>true: 해당 이름의 Anomaly가 이미 실행중임<br/>
    /// false: 해당 이름의 Anomaly는 실행되고 있지 않음</returns>
    bool CheckDuplicateFromEffectiveList(Anomaly anomaly)
    {
        for (int i = 0; i < effectiveAnomalys.Count; i++)
        {
            if (anomaly.anomalyName == effectiveAnomalys[i].anomalyName)
            {
                return true;
            }
        }
        return false;
    }
}
