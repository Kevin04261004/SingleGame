using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anomaly : MonoBehaviour
{
    [Header("기본 정보")]
    public string anomalyName;
    [Header("제한 시간"), Tooltip("해당 현상 발현 이후 해결할 수 있는 제한 시간, 초과 시 게임 오버")]
    public float timeLimit = 120f;
    [Tooltip("남은 제한 시간")]
    [SerializeField, ReadOnly] float counter_timeLimit = -1f;

    [Header("발현 시 효과")]
    [Tooltip("이상현상 전용 오브젝트를 생성\n해당 오브젝트들을 모두 해결해야 최종적으로 이상현상이 해결됨")]
    public AnomalyObject[] createObjects;

    [ReadOnly, Tooltip("해당 현상이 발생시켰으며 해결되기 위해 남은 자식들의 수")] int cnt_resolve;

    private void Start()
    {
        for (int i = 0; i < createObjects.Length; i++)
        {
            AnomalyObject anoObj = Instantiate(createObjects[i]);
            anoObj.Init(this);
        }
    }

    private void Update()
    {
        TimeCounter();
    }
    void TimeCounter()
    {
        timeLimit -= Time.deltaTime;
        if (timeLimit <= float.Epsilon)
        {
            // GameManager.GameOver();
        }
    }
    public void Child_Resolved()
    {
        if (--cnt_resolve == 0)
        {
            // 필요 시 게임매니저 등에 해결됨을 알림
            Destroy(gameObject);
        }
    }
}
