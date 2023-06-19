using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ano_2 : Anomaly
{
    [SerializeField] Phe_2_Person prefab_phe_2_person;
    Phe_2_Person person;
    [SerializeField] Vector3 pos_spawn_guard;
    public override void AnomalyEnd()
    {
        Destroy(person.gameObject);
    }
    public override void AnomalyStart()
    {
        person = InstantiatePhenomenon(prefab_phe_2_person);
        person.gameObject.transform.localPosition = pos_spawn_guard;
    }
    public override bool CheckExecuteCondition()
    {
        return GameObject.FindObjectOfType<StageSystem.Stage>().playerLocatedArea != StageSystem.Area.AreaType.mainHall && GameObject.FindObjectOfType<StageSystem.Stage>().playerLocatedArea != StageSystem.Area.AreaType.corrider_1F;
    }
}
