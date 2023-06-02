using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anomaly_DoorKnob : InteractableObject
{
    public override void Interact()
    {
        transform.parent.GetComponent<Anomaly_Door>().ToggleDoor();
        gameObject.tag = "Untagged";
    }
}
