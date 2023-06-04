using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_3_DoorKnob : MonoBehaviour, IInteratable
{
    public void Interact()
    {
        transform.parent.GetComponent<Phe_3_Door>().ToggleDoor();
        gameObject.tag = "Untagged";
    }
}
