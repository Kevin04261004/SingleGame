using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� ����.
/// ���� �鿩�ٺ��� ���.
/// </summary>
public class Ano_3 : Anomaly
{
    [SerializeField] private GameObject Door;
    [SerializeField] private GameObject DoorKnob;
    public override void AnomalyStart()
    {
        DoorKnob.tag = "Interactable";
        Door.GetComponent<Anomaly_Door>().ToggleDoor();
    }
    public override void AnomalyEnd()
    {
        ;
    }
}
