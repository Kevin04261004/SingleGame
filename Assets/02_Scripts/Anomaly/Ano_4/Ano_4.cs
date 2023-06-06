using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ano_4 : Anomaly
{
    Phe_4_Door door = null;

    public override void AnomalyEnd()
    {
        
    }

    public override void AnomalyStart()
    {
        door = FindPhenomenonObjectInScene<Phe_4_Door>();
    }
}
