using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_5_NoiseCameraVolume : Phenomenon
{
    [SerializeField] Area_DetectPlayer detectPlayer_controlRoom;
    protected override void PhenomenonEnd()
    {
        throw new System.NotImplementedException();
    }

    protected override void PhenomenonStart()
    {
        
    }

    void DetectPlayer(float time)
    {
        Solution();
    }

    protected override void Solution()
    {
        
    }
}
