using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ano6 : Anomaly
{
    [SerializeField] Phe_6_Blood prefab_blood;
    Phe_6_Blood bloodObj;
    [SerializeField] Vector3 bloodSpawnPos;

    public override void AnomalyEnd()
    {
        Destroy(bloodObj.gameObject);
    }

    public override void AnomalyStart()
    {
        bloodObj = InstantiatePhenomenon<Phe_6_Blood>(prefab_blood);
        bloodObj.gameObject.transform.position = bloodSpawnPos;
    }
}
