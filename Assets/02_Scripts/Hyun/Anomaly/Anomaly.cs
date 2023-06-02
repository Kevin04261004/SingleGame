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
    [SerializeField, Tooltip("���б��� ���� ���� �ð�"), ReadOnly] float counter_timeLimit = -1f;
    [SerializeField, Tooltip("�ش� Anomaly�� �ذ�Ǳ� ���� ���� ������ ��\n(����: ������ �ӹ���, ��ȭ�ϱ� ��)")]
    int remainProblemCount = -1;
    [SerializeField, Tooltip("�ش� Anomaly�� ������ ����(����) ������Ʈ��\nAnomaly�� ����� �� ������ ���� ������Ʈ�� �����ϱ� ���� �����̳� ����")]
    List<Phenomenon> createdPhenomenons;

    protected virtual void Start()
    {
        counter_timeLimit = timeLimit;
        remainProblemCount = 0;
        createdPhenomenons = new List<Phenomenon>();
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
    /// ����(����) ������Ʈ���� ������ �� ���<br/>
    /// ������ ������ �ذ��� �� �ִ� ������� problemCount�� ������Ű��,
    /// Anomaly�� ����� �� ������� ������ �� �ְ� �����̳ʿ� ��´�.<br/>
    /// Phenomenon�� �ʱ�ȭ �۾��� �����Ѵ�.
    /// </summary>
    /// <param name="phenomenonPrefab"></param>
    /// <returns></returns>
    protected Phenomenon InstantiatePhenomenon(Phenomenon phenomenonPrefab)
    {
        if (phenomenonPrefab.hasSolution) remainProblemCount++;
        Phenomenon phenomenon = Instantiate(phenomenonPrefab);
        createdPhenomenons.Add(phenomenon);
        phenomenon.Init(this);
        return phenomenon;
    }
    public void FixProblem()
    {
        if (--remainProblemCount == 0)
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
    public abstract void AnomalyEnd();}
