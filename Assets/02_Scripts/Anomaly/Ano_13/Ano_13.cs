using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ano_13 : Anomaly
{
    [SerializeField] Phe_13 prefab_stand;
    Phe_13 stand;

    public override void AnomalyEnd()
    {
        Destroy(stand.gameObject);
    }

    public override void AnomalyStart()
    {
        stand = InstantiatePhenomenon<Phe_13>(prefab_stand);
    }

    protected override void OutOfTime()
    {
        
    }
}
