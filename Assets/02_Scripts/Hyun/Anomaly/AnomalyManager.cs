using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : Singleton<AnomalyManager>
{
    /// <summary>
    /// ���� �߻��ǰ� �ִ� �̻����� ����Ʈ
    /// </summary>
    public List<Anomaly> effectiveAnomalys = new List<Anomaly>();

    public void AddAnomaly(int index = -1)
    {

    }
}
