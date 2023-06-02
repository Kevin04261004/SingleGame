using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모텔 문이 열림.
/// 안을 들여다보면 사망.
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
