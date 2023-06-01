using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ ��ȣ�ۿ��� �� �ִ� ������Ʈ���� ��ӹ޾ƾ� �� ���
/// </summary>
public interface IInteratable
{
    public void Interact();
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

            Debug.Log(hit.collider.gameObject.name);

            if (hit.collider.CompareTag("Interactable") && hit.distance <= handLength)
            {
                UIManager.instance.Set_middlePoint_Image_Color(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    InteractableObject interactable = hit.collider.gameObject.GetComponent<InteractableObject>();
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
