using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 제한 시간, 현상들을 관리
/// </summary>
public class Anomaly : MonoBehaviour
{
    [Header("기본 정보")]
    public string anomalyName;
    [Tooltip("해당 이상 발현 이후 해결할 수 있는 제한 시간, 초과 시 게임 오버"), Min(0.01f)]
    public float timeLimit = 120f;

    [Space(10), Tooltip("해당 이상이 발생할 경우 나타나는 모든 현상들")]
    public Phenomenon[] phenomenons;

    [Header("Debug")]
    [SerializeField, Tooltip("해당 현상이 발생시켰으며 해결해야할 문제의 수"), ReadOnly] int cnt_remainProbloms;
    [SerializeField, Tooltip("실패까지 남은 제한 시간"), ReadOnly] float counter_timeLimit = -1f;

    private void Start()
    {
        cnt_remainProbloms = 0;
        counter_timeLimit = timeLimit;
        for (int i = 0; i < phenomenons.Length; i++)
        {
            //Phenomenon anoObj = Instantiate(phenomenons[i]);
            //anoObj.Init(this);
        } cnt_remainProbloms += phenomenons.Length;
    }

    private void Update()
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
    public void Child_Resolved()
    {
        if (--cnt_remainProbloms == 0)
        {
            // 필요 시 게임매니저 등에 해결됨을 알림
            Destroy(gameObject);
        }
    }
}
