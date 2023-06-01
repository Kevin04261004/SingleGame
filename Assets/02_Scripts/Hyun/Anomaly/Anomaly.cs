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

    protected virtual void Start()
    {
        counter_timeLimit = timeLimit;
        AnomalyStart();
    }

    protected virtual void Update()
    {
        TimeCounter();
    }
    void TimeCounter()
    {
        counter_timeLimit -= Time.deltaTime;
        if (counter_timeLimit <= float.Epsilon)
        {
            // GameManager.GameOver();
        }
    }
    private void OnDestroy()
    {
        AnomalyEnd();
    }
    /// <summary>
    /// �̻������� ���۵ɶ� �ؾ��� ó��(��: ���� ���� ����)
    /// </summary>
    public abstract void AnomalyStart();
    /// <summary>
    /// �̻������� ����ɶ� �ؾ��� ó��(��: ������Ʈ �ı�)
    /// </summary>
    public abstract void AnomalyEnd();}
