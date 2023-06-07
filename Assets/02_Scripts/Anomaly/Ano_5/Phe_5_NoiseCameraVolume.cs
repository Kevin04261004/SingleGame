using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

public class Phe_5_NoiseCameraVolume : Phenomenon
{
    [SerializeField] Area_DetectPlayer prefab_detectPlayer_controlRoom;
    Area_DetectPlayer detectPlayer_controlRoom = null;
    protected override void PhenomenonEnd()
    {
        InputManager.instance.event_keyInput -= GetKey;
    }

    protected override void PhenomenonStart()
    {
        detectPlayer_controlRoom = Instantiate(prefab_detectPlayer_controlRoom);
        detectPlayer_controlRoom.event_time_playerInArea += DetectPlayer;
    }

    void DetectPlayer(float time)
    {
        detectPlayer_controlRoom.event_time_playerInArea -= DetectPlayer;
        Destroy(detectPlayer_controlRoom.gameObject);
        InputManager.instance.event_keyInput += GetKey;
    }

    void GetKey(KeyType keyType, InputType inputType)
    {
        if (keyType == KeyType.toggle_portableCCTV)
        {
            if (inputType == InputType.down)
            {
                Solution();
            }
        }
    }

    protected override void Solution()
    {
        TryFixThisPhenomenon();
    }
}
