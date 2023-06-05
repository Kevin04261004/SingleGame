using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ano_14 : Anomaly
{
    [SerializeField] private Phe_14 phe_14;
    Phe_14 phe;
    [SerializeField] private LightManager lightManager;

    public override void AnomalyEnd()
    {
        lightManager.SecondFloorLight_Bool(true);
        Destroy(phe.gameObject);
    }

    public override void AnomalyStart()
    {
        phe = InstantiatePhenomenon(phe_14);
        lightManager = GameObject.FindObjectOfType<LightManager>();
        lightManager.SecondFloorLight_Bool(false);
    }
}