using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_2_Person : MonoBehaviour, IInteratable
{
    [SerializeField] private PlayerMovementController playerController;
    [SerializeField] private DialogueData data = new DialogueData();
    private void OnEnable()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerMovementController>();
    }
    public void Interact()
    {
        playerController.Set_canMove_Bool(false);
        UIManager.instance.Set_DialogueGameObject_Bool(true);
        DialogueManager.instance.StartReadDialogue(data);
    }
}