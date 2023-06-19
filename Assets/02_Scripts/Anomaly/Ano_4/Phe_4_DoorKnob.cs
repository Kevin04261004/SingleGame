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
    [SerializeField, ReadOnly] bool lockEnded = false;
    [SerializeField] Phe_4_Key prefab_keyObj;
    Phe_4_Key spawnedKey;
    [SerializeField] Transform keyPivot;

    private void Awake()
    {
        parent = transform.parent.GetComponent<Phe_4_Door>();
    }
    public delegate void HasEnded();
    HasEnded ended;
    public void Init(HasEnded whenEndedProcess)
    {
        isStartedTryLockDoor = false;
        lockEnded = false;
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
            if (lockEnded)
            {
                isStartedTryLockDoor = false;
                lockEnded = false;
                ended();
                Destroy(spawnedKey.gameObject);
            }
        }
        else if (!parent.isOpen)
        {
            StartLockAnim();
        }
    }
    void StartLockAnim()
    {
        isStartedTryLockDoor = true;
        spawnedKey = Instantiate(prefab_keyObj, keyPivot);
        spawnedKey.event_keyPulled = delegate ()
        {
            lockEnded = true;
        };
    }
    public void Interact_Hold_End()
    {
        if (isStartedTryLockDoor)
        {
            // 문을 한 번에 끝까지 잠그지 않았을 경우
            if (!lockEnded)
            {
                Debug.Log("플레이어가 Ano4의 문을 끝까지 잠그지 않았습니다.");
                GameManager.instance.Died(GameManager.CauseOfDeath.violationRule, "규칙 4: 문은 끝까지 잠가야 한다");
            }
            else
            {
                ended();
            }
            Destroy(spawnedKey.gameObject);
        }
    }

    public bool IsInteractable() => parent.isInteractable;
}
