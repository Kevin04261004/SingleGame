using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ ��ȣ�ۿ��� �� �ִ� ������Ʈ���� ��ӹ޾ƾ� �� ���
/// </summary>
public interface IInteratable
{
    public void Interact();
    /// <summary>
    /// ���� ��ȣ�ۿ��� ������ �������� �Ǻ��Ѵ�.<br/>
    /// ���� ���, ���� ������ ������ ���߿���(isOpening, isClosing) �ش� �Լ��� ��ȯ���� false�� ��ȯ�Ͽ�<br/>
    /// UI�� ���� ���� ���� �ʵ��� ������ �� �� �ִ�.
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
                        Debug.LogError($"'Interactable' �±��� ������Ʈ�� {typeof(IInteratable).Name}������ ������Ʈ�� �������� �ʽ��ϴ�.");
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
