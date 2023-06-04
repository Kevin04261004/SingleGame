using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnob : MonoBehaviour, IInteratable
{
    public void Interact()
    {
        transform.GetComponentInParent<Door>().ToggleDoor();
    }
}
