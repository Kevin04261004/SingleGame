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
            // ���� �� ���� ������ ����� �ʾ��� ���
            if (!lockEnded)
            {
                Debug.Log("�÷��̾ Ano4�� ���� ������ ����� �ʾҽ��ϴ�.");
                GameManager.instance.Died(GameManager.CauseOfDeath.violationRule, "��Ģ 4: ���� ������ �ᰡ�� �Ѵ�");
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
