using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Anomaly : MonoBehaviour
{
    [Header("기본 정보")]
    public string anomalyName;
    [Tooltip("해당 이상 발현 이후 해결할 수 있는 제한 시간, 초과 시 게임 오버"), Min(0.01f)]
    public float timeLimit = 120f;

    [Header("Debug")]
    [SerializeField, Tooltip("실패까지 남은 제한 시간"), ReadOnly] float counter_timeLimit = -1f;

    protected virtual void Start()
    {
        counter_timeLimit = timeLimit;
        AnomalyStart();
    }

    protected virtual void Update()
    {
        TimeCounter();
    }
    void TimeCounter()
    {
        counter_timeLimit -= Time.deltaTime;
        if (counter_timeLimit <= float.Epsilon)
        {
            // GameManager.GameOver();
        }
    }
    private void OnDestroy()
    {
        AnomalyEnd();
    }
    /// <summary>
    /// 이상현상이 시작될때 해야할 처리(예: 판정 구역 생성)
    /// </summary>
    public abstract void AnomalyStart();
    /// <summary>
    /// 이상현상이 종료될때 해야할 처리(예: 오브젝트 파괴)
    /// </summary>
    public abstract void AnomalyEnd();}
