using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ano_8 : Anomaly
{
    [SerializeField] Phe_8_Girl prefab_phe8girl;
    Phe_8_Girl phe8girl;
    [SerializeField] Vector3 spawnPos_phe8;
    public override void AnomalyEnd()
    {
        Destroy(phe8girl.gameObject);
    }

    public override void AnomalyStart()
    {
        phe8girl = InstantiatePhenomenon<Phe_8_Girl>(prefab_phe8girl);
        phe8girl.gameObject.transform.position = spawnPos_phe8;
    }
}
