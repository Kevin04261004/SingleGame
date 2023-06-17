using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

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
        //playerController.Set_canMove_Bool(false);
        playerController.PreventMovement_AddStack();
        UIManager.instance.Set_DialogueGameObject_Bool(true);
#warning add Dia
        //DialogueManager.instance.StartReadDialogue(data);
    }

    public void Interact_Hold() { }
    public void Interact_Hold_End() { }
    public bool IsInteractable() => true;
}