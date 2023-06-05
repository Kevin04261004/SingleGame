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
            Debug.LogWarning("instance�� null�� �� �����ϴ�. �̹� ���ŵ� Anomaly�� �� �ֽ��ϴ�.");
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
        Debug.LogError($"{instance}�� �������� Anomaly��Ͽ� �������� �ʽ��ϴ�.\nAnomaly�� ���� �� ó���� AnomalyManager�� �����ؾ� �մϴ�.");
    }
    /// <summary>
    /// Anomaly �̸��� Ȱ���� �̹� Scene���� ����ǰ� �ִ� Anomaly���� Ȯ��
    /// </summary>
    /// <param name="anomaly"></param>
    /// <returns>true: �ش� �̸��� Anomaly�� �̹� ��������<br/>
    /// false: �ش� �̸��� Anomaly�� ����ǰ� ���� ����</returns>
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
