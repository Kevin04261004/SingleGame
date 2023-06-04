using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 상호작용할 수 있는 오브젝트들이 상속받아야 할 기능
/// </summary>
public interface IInteratable
{
    public void Interact();
    /// <summary>
    /// 현재 상호작용이 가능한 상태인지 판별한다.<br/>
    /// 예를 들어, 문이 열리고 닫히는 도중에는(isOpening, isClosing) 해당 함수의 반환값을 false로 반환하여<br/>
    /// UI가 붉은 점이 되지 않도록 조건을 걸 수 있다.
    /// </summary>
    public virtual bool IsInteractable() => true;
}

public class PlayerInteractor : MonoBehaviour
{
    public float handLength = 3f;
    public Transform forward = null;

    private void Update()
    {
        if (Physics.Raycast(forward.transform.position, forward.transform.forward, out RaycastHit hit))
        {
            Debug.DrawRay(forward.transform.position, forward.transform.forward * hit.distance, Color.red);

            if (hit.collider.CompareTag("Interactable") && hit.distance <= handLength)
            {
                UIManager.instance.Set_middlePoint_Image_Color(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    IInteratable interactable = hit.collider.gameObject.GetComponent<IInteratable>();
                    if (interactable == null)
                    {
                        Debug.LogError($"'Interactable' 태그의 오브젝트가 {typeof(IInteratable).Name}형식의 컴포넌트를 갖고있지 않습니다.");
                        return;
                    }
                    interactable.Interact();
                }
            }
            else
            {
                UIManager.instance.Set_middlePoint_Image_Color(false);
            }
        }
    }
}
