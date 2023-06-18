using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;
using StageSystem;

public class Phe_8_Girl : Phenomenon
{
    [SerializeField] private H_DialogueData dialogue;
    Stage stage;
    [SerializeField] float timeLimit_go_controlRoom = 15;
    bool after_ignore = false;

    protected override void PhenomenonEnd()
    {
        stage.event_player_area_enter -= DetectPlayer;
        if (coroutine_timeLimit != null)
        {
            StopCoroutine(coroutine_timeLimit);
        }
    }

    protected override void PhenomenonStart()
    {
        stage = GameObject.FindObjectOfType<Stage>();
        stage.event_player_area_enter += DetectPlayer;
        dialogue.AddCallbacksToAllSelections(CheckValue);
    }

    protected override void Solution()
    {
        
    }

    void CheckValue(ValueWhenClicked valueWhenClicked)
    {
        if (valueWhenClicked == ValueWhenClicked.False)
        {
            GameManager.instance.Died();
        }
        else if (valueWhenClicked == ValueWhenClicked.True)
        {
            after_ignore = true;
            DialogueManager.instance.TryStopReadingSummaries();
            coroutine_timeLimit = StartCoroutine(TimeLimit_ControlRoom());
        }
    }
    Coroutine coroutine_timeLimit = null;
    IEnumerator TimeLimit_ControlRoom()
    {
        WaitForSeconds waitSec = new WaitForSeconds(timeLimit_go_controlRoom);
        Debug.Log($"Ano8: {timeLimit_go_controlRoom}초 내로 관제실에 입장해야 합니다.");

        yield return waitSec;
        Debug.Log($"Ano8: {timeLimit_go_controlRoom}초 내로 관제실에 입장하지 못했습니다.");
        GameManager.instance.Died();

        coroutine_timeLimit = null;
    }

    void DetectPlayer(Area.AreaType areaType)
    {
        if (after_ignore)
        {
            if (areaType == Area.AreaType.controlRoom)
            {
                TryFixThisPhenomenon();
            }
        }
        else
        {
            if (areaType == Area.AreaType.corrider_2F)
            {
                DialogueManager.instance.Try_RequestStartReadingDialogueData(dialogue);
            }
        }
    }
}
