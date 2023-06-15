using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnomalyManager : Singleton<AnomalyManager>
{
#warning temporary
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ExecuteRandomAnomaly_IfNotAllAnoExecuted();
        }
    }
    protected override void Awake()
    {
        base.Awake();
        if (executeDelay == null || executeDelay.Length == 0)
        {
            Debug.LogError("���� �����̰� �������� �ʾҽ��ϴ�.");
            return;
        }
        StartCoroutine(ExecuteCoroutine(executeDelay));
    }
    [Tooltip("���ӿ� �����ϴ� �̻����� ���(Prefab)")]
    public Anomaly[] anomalyList;
    [SerializeField] int[] executeDelay;
    /// <summary>
    /// ���� �߻��ǰ� �ִ� �̻����� ����Ʈ
    /// </summary>
    [ReadOnly] public List<Anomaly> effectiveAnomalys = new List<Anomaly>();
    public bool everyAnoIsExecuting => (effectiveAnomalys.Count == anomalyList.Length);
    /// <summary>
    /// anomaly ��Ͽ��� �ϳ��� �����Ŵ<br/>
    /// �ߺ� ���ε ���� ���࿡ ������ �� ����
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
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
    /// <summary>
    /// anomaly ��Ͽ��� �ϳ��� �����Ŵ<br/>
    /// ��� Anomaly�� �������� ��츦 �����ϰ�, �ݵ�� �ϳ��� �����Ŵ
    /// </summary>
    /// <returns></returns>
    public bool ExecuteRandomAnomaly_IfNotAllAnoExecuted()
    {
        while (!everyAnoIsExecuting)
        {
            if (ExecuteAnomaly())
            {
                return true;
            }
        }
        return false;
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

    IEnumerator ExecuteCoroutine(int[] delayList)
    {
        int curIdx = 0;

        WaitForSeconds waitDelay;

        while (curIdx++ < delayList.Length)
        {
            waitDelay = new WaitForSeconds(delayList[curIdx]);
            Debug.Log($"{delayList[curIdx]}�� �� Anomaly�� �����մϴ�..");
            while (true)
            {
                yield return waitDelay;
                ExecuteRandomAnomaly_IfNotAllAnoExecuted();
            }
        }
    }
}
