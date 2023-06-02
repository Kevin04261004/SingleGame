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
    [SerializeField, ReadOnly, Tooltip("실패까지 남은 제한 시간")] float counter_timeLimit = -1f;
    [SerializeField, ReadOnly, Tooltip("해당 Anomaly가 해결되기 위해 남은 문제의 수\n(문제: 구역에 머물기, 대화하기 등)")]
    int remainProblemCount = -1;
    [SerializeField, Tooltip("해당 Anomaly가 생성한 현상(문제) 오브젝트들\nAnomaly가 종료될 때 생성된 현상 오브젝트를 제거하기 위한 컨테이너 역할")]
    List<Phenomenon> createdPhenomenons;

    public void Init()
    {
        transform.position = Vector3.zero;
        counter_timeLimit = timeLimit;
        remainProblemCount = 0;
        createdPhenomenons = new List<Phenomenon>();
        Debug.Log($"'{anomalyName}'현상이 시작되었습니다. 제한시간: {timeLimit}초");
        AnomalyStart();
    }

    protected virtual void Update()
    {
        TimeCounter();
    }
    private void OnDestroy()
    {
        AnomalyEnd();
    }
    void TimeCounter()
    {
        counter_timeLimit -= Time.deltaTime;
        if (counter_timeLimit <= float.Epsilon)
        {
            // GameManager.GameOver(deathSceneNum);
        }
    }
    /// <summary>
    /// 해당 Anomaly를 실행시킬 조건이 충족되었는지 확인. AnomalyManager가 실행할 Anomaly를 결정하는 조건이 된다.<br/>
    /// 상속받은 객체에서 상황에 맞게 bool을 반환하면 된다. (기본 값: true반환)<br/>
    /// 예) 플레이어가 관제실 안에 있을 경우 true, 아닐 경우 false
    /// </summary>
    /// <returns>true: 조건에 부합<br/>
    /// false: 조건에 부합하지 않음</returns>
    public virtual bool CheckExecuteCondition() => true;
    /// <summary>
    /// 현상(문제) 오브젝트들을 생성할 때 사용<br/>
    /// 생성할 현상이 해결할 수 있는 문제라면 problemCount를 증가시키고,
    /// Anomaly가 종료될 때 현상들을 제거할 수 있게 컨테이너에 담는다.<br/>
    /// Phenomenon의 초기화 작업도 진행한다.
    /// </summary>
    /// <param name="phenomenonPrefab"></param>
    /// <returns></returns>
    protected T InstantiatePhenomenon<T>(T phenomenonPrefab) where T : Phenomenon
    {
        if (phenomenonPrefab.hasSolution) remainProblemCount++;
        T phenomenon = Instantiate(phenomenonPrefab);
        createdPhenomenons.Add(phenomenon);
        phenomenon.Init(this);
        return phenomenon;
    }
    public void FixProblem()
    {
        // TODO
        // remainProblemCount가 InstantiatePhenomenon()말고
        // Scene에 이미 존재하는 오브젝트를 사용할 때에도 카운트되도록
        if (--remainProblemCount <= 0)
        {
            Debug.Log($"'{anomalyName}'현상이 해결되었습니다.");
            Destroy(gameObject);
            return;
        }
        Debug.Log($"'{anomalyName}'현상이 해결되기까지 {remainProblemCount}개의 문제가 남았습니다.");
    }
    /// <summary>
    /// 이상현상이 시작될때 해야할 처리<br/>
    /// (예: 현상들 생성)
    /// </summary>
    public abstract void AnomalyStart();
    /// <summary>
    /// 이상현상이 종료될때 해야할 처리<br/>
    /// (예: 현상들 파괴)
    /// </summary>
    public abstract void AnomalyEnd();
}
