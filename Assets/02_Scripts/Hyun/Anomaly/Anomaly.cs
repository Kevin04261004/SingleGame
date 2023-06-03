using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Anomaly : MonoBehaviour
{
    [Header("�⺻ ����")]
    public string anomalyName;
    [Tooltip("�ش� �̻� ���� ���� �ذ��� �� �ִ� ���� �ð�, �ʰ� �� ���� ����"), Min(0.01f)]
    public float timeLimit = 120f;

    [Header("Debug")]
    [SerializeField, ReadOnly, Tooltip("���б��� ���� ���� �ð�")] float counter_timeLimit = -1f;
    [SerializeField, ReadOnly, Tooltip("�ش� Anomaly�� �ذ�Ǳ� ���� ���� ������ ��\n(����: ������ �ӹ���, ��ȭ�ϱ� ��)")]
    int remainProblemCount = -1;
    [SerializeField, Tooltip("�ش� Anomaly�� ������ ����(����) ������Ʈ��\nAnomaly�� ����� �� ������ ���� ������Ʈ�� �����ϱ� ���� �����̳� ����")]
    List<Phenomenon> phenomenonsFromThisAnomaly;

    public delegate void WhenAnomalyEnded();
    /// <summary>Anomaly�� ����Ǿ� ������Ʈ�� �ı��Ǳ� ������ ȣ��ȴ�.</summary>
    public event WhenAnomalyEnded event_whenAnomalyEnded;

    public void Init()
    {
        transform.position = Vector3.zero;
        counter_timeLimit = timeLimit;
        remainProblemCount = 0;
        phenomenonsFromThisAnomaly = new List<Phenomenon>();
        Debug.Log($"'{anomalyName}'������ ���۵Ǿ����ϴ�. ���ѽð�: {timeLimit}��");
        AnomalyStart();
    }

    protected virtual void Update()
    {
        TimeCounter();
    }
#warning need modification: Call GameOver
    void TimeCounter()
    {
        counter_timeLimit -= Time.deltaTime;
        if (counter_timeLimit <= float.Epsilon)
        {
            // GameManager.GameOver(deathSceneNum);
        }
    }
    /// <summary>
    /// �ش� Anomaly�� �����ų ������ �����Ǿ����� Ȯ��. AnomalyManager�� ������ Anomaly�� �����ϴ� ������ �ȴ�.<br/>
    /// ��ӹ��� ��ü���� ��Ȳ�� �°� bool�� ��ȯ�ϸ� �ȴ�. (�⺻ ��: true��ȯ)<br/>
    /// ��) �÷��̾ ������ �ȿ� ���� ��� true, �ƴ� ��� false
    /// </summary>
    /// <returns>true: ���ǿ� ����<br/>
    /// false: ���ǿ� �������� ����</returns>
    public virtual bool CheckExecuteCondition() => true;
    /// <summary>
    /// ����(����) ������Ʈ���� ������ �� ���, ������ �� �̸��� Hierarchy���������ϱ� ���� �����Ѵ�.<br/>
    /// ������ ������ �ذ��� �� �ִ� ������� problemCount�� ������Ű��,
    /// Anomaly�� ������ ������� Ȯ���� �� �ְ� �����̳ʿ� ��´�.<br/>
    /// Phenomenon�� �ʱ�ȭ �۾��� �����Ѵ�.
    /// </summary>
    /// <param name="phenomenonPrefab"></param>
    /// <returns></returns>
    protected T InstantiatePhenomenon<T>(T phenomenonPrefab) where T : Phenomenon
    {
        if (phenomenonPrefab.hasSolution) remainProblemCount++;
        T phenomenon = Instantiate(phenomenonPrefab);
        phenomenon.gameObject.name = $"{this.GetType().Name}Obj_{phenomenonPrefab.gameObject.name}";
        phenomenonsFromThisAnomaly.Add(phenomenon);
        phenomenon.Init(this);
        return phenomenon;
    }
    /// <summary>
    /// ����(����) ������Ʈ�� Scene�� �̹� �����ϴ� ���� ������ ����� �� ���<br/>
    /// ������ ������ �ذ��� �� �ִ� ������� problemCount�� ������Ű��,
    /// Anomaly�� ������ ������� Ȯ���� �� �ְ� �����̳ʿ� ��´�.<br/>
    /// Phenomenon�� �ʱ�ȭ �۾��� �����Ѵ�.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="phenomenonType"></param>
    /// <returns>null: Scene���� TŸ���� Phenomenon�� ã�� ����</returns>
    protected T FindPhenomenonObjectInScene<T>() where T : Phenomenon
    {
        T phenomenon = FindObjectOfType<T>();
        if (phenomenon == null)
        {
            Debug.LogError($"Scene���� '{typeof(T)}'������Ʈ�� ������ Phenomenon�� ã�� ���Ͽ����ϴ�.");
            return null;
        }
        if (phenomenon.hasSolution) remainProblemCount++;
        phenomenonsFromThisAnomaly.Add(phenomenon);
        phenomenon.Init(this);
        return phenomenon;
    }
    public void ProblemSolved()
    {
        if (--remainProblemCount == 0)
        {
            Debug.Log($"'{anomalyName}'������ �ذ�Ǿ����ϴ�.");
            DestroyAnomaly();
            return;
        }
        Debug.Log($"'{anomalyName}'������ �ذ�Ǳ���� {remainProblemCount}���� ������ ���ҽ��ϴ�.");
    }
    public void DestroyAnomaly()
    {
        AnomalyEnd();
        event_whenAnomalyEnded?.Invoke();
        Destroy(gameObject);
    }
    /// <summary>
    /// �̻������� ���۵ɶ� �ؾ��� ó��<br/>
    /// (��: Phenomenon ����, �ڷ�ƾ ���� ��)
    /// </summary>
    public abstract void AnomalyStart();
    /// <summary>
    /// �̻������� ����ɶ� �ؾ��� ó��<br/>
    /// (��: Phenomenon �ı�, �ڷ�ƾ ���� ��)
    /// </summary>
    public abstract void AnomalyEnd();
}
