using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� ����.
/// ���� �鿩�ٺ��� ���.
/// </summary>
public class Ano_3 : Anomaly
{
    private Phe_3_Door Door;
    public override void AnomalyStart()
    {
        Door = FindObjectOfType<Phe_3_Door>();

        Door.doorKnob.tag = "Interactable";
        Door.GetComponent<Phe_3_Door>().ToggleDoor();
        Door.Init(this);
    }
    public override void AnomalyEnd()
    {

    }
}
