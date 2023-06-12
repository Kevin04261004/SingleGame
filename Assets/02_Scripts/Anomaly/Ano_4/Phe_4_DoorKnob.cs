using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �����ִٸ� ��� ���� ���� �ݰ�, ���� ���� �����ð� ���� 
/// </summary>
public class Phe_4_DoorKnob : MonoBehaviour, IInteratable
{
    Phe_4_Door parent = null;
    [SerializeField, ReadOnly] bool isStartedTryLockDoor = false;
    [SerializeField, ReadOnly] float holdingTime = 0f;
    [SerializeField] GameObject prefab_keyObj;
    [SerializeField] Transform keyPivot;
    [SerializeField] Vector3 key_spawn_pos;

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
                parent.Request_MakeNoise(false);
                ended();
            }
        }
        else if (!parent.isOpen)
        {
            isStartedTryLockDoor = true;
            parent.Request_MakeNoise();
            Instantiate(prefab_keyObj, keyPivot);
            keyPivot.gameObject.GetComponent<Animator>().SetBool("Play", true);
            keyPivot.gameObject.GetComponent<Animator>().SetBool("Play", false);
        }
    }
    public void Interact_Hold_End()
    {
        if (isStartedTryLockDoor)
        {
            // ���� �� ���� ������ ����� �ʾ��� ���
            if (holdingTime < parent.requireTimeForLockingDoor)
            {
                parent.Request_MakeNoise(false);
#warning needModification: callGameOver
                // GameOver
                Debug.Log("�÷��̾ Ano4�� ���� ������ ����� �ʾҽ��ϴ�.");
            }
            else
            {
                parent.Request_MakeNoise(false);
                ended();
            }
        }
    }

    public bool IsInteractable() => parent.isInteractable;
}
