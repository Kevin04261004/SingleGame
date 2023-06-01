using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3층 복도에 경비원 출현.
/// 해결법: 관제실에 10초간 머물기
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
