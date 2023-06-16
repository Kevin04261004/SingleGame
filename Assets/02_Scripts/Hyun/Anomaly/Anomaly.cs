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
    [SerializeField, Tooltip("해당 Anomaly가 생성한 현상(문제) 오브젝트들\nAnomaly가 종료될 때 생성된 현상 오브젝트를 제거하기 위한 컨테이너 역할"), ReadOnly]
    List<Phenomenon> phenomenonsFromThisAnomaly;

    public delegate void WhenAnomalyEnded();
    /// <summary>Anomaly가 종료되어 오브젝트가 파괴되기 직전에 호출된다.</summary>
    public event WhenAnomalyEnded event_whenAnomalyEnded;

    Coroutine coroutine_timeCounter = null;

    public void Init()
    {
        transform.position = Vector3.zero;
        counter_timeLimit = timeLimit;
        remainProblemCount = 0;
        phenomenonsFromThisAnomaly = new List<Phenomenon>();
        coroutine_timeCounter = StartCoroutine(TimeCounter());
        Debug.Log($"'{anomalyName}'현상이 시작되었습니다. 제한시간: {timeLimit}초");
        AnomalyStart();
    }
    IEnumerator TimeCounter()
    {
        WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
        while (true)
        {
            if ((counter_timeLimit -= Time.deltaTime) <= float.Epsilon) break;
            yield return waitFrame;
        }
        coroutine_timeCounter = null;
        OutOfTime();
    }
    /// <summary>
    /// 제한 시간이 모두 경과되었을 때 실행할 행위<br/>
    /// 기본적으로 GameOver시키며, 특수한 경우 오버라이딩 해서 사용할 것
    /// </summary>
    protected virtual void OutOfTime()
    {
        Debug.Log($"'{anomalyName}' 현상의 제한 시간이 모두 경과하였습니다.");
        GameManager.instance.Died();
    }
    /// <summary>
    /// 제한시간을 카운트하는 코루틴을 종료시키는 함수<br/>
    /// 예: 상호작용 시 대화창이 뜨는 현상은 대화 선택지에 따라 현상 종료가 결정되므로,
    /// 필요 시 해당 Anomlay의 제한 시간 카운트를 멈출 수 있다.
    /// </summary>
    public void StopTimeCounter()
    {
        if (coroutine_timeCounter == null)
        {
            Debug.LogWarning("제한 시간 타이머가 존재하지 않습니다. 이미 멈추었거나 실행되지 않았을 수 있습니다.");
            return;
        }
        StopCoroutine(coroutine_timeCounter);
        coroutine_timeCounter = null;
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
    /// 현상(문제) 오브젝트들을 생성할 때 사용, 생성될 때 이름을 Hierarchy에서구분하기 쉽게 변경한다.<br/>
    /// 생성할 현상이 해결할 수 있는 문제라면 problemCount를 증가시키고,
    /// Anomaly가 보유한 현상들을 확인할 수 있게 컨테이너에 담는다.
    /// </summary>
    /// <param name="phenomenonPrefab">생성시킬 Phenomenon의 Prefab</param>
    /// <param name="callInitMethod">true: Phenomenon 생성 후 자동으로 초기화(시작)한다.<br/>
    /// false: Phenomenon 생성 후 자동으로 초기화(시작)하지 않으며, 직접 초기화함수를 호출해야 한다.<br/>
    /// 생성과 동시에 시작시키고 싶지 않은 특수한 경우에 사용한다.</param>
    /// <returns></returns>
    protected T InstantiatePhenomenon<T>(T phenomenonPrefab, bool callInitMethod = true) where T : Phenomenon
    {
        if (phenomenonPrefab.hasSolution) remainProblemCount++;
        T phenomenon = Instantiate(phenomenonPrefab);
        phenomenon.gameObject.name = $"{this.GetType().Name}Obj_{phenomenonPrefab.gameObject.name}";
        phenomenonsFromThisAnomaly.Add(phenomenon);
        if (callInitMethod)
        {
            phenomenon.Init(this);
        }
        return phenomenon;
    }
    /// <summary>
    /// 현상(문제) 오브젝트를 Scene에 이미 존재하는 것을 가져와 사용할 때 사용<br/>
    /// 생성할 현상이 해결할 수 있는 문제라면 problemCount를 증가시키고,
    /// Anomaly가 보유한 현상들을 확인할 수 있게 컨테이너에 담는다.
    /// </summary>
    /// <param name="callInitMethod">true: Scene의 Phenomenon을 찾은 후 자동으로 초기화(시작)한다.<br/>
    /// false: Scene의 Phenomenon을 찾은 후 자동으로 초기화(시작)하지 않으며, 직접 초기화함수를 호출해야 한다.<br/>
    /// 찾음과 동시에 시작시키고 싶지 않은 특수한 경우에 사용한다.</param>
    /// <returns>null: Scene에서 T타입의 Phenomenon을 찾지 못함</returns>
    protected T FindPhenomenonObjectInScene<T>(bool callInitMethod = true) where T : Phenomenon
    {
        T phenomenon = FindObjectOfType<T>();
        if (phenomenon == null)
        {
            Debug.LogError($"Scene에서 '{typeof(T)}'컴포넌트가 부착된 Phenomenon을 찾지 못하였습니다.");
            return null;
        }
        if (phenomenon.hasSolution) remainProblemCount++;
        phenomenonsFromThisAnomaly.Add(phenomenon);
        if (callInitMethod)
        {
            phenomenon.Init(this);
        }
        return phenomenon;
    }
    public void ProblemSolved()
    {
        if (--remainProblemCount == 0)
        {
            Debug.Log($"'{anomalyName}'현상이 해결되었습니다.");
            DestroyAnomaly();
            return;
        }
        if (remainProblemCount < 0)
        {
            Debug.LogError("하나의 현상이 해결되기 위한 문제의 수는 음수가 될 수 없습니다.\nAnomaly의 재사용 시 제대로 된 초기화가 진행되지 않았거나 몇 개의 Anomaly가 같은 기능을 올바르게 사용중인지 확인하십시오.");
            return;
        }
        Debug.Log($"'{anomalyName}'현상이 해결되기까지 {remainProblemCount}개의 문제가 남았습니다.");
    }
    /// <summary>
    /// 남은 문제(현상)수에 상관 없이 Anomaly를 해결<br/>
    /// 특수한 경우에 사용(예: 제한 시간 종료 시 해결됨 등)
    /// </summary>
    public void Force_SolveAnomaly()
    {
        DestroyAnomaly();
    }
    public void DestroyAnomaly()
    {
        AnomalyEnd();
        event_whenAnomalyEnded?.Invoke();
        AnomalyManager.instance.RemoveAnomalyFromEffectiveList(this);
        Destroy(gameObject);
    }
    /// <summary>
    /// 이상현상이 시작될때 해야할 처리<br/>
    /// (예: Phenomenon 생성, 코루틴 실행 등)
    /// </summary>
    public abstract void AnomalyStart();
    /// <summary>
    /// 이상현상이 종료될때 해야할 처리<br/>
    /// (예: Phenomenon 파괴, 코루틴 종료 등)<br/>
    /// <b>※ Anomaly 게임 오브젝트는 이후 자동으로 파괴되므로 따로 파괴하면 안 된다.</b>
    /// </summary>
    public abstract void AnomalyEnd();
}
