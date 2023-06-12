using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ano_13 : Anomaly
{
    [SerializeField] Phe_13 prefab_stand;
    Phe_13 stand;
    [SerializeField] Vector3 spawnPos_stand;
    [SerializeField] Vector3 rotation_stand;

    public override void AnomalyEnd()
    {
        Destroy(stand.gameObject);
    }

    public override void AnomalyStart()
    {
        stand = InstantiatePhenomenon<Phe_13>(prefab_stand);
        stand.transform.position = spawnPos_stand;
        stand.transform.rotation = Quaternion.Euler(rotation_stand.x, rotation_stand.y, rotation_stand.z);
    }

    protected override void OutOfTime()
    {
        Debug.Log($"'{anomalyName}' ������ ���� �ð��� ��� ����Ͽ����ϴ�.(����)");
        Force_SolveAnomaly();
    }
}
