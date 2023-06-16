using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

/// <summary>
/// 플레이어가 상호작용할 수 있는 오브젝트들이 상속받아야 할 기능
/// </summary>
public interface IInteratable
{
    /// <summary>
    /// 짧은 순간에 <b>키를 눌렀다 떼면 실행</b>되는 함수
    /// </summary>
    public void Interact();
    /// <summary>
    /// 키를 누르고 일정 시간 뒤에도 떼지 않으면(홀드 상태) <b>키를 떼기 전까지 매 프레임마다 실행</b>될 함수
    /// </summary>
    public void Interact_Hold();
    /// <summary>
    /// 키를 누르고 일정 시간 뒤에도 (홀드 상태) <b>떼지 않다가 키를 떼면 실행</b>될 함수
    /// </summary>
    public void Interact_Hold_End();
    /// <summary>
    /// 현재 상호작용이 가능한 상태인지 판별한다.<br/>
    /// 예를 들어, 문이 열리고 닫히는 도중에는(isOpening, isClosing) 해당 함수의 반환값을 false로 반환하여<br/>
    /// UI가 붉은 점이 되지 않고 상호작용이 안되도록 할 수 있다.
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

    private void Awake()
    {
        layer_exclude_area = (int.MaxValue ^ (1 << LayerMask.NameToLayer("Area")));
    }

    private void Start()
    {
        InputManager.instance.event_keyInput += GetInput_Interactable;
    }

    Ray interactRay;
    int layer_exclude_area = -1;

    private void Update()
    {
        interactRay.origin = forward.transform.position;
        interactRay.direction = forward.transform.forward;

        if (Physics.Raycast(interactRay, out RaycastHit hit, handLength, layer_exclude_area))
        {
            Debug.DrawRay(interactRay.origin, interactRay.direction * handLength, Color.red);
            if (hit.collider.CompareTag("Interactable"))
            {
                interactable = hit.collider.gameObject.GetComponent<IInteratable>();
                if (interactable == null)
                {
                    Debug.LogError($"'Interactable' 태그의 오브젝트가 {typeof(IInteratable).Name}형식의 컴포넌트를 갖고있지 않습니다.");
                    return;
                }
                if (interactable.IsInteractable())
                {
                    UIManager.instance.Set_middlePoint_Image_Color(true);

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
        //if (Physics.Raycast(forward.transform.position, forward.transform.forward, out RaycastHit hit))
        //{
        //    Debug.DrawRay(forward.transform.position, forward.transform.forward * hit.distance, Color.red);

        //    Debug.Log($"{hit.collider?.gameObject.name}");

        //    if (hit.collider.CompareTag("Interactable") && hit.distance <= handLength)
        //    {
        //        interactable = hit.collider.gameObject.GetComponent<IInteratable>();
        //        if (interactable == null)
        //        {
        //            Debug.LogError($"'Interactable' 태그의 오브젝트가 {typeof(IInteratable).Name}형식의 컴포넌트를 갖고있지 않습니다.");
        //            return;
        //        }
        //        if (interactable.IsInteractable())
        //        {
        //            UIManager.instance.Set_middlePoint_Image_Color(true);

        //            if (isPressedInteractKey)
        //            {
        //                float pressDuration = Time.time - interactKeyPressTime;
        //                if (pressDuration > holdThreshold)
        //                {
        //                    interactable.Interact_Hold();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            UIManager.instance.Set_middlePoint_Image_Color(false);
        //        }
        //    }
        //    else
        //    {
        //        if (isPressedInteractKey)
        //        {
        //            isPressedInteractKey = false;
        //            interactKeyPressTime = Time.time;
        //        }
        //        interactable = null;
        //        UIManager.instance.Set_middlePoint_Image_Color(false);
        //    }
        //}
    }

    void GetInput_Interactable(KeyType keyType, InputType inputType)
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
