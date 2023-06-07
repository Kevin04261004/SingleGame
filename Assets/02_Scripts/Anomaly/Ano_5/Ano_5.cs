using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �޴� CCTV�� ��� ȭ�鿡 ����� �� ���� �ֽ��ϴ�. �߾� �����ǿ� �鷯 ���� CCTV�� Ȯ���� ��, �ٽ� �޴� CCTV�� Ȯ���ϸ� ���������� �۵��� ���Դϴ�.
/// </summary>
public class Ano_5 : Anomaly
{
    [SerializeField] Phe_5_NoiseCameraVolume volumeObj;
    public override void AnomalyEnd()
    {
        
    }

    public override void AnomalyStart()
    {
        InstantiatePhenomenon<Phe_5_NoiseCameraVolume>(volumeObj);
    }
    public override bool CheckExecuteCondition()
    {
        return (FindObjectOfType<StageSystem.Stage>()?.playerLocatedArea != StageSystem.Area.AreaType.controlRoom);
    }
}
