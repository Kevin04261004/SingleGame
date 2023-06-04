using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ano_11 : Anomaly
{
    [SerializeField] Phe_11_Person prefab_phe_11_person;
    Phe_11_Person person;
    [SerializeField] Vector3 pos_spawn_guard;
    public override void AnomalyEnd()
    {
        Destroy(person.gameObject);
    }
    public override void AnomalyStart()
    {
        person = InstantiatePhenomenon(prefab_phe_11_person);
        person.gameObject.transform.localPosition = pos_spawn_guard;
    }

}
