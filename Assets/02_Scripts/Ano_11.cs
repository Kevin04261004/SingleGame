using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ano_11 : Anomaly
{
    [SerializeField] private GameObject Person;
    public override void AnomalyStart()
    {
        Person.SetActive(true);
    }
    public override void AnomalyEnd()
    {
        Person.SetActive(false);
    }
}
