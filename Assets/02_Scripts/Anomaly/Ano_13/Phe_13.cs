using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_13 : Phenomenon, IFlashLight
{
    public void OnLighting()
    {
#warning todo: call GameOver
        Debug.Log("불을 킨 상태로 바라보고 있습니다.(게임오버)");
    }

    public void OnLighting_End()
    {
        
    }

    public void OnLighting_Start()
    {
        
    }

    // 시야각 구현하고
    // 플레이어 시야가 이 오브젝트에 닿으면 함수 실행해서 GameOver시키면 될듯?
    protected override void PhenomenonEnd()
    {
        
    }

    protected override void PhenomenonStart()
    {
        
    }

    protected override void Solution()
    {
        
    }
}
