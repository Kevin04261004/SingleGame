using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_11_Person : Phenomenon
{
    protected override void PhenomenonEnd()
    {
        UIManager.instance.Set_DialogueGameObject_Bool(false);
        UIManager.instance.Set_Buttons_Bool(false);
    }

    protected override void PhenomenonStart()
    {
        //nothing
    }

    protected override void Solution()
    {
        //nothing
    }
}
