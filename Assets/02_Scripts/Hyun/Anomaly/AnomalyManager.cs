using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : Singleton<AnomalyManager>
{
    [Tooltip("���ӿ� �����ϴ� �̻����� ���(Prefab)")]
    public Anomaly[] anomalyList;
    /// <summary>
    /// ���� �߻��ǰ� �ִ� �̻����� ����Ʈ
    /// </summary>
    public List<Anomaly> effectiveAnomalys = new List<Anomaly>();

    public void ExecuteAnomaly(int index = -1)
    {

    }
}
