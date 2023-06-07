using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_11_Person : Phenomenon, IDialogue
{
    public void Fixed()
    {
        TryFixThisPhenomenon();
        
    }

    protected override void PhenomenonEnd()
    {
        UIManager.instance.Set_DialogueGameObject_Bool(false);
        UIManager.instance.Set_Buttons_Bool(false);
    }

    protected override void PhenomenonStart()
    {
        UIManager.instance.Set_Phenomenom(this);

    }

    protected override void Solution()
    {
        //nothing
    }
    
}