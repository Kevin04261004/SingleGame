using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

public class Phe_2_Person : Phenomenon, IInteratable
{
    [SerializeField] H_DialogueData dialogueData;
    bool dialogueStarted = false;
    protected override void PhenomenonEnd()
    {

    }

    protected override void PhenomenonStart()
    {
        dialogueData.AddCallbacksToAllSelections(CheckValue);
    }

    void CheckValue(ValueWhenClicked valueWhenClicked)
    {
        if (valueWhenClicked == ValueWhenClicked.False)
        {
            GameManager.instance.Died();
        }
        else if (valueWhenClicked == ValueWhenClicked.True)
        {
            TryFixThisPhenomenon();
        }
    }

    protected override void Solution()
    {
        //nothing
    }

    public void Interact()
    {
        dialogueStarted = true;
        DialogueManager.instance.Try_RequestStartReadingDialogueData(dialogueData);
    }

    public void Interact_Hold()
    {
        
    }

    public void Interact_Hold_End()
    {
        
    }

    public bool IsInteractable()
    {
        return !dialogueStarted;
    }
}