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
    List<Phenomenon> createdPhenomenons;

    public void Init()
    {
        transform.position = Vector3.zero;
        counter_timeLimit = timeLimit;
        remainProblemCount = 0;
        createdPhenomenons = new List<Phenomenon>();
        Debug.Log($"'{anomalyName}'������ ���۵Ǿ����ϴ�. ���ѽð�: {timeLimit}��");
        AnomalyStart();
    }

    protected virtual void Update()
    {
        TimeCounter();
    }
    private void OnDestroy()
    {
        AnomalyEnd();
    }
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
    /// ����(����) ������Ʈ���� ������ �� ���<br/>
    /// ������ ������ �ذ��� �� �ִ� ������� problemCount�� ������Ű��,
    /// Anomaly�� ����� �� ������� ������ �� �ְ� �����̳ʿ� ��´�.<br/>
    /// Phenomenon�� �ʱ�ȭ �۾��� �����Ѵ�.
    /// </summary>
    /// <param name="phenomenonPrefab"></param>
    /// <returns></returns>
    protected T InstantiatePhenomenon<T>(T phenomenonPrefab) where T : Phenomenon
    {
        if (phenomenonPrefab.hasSolution) remainProblemCount++;
        T phenomenon = Instantiate(phenomenonPrefab);
        createdPhenomenons.Add(phenomenon);
        phenomenon.Init(this);
        return phenomenon;
    }
    public void FixProblem()
    {
        // TODO
        // remainProblemCount�� InstantiatePhenomenon()����
        // Scene�� �̹� �����ϴ� ������Ʈ�� ����� ������ ī��Ʈ�ǵ���
        if (--remainProblemCount <= 0)
        {
            Debug.Log($"'{anomalyName}'������ �ذ�Ǿ����ϴ�.");
            Destroy(gameObject);
            return;
        }
        Debug.Log($"'{anomalyName}'������ �ذ�Ǳ���� {remainProblemCount}���� ������ ���ҽ��ϴ�.");
    }
    /// <summary>
    /// �̻������� ���۵ɶ� �ؾ��� ó��<br/>
    /// (��: ����� ����)
    /// </summary>
    public abstract void AnomalyStart();
    /// <summary>
    /// �̻������� ����ɶ� �ؾ��� ó��<br/>
    /// (��: ����� �ı�)
    /// </summary>
    public abstract void AnomalyEnd();
}
