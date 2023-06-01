using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �ð�, ������� ����
/// </summary>
public class Anomaly : MonoBehaviour
{
    [Header("�⺻ ����")]
    public string anomalyName;
    [Tooltip("�ش� �̻� ���� ���� �ذ��� �� �ִ� ���� �ð�, �ʰ� �� ���� ����"), Min(0.01f)]
    public float timeLimit = 120f;

    [Space(10), Tooltip("�ش� �̻��� �߻��� ��� ��Ÿ���� ��� �����")]
    public Phenomenon[] phenomenons;

    [Header("Debug")]
    [SerializeField, Tooltip("�ش� ������ �߻��������� �ذ��ؾ��� ������ ��"), ReadOnly] int cnt_remainProbloms;
    [SerializeField, Tooltip("���б��� ���� ���� �ð�"), ReadOnly] float counter_timeLimit = -1f;

    private void Start()
    {
        cnt_remainProbloms = 0;
        counter_timeLimit = timeLimit;
        for (int i = 0; i < phenomenons.Length; i++)
        {
            //Phenomenon anoObj = Instantiate(phenomenons[i]);
            //anoObj.Init(this);
        } cnt_remainProbloms += phenomenons.Length;
    }

    private void Update()
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
    public void Child_Resolved()
    {
        if (--cnt_remainProbloms == 0)
        {
            // �ʿ� �� ���ӸŴ��� � �ذ���� �˸�
            Destroy(gameObject);
        }
    }
}
