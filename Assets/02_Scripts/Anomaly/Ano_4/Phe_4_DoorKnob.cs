using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 문이 열려있다면 잠깐 눌러 문을 닫고, 닫은 이후 일정시간 눌러 
/// </summary>
public class Phe_4_DoorKnob : MonoBehaviour, IInteratable
{
    Phe_4_Door parent = null;
    [SerializeField, ReadOnly] bool isStartedTryLockDoor = false;
    [SerializeField, ReadOnly] float holdingTime = 0f;

    private void Awake()
    {
        parent = transform.parent.GetComponent<Phe_4_Door>();
    }
    public delegate void HasEnded();
    HasEnded ended;
    public void Init(HasEnded whenEndedProcess)
    {
        isStartedTryLockDoor = false;
        holdingTime = 0f;
        ended = whenEndedProcess;
    }
    public void Interact()
    {
        if (parent.isOpen)
        {
            parent.ToggleDoor();
        }
    }

    public void Interact_Hold()
    {
        if (isStartedTryLockDoor)
        {
            holdingTime += Time.deltaTime;
            if (holdingTime >= parent.requireTimeForLockingDoor)
            {
                isStartedTryLockDoor = false;
                ended();
            }
        }
        else if (!parent.isOpen)
        {
            isStartedTryLockDoor = true;
        }
    }
    public void Interact_Hold_End()
    {
        if (isStartedTryLockDoor)
        {
            // 문을 한 번에 끝까지 잠그지 않았을 경우
            if (holdingTime < parent.requireTimeForLockingDoor)
            {
#warning needModification: callGameOver
                // GameOver
            }
            else
            {
                ended();
            }
        }
    }

    public bool IsInteractable() => parent.isInteractable;
}
