using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_11_Person : InteractableObject
{
    [SerializeField] private PlayerMovementController playerController;
    [SerializeField] private string name;
    [SerializeField] private string content;
    [SerializeField] private string btn1;
    [SerializeField] private string btn2;
    [SerializeField] private string btn3;

    private void OnEnable()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerMovementController>();
    }
    public override void Interact()
    {
        playerController.Set_canMove_Bool(false);
        UIManager.instance.Set_DialogueGameObject_Bool(true);
        DialogueManager.instance.StartReadDialogue(name, content,btn1,btn2,btn3);
    }
}