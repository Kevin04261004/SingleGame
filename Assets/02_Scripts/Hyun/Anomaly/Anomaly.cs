using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anomaly : MonoBehaviour
{
    [Header("�⺻ ����")]
    public string anomalyName;
    [Header("���� �ð�"), Tooltip("�ش� ���� ���� ���� �ذ��� �� �ִ� ���� �ð�, �ʰ� �� ���� ����")]
    public float timeLimit = 120f;
    [Tooltip("���� ���� �ð�")]
    [SerializeField, ReadOnly] float counter_timeLimit = -1f;

    [Header("���� �� ȿ��")]
    [Tooltip("�̻����� ���� ������Ʈ�� ����\n�ش� ������Ʈ���� ��� �ذ��ؾ� ���������� �̻������� �ذ��")]
    public AnomalyObject[] createObjects;

    [ReadOnly, Tooltip("�ش� ������ �߻��������� �ذ�Ǳ� ���� ���� �ڽĵ��� ��")] int cnt_resolve;

    private void Start()
    {
        for (int i = 0; i < createObjects.Length; i++)
        {
            AnomalyObject anoObj = Instantiate(createObjects[i]);
            anoObj.Init(this);
        }
    }

    private void Update()
    {
        TimeCounter();
    }
    void TimeCounter()
    {
        timeLimit -= Time.deltaTime;
        if (timeLimit <= float.Epsilon)
        {
            // GameManager.GameOver();
        }
    }
    public void Child_Resolved()
    {
        if (--cnt_resolve == 0)
        {
            // �ʿ� �� ���ӸŴ��� � �ذ���� �˸�
            Destroy(gameObject);
        }
    }
}
