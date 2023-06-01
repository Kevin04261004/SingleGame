using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnob : InteractableObject
{
    public override void Interact()
    {
        transform.GetComponentInParent<Door>().ToggleDoor();
    }
}
