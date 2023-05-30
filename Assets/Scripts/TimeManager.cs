using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private int gameTime = 0;
    private float realTime = 0;
    public const float _10minPerRealSecond = 20; //게임 10분당 현실 초
    private void Update()
    {
        realTime += Time.deltaTime;
        if (realTime > _10minPerRealSecond)
        {
            gameTime += 10;
            realTime = 0;
            UIManager.instance.Set_TimeClock_TMP(gameTime);
        }
    }
}
