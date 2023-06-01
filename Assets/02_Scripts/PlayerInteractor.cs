using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 상호작용할 수 있는 오브젝트들이 상속받아야 할 기능
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
