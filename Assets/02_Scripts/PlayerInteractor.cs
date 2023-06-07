using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

/// <summary>
/// �÷��̾ ��ȣ�ۿ��� �� �ִ� ������Ʈ���� ��ӹ޾ƾ� �� ���
/// </summary>
public interface IInteratable
{
    /// <summary>
    /// ª�� ������ <b>Ű�� ������ ���� ����</b>�Ǵ� �Լ�
    /// </summary>
    public void Interact();
    /// <summary>
    /// Ű�� ������ ���� �ð� �ڿ��� ���� ������(Ȧ�� ����) <b>Ű�� ���� ������ �� �����Ӹ��� ����</b>�� �Լ�
    /// </summary>
    public void Interact_Hold();
    /// <summary>
    /// Ű�� ������ ���� �ð� �ڿ��� (Ȧ�� ����) <b>���� �ʴٰ� Ű�� ���� ����</b>�� �Լ�
    /// </summary>
    public void Interact_Hold_End();
    /// <summary>
    /// ���� ��ȣ�ۿ��� ������ �������� �Ǻ��Ѵ�.<br/>
    /// ���� ���, ���� ������ ������ ���߿���(isOpening, isClosing) �ش� �Լ��� ��ȯ���� false�� ��ȯ�Ͽ�<br/>
    /// UI�� ���� ���� ���� �ʰ� ��ȣ�ۿ��� �ȵǵ��� �� �� �ִ�.
    /// </summary>
    public bool IsInteractable();
}

public class PlayerInteractor : MonoBehaviour
{
    public float handLength = 3f;
    public Transform forward = null;

    bool isPressedInteractKey = false;
    private float interactKeyPressTime = 0f;
    private const float holdThreshold = 0.2f;

    IInteratable interactable = null;

    private void Start()
    {
        InputManager.instance.event_keyInput += GetInput;
    }

    private void Update()
    {
        if (Physics.Raycast(forward.transform.position, forward.transform.forward, out RaycastHit hit))
        {
            Debug.DrawRay(forward.transform.position, forward.transform.forward * hit.distance, Color.red);

            if (hit.collider.CompareTag("Interactable") && hit.distance <= handLength)
            {
                interactable = hit.collider.gameObject.GetComponent<IInteratable>();
                if (interactable == null)
                {
                    Debug.LogError($"'Interactable' �±��� ������Ʈ�� {typeof(IInteratable).Name}������ ������Ʈ�� �������� �ʽ��ϴ�.");
                    return;
                }
                if (interactable.IsInteractable())
                {
                    UIManager.instance.Set_middlePoint_Image_Color(true);

                    //if (Input.GetKeyDown(KeyCode.F))
                    //{
                    //    isPressedInteractKey = true;
                    //    interactKeyPressTime = Time.time;
                    //}
                    //if (Input.GetKeyUp(KeyCode.F))
                    //{
                    //    isPressedInteractKey = false;
                    //    float pressDuration = Time.time - interactKeyPressTime;
                    //    if (pressDuration <= holdThreshold)
                    //    {
                    //        interactable.Interact();
                    //    }
                    //    else
                    //    {
                    //        interactable.Interact_Hold_End();
                    //    }
                    //}
                    if (isPressedInteractKey)
                    {
                        float pressDuration = Time.time - interactKeyPressTime;
                        if (pressDuration > holdThreshold)
                        {
                            interactable.Interact_Hold();
                        }
                    }
                }
                else
                {
                    UIManager.instance.Set_middlePoint_Image_Color(false);
                }
            }
            else
            {
                if (isPressedInteractKey)
                {
                    isPressedInteractKey = false;
                    interactKeyPressTime = Time.time;
                }
                interactable = null;
                UIManager.instance.Set_middlePoint_Image_Color(false);
            }
        }
    }

    void GetInput(KeyType keyType, InputType inputType)
    {
        if (interactable == null) return;
        if (keyType == KeyType.interact)
        {
            if (inputType == InputType.down)
            {
                isPressedInteractKey = true;
                interactKeyPressTime = Time.time;
            }
            if (inputType == InputType.up)
            {
                isPressedInteractKey = false;
                float pressDuration = Time.time - interactKeyPressTime;
                if (pressDuration <= holdThreshold)
                {
                    interactable.Interact();
                }
                else
                {
                    interactable.Interact_Hold_End();
                }
            }
        }
    }
}
