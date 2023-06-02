using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyManager : Singleton<AnomalyManager>
{
    [Tooltip("���ӿ� �����ϴ� �̻����� ���(Prefab)")]
    public Anomaly[] anomalyList;
    /// <summary>
    /// ���� �߻��ǰ� �ִ� �̻����� ����Ʈ<br/>
    /// 
    /// TODO<br/>
    /// Anomaly�� �ذ�� �� List���� �����ϴ� �۾� �ʿ�.
    /// �Ǵ� ���� �߻��ϰ��ִ� Anomaly����� �� �ʿ䰡 ���ٸ� �����ص� ����
    /// </summary>
    [ReadOnly] public List<Anomaly> effectiveAnomalys = new List<Anomaly>();
    /// <summary>
    /// TODO<br/>
    /// �̹� �߻��ϰ� �ִ� Anomaly���� Ȯ���ϰ� �ߺ�������� �ʰ� �ϴ� �۾� �ʿ�.
    /// List int���� Ȱ���� �� ����
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
