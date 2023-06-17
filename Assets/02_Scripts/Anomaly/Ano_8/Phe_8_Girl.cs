using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

public class Phe_8_Girl : Phenomenon
{
    [SerializeField] private H_DialogueData dialogue;
    [SerializeField] private Area_DetectPlayer area_startDialogue;

    protected override void PhenomenonEnd()
    {
        area_startDialogue.event_time_playerInArea -= DetectPlayer;
    }

    protected override void PhenomenonStart()
    {
        area_startDialogue.event_time_playerInArea += DetectPlayer;
    }

    protected override void Solution()
    {
        
    }

    void DetectPlayer(float time)
    {
        // dialogue Ãâ·Â
    }
}
