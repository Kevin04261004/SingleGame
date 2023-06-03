using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ano_9 : Anomaly
{
    [SerializeField] Phe_9_Guard prefab_guard;
    Phe_9_Guard guard;
    [SerializeField] Vector3 pos_spawn_guard;
    public override void AnomalyEnd()
    {
        Destroy(guard.gameObject);
    }

    public override void AnomalyStart()
    {
        guard = InstantiatePhenomenon(prefab_guard);
        guard.gameObject.transform.localPosition = pos_spawn_guard;
    }
}
