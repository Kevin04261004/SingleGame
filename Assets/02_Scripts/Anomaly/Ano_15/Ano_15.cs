using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ano_15 : Anomaly
{
    public override void AnomalyEnd()
    {
        
    }

    public override void AnomalyStart()
    {
        if ((FindPhenomenonObjectInScene<Phe_15_RockingChair>()) == null)
        {
            Debug.LogError($"X");
        }
    }
}
