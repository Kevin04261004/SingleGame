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
    [SerializeField, Tooltip("�ش� Anomaly�� ������ ����(����) ������Ʈ��\nAnomaly�� ����� �� ������ ���� ������Ʈ�� �����ϱ� ���� �����̳� ����"), ReadOnly]
    List<Phenomenon> phenomenonsFromThisAnomaly;

    public delegate void WhenAnomalyEnded();
    /// <summary>Anomaly�� ����Ǿ� ������Ʈ�� �ı��Ǳ� ������ ȣ��ȴ�.</summary>
    public event WhenAnomalyEnded event_whenAnomalyEnded;

    Coroutine coroutine_timeCounter = null;

    public void Init()
    {
        transform.position = Vector3.zero;
        counter_timeLimit = timeLimit;
        remainProblemCount = 0;
        phenomenonsFromThisAnomaly = new List<Phenomenon>();
        coroutine_timeCounter = StartCoroutine(TimeCounter());
        Debug.Log($"'{anomalyName}'������ ���۵Ǿ����ϴ�. ���ѽð�: {timeLimit}��");
        AnomalyStart();
    }
    IEnumerator TimeCounter()
    {
        WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
        while (true)
        {
            if ((counter_timeLimit -= Time.deltaTime) <= float.Epsilon) break;
            yield return waitFrame;
        }
        coroutine_timeCounter = null;
        OutOfTime();
    }
    /// <summary>
    /// ���� �ð��� ��� ����Ǿ��� �� ������ ����<br/>
    /// �⺻������ GameOver��Ű��, Ư���� ��� �������̵� �ؼ� ����� ��
    /// </summary>
    protected virtual void OutOfTime()
    {
        Debug.Log($"'{anomalyName}' ������ ���� �ð��� ��� ����Ͽ����ϴ�.");
        GameManager.instance.Died();
    }
    /// <summary>
    /// ���ѽð��� ī��Ʈ�ϴ� �ڷ�ƾ�� �����Ű�� �Լ�<br/>
    /// ��: ��ȣ�ۿ� �� ��ȭâ�� �ߴ� ������ ��ȭ �������� ���� ���� ���ᰡ �����ǹǷ�,
    /// �ʿ� �� �ش� Anomlay�� ���� �ð� ī��Ʈ�� ���� �� �ִ�.
    /// </summary>
    public void StopTimeCounter()
    {
        if (coroutine_timeCounter == null)
        {
            Debug.LogWarning("���� �ð� Ÿ�̸Ӱ� �������� �ʽ��ϴ�. �̹� ���߾��ų� ������� �ʾ��� �� �ֽ��ϴ�.");
            return;
        }
        StopCoroutine(coroutine_timeCounter);
        coroutine_timeCounter = null;
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
    /// Anomaly�� ������ ������� Ȯ���� �� �ְ� �����̳ʿ� ��´�.
    /// </summary>
    /// <param name="phenomenonPrefab">������ų Phenomenon�� Prefab</param>
    /// <param name="callInitMethod">true: Phenomenon ���� �� �ڵ����� �ʱ�ȭ(����)�Ѵ�.<br/>
    /// false: Phenomenon ���� �� �ڵ����� �ʱ�ȭ(����)���� ������, ���� �ʱ�ȭ�Լ��� ȣ���ؾ� �Ѵ�.<br/>
    /// ������ ���ÿ� ���۽�Ű�� ���� ���� Ư���� ��쿡 ����Ѵ�.</param>
    /// <returns></returns>
    protected T InstantiatePhenomenon<T>(T phenomenonPrefab, bool callInitMethod = true) where T : Phenomenon
    {
        if (phenomenonPrefab.hasSolution) remainProblemCount++;
        T phenomenon = Instantiate(phenomenonPrefab);
        phenomenon.gameObject.name = $"{this.GetType().Name}Obj_{phenomenonPrefab.gameObject.name}";
        phenomenonsFromThisAnomaly.Add(phenomenon);
        if (callInitMethod)
        {
            phenomenon.Init(this);
        }
        return phenomenon;
    }
    /// <summary>
    /// ����(����) ������Ʈ�� Scene�� �̹� �����ϴ� ���� ������ ����� �� ���<br/>
    /// ������ ������ �ذ��� �� �ִ� ������� problemCount�� ������Ű��,
    /// Anomaly�� ������ ������� Ȯ���� �� �ְ� �����̳ʿ� ��´�.
    /// </summary>
    /// <param name="callInitMethod">true: Scene�� Phenomenon�� ã�� �� �ڵ����� �ʱ�ȭ(����)�Ѵ�.<br/>
    /// false: Scene�� Phenomenon�� ã�� �� �ڵ����� �ʱ�ȭ(����)���� ������, ���� �ʱ�ȭ�Լ��� ȣ���ؾ� �Ѵ�.<br/>
    /// ã���� ���ÿ� ���۽�Ű�� ���� ���� Ư���� ��쿡 ����Ѵ�.</param>
    /// <returns>null: Scene���� TŸ���� Phenomenon�� ã�� ����</returns>
    protected T FindPhenomenonObjectInScene<T>(bool callInitMethod = true) where T : Phenomenon
    {
        T phenomenon = FindObjectOfType<T>();
        if (phenomenon == null)
        {
            Debug.LogError($"Scene���� '{typeof(T)}'������Ʈ�� ������ Phenomenon�� ã�� ���Ͽ����ϴ�.");
            return null;
        }
        if (phenomenon.hasSolution) remainProblemCount++;
        phenomenonsFromThisAnomaly.Add(phenomenon);
        if (callInitMethod)
        {
            phenomenon.Init(this);
        }
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
        if (remainProblemCount < 0)
        {
            Debug.LogError("�ϳ��� ������ �ذ�Ǳ� ���� ������ ���� ������ �� �� �����ϴ�.\nAnomaly�� ���� �� ����� �� �ʱ�ȭ�� ������� �ʾҰų� �� ���� Anomaly�� ���� ����� �ùٸ��� ��������� Ȯ���Ͻʽÿ�.");
            return;
        }
        Debug.Log($"'{anomalyName}'������ �ذ�Ǳ���� {remainProblemCount}���� ������ ���ҽ��ϴ�.");
    }
    /// <summary>
    /// ���� ����(����)���� ��� ���� Anomaly�� �ذ�<br/>
    /// Ư���� ��쿡 ���(��: ���� �ð� ���� �� �ذ�� ��)
    /// </summary>
    public void Force_SolveAnomaly()
    {
        DestroyAnomaly();
    }
    public void DestroyAnomaly()
    {
        AnomalyEnd();
        event_whenAnomalyEnded?.Invoke();
        AnomalyManager.instance.RemoveAnomalyFromEffectiveList(this);
        Destroy(gameObject);
    }
    /// <summary>
    /// �̻������� ���۵ɶ� �ؾ��� ó��<br/>
    /// (��: Phenomenon ����, �ڷ�ƾ ���� ��)
    /// </summary>
    public abstract void AnomalyStart();
    /// <summary>
    /// �̻������� ����ɶ� �ؾ��� ó��<br/>
    /// (��: Phenomenon �ı�, �ڷ�ƾ ���� ��)<br/>
    /// <b>�� Anomaly ���� ������Ʈ�� ���� �ڵ����� �ı��ǹǷ� ���� �ı��ϸ� �� �ȴ�.</b>
    /// </summary>
    public abstract void AnomalyEnd();
}
