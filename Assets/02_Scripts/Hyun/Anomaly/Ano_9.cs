using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3�� ������ ���� ����.
/// �ذ��: �����ǿ� 10�ʰ� �ӹ���
/// </summary>
public class Ano_9 : Anomaly
{
    public GameObject guard;
    public Transform guardSpawnPos;

    public override void AnomalyEnd()
    {
        
    }

    public override void AnomalyStart()
    {
        GameObject _guard = Instantiate(guard);
        _guard.transform.position = guardSpawnPos.position;
    }
}
