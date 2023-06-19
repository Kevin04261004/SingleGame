using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ano_13 : Anomaly
{
    [SerializeField] Phe_13 prefab_stand;
    [SerializeField] Phe_13_invisible prefab_invi;
    Phe_13 stand;
    Phe_13_invisible invi;
    [SerializeField] Vector3 spawnPos_stand;
    [SerializeField] Vector3 spawnPos_invi;
    [SerializeField] Vector3 rotation_stand;

    public override void AnomalyEnd()
    {
        Destroy(stand.gameObject);
        Destroy(invi.gameObject);
    }

    public override void AnomalyStart()
    {
        stand = InstantiatePhenomenon<Phe_13>(prefab_stand);
        stand.transform.position = spawnPos_stand;
        stand.transform.rotation = Quaternion.Euler(rotation_stand.x, rotation_stand.y, rotation_stand.z);

        invi = InstantiatePhenomenon<Phe_13_invisible>(prefab_invi);
        invi.transform.position = spawnPos_invi;
    }

    protected override void OutOfTime()
    {
        Debug.Log($"'{anomalyName}' 현상의 유지 시간이 모두 경과하였습니다.");
        if (remainProblemCount > 1)
        {
            GameManager.instance.Died();
        }
        else
        {
            Force_SolveAnomaly();
        }
    }
    public override bool CheckExecuteCondition()
    {
        return GameObject.FindObjectOfType<StageSystem.Stage>().playerLocatedArea != StageSystem.Area.AreaType.rooftop;
    }
}
