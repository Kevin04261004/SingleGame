using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 휴대 CCTV의 모든 화면에 노이즈가 낄 때가 있습니다. 중앙 관제실에 들러 메인 CCTV를 확인한 뒤, 다시 휴대 CCTV를 확인하면 정상적으로 작동할 것입니다.
/// </summary>
public class Ano_5 : Anomaly
{
    [SerializeField] Phe_5_NoiseCameraVolume prefab_volumeObj;
    [SerializeField] Phe_5_NoiseCameraVolume volumeObj;
    public override void AnomalyEnd()
    {
        Destroy(volumeObj.gameObject);
    }

    public override void AnomalyStart()
    {
        volumeObj = InstantiatePhenomenon<Phe_5_NoiseCameraVolume>(prefab_volumeObj);
    }
    public override bool CheckExecuteCondition()
    {
        return (FindObjectOfType<StageSystem.Stage>().playerLocatedArea != StageSystem.Area.AreaType.controlRoom);
    }
}
