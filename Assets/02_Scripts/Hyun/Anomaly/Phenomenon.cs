using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("Dialogue를 객체화 함에 따라 더 이상 사용할 필요가 없음", true)]
public interface IDialogue
{
    public void Fixed();
}
/// <summary>
/// Anomaly가 발생됨에 따라 발생하는 현상들<br/>
/// '해결' 조건이 걸려있으며 해당 오브젝트를 해결 시 자신을 발현시킨 Anomaly의 제거 카운트를 감소시키는 함수를 호출한다.<br/>
/// 해결할 수 없으면 '현상', 해결할 수 있으면 '문제'라고 칭한다.
/// </summary>
[System.Serializable]
public abstract class Phenomenon : MonoBehaviour
{
    /// <summary>자신이 소속된(자신을 생성한) Anomaly의 인스턴스</summary>
    protected Anomaly from = null;
    /// <summary>
    /// 해당 현상이 해결할 수 있는 문제일 경우 true<br/>
    /// true일 경우 Anomaly가 해당 현상을 초기화하는 과정에서 문제 카운트를 증가시킨다.
    /// </summary>
    public bool hasSolution = false;
    /// <summary>
    /// 자신을 사용(생성 또는 찾은)하는 Anomaly가 호출한다.
    /// </summary>
    public void Init(Anomaly anomalyInstance)
    {
        from = anomalyInstance;
        if (!hasSolution)
        {
            from.event_whenAnomalyEnded += PhenomenonEnd;
        }
        PhenomenonStart();
    }
    /// <summary>
    /// 해당 현상이 해결방법을 갖지 않으면(hasSolution == false) 빈 내용으로 구현해도 문제 없다.<br/>
    /// <b>해결 방법이 있을 경우 반드시 구현해야 하며 문제를 해결했다면 TryFixThisPhenomenon을 호출해 해당 현상이 해결됬음을 알려야 한다.</b>
    /// </summary>
    protected abstract void Solution();
    protected void TryFixThisPhenomenon()
    {
        if (!hasSolution)
        {
            Debug.LogError("해당 현상은 Solution을 갖지 않는 현상입니다.\n" +
                "Anomaly 해결을 위한 카운트에 문제가 되므로 hasSolution을 true로 변경해야 합니다.");
            return;
        }
        from.ProblemSolved();
        PhenomenonEnd();
    }
    
    /// <summary>
    /// 해당 현상이 시작될 때 해야할 처리<br/>
    /// (예: 카메라의 FOV값을 조절하기, 코루틴 실행 등)
    /// </summary>
    protected abstract void PhenomenonStart();
    /// <summary>
    /// 해당 현상이 종료될 때 해야할 처리<br/>
    /// (예: 카메라를 원래대로 돌려놓기, 코루틴 종료 등)<br/>
    /// <b>※ hasSolution이 true일 경우 해당 현상이 해결됨에 따라 실행되며<br/>
    /// false일 경우 자신을 사용한 Anomaly가 종료될 때 실행된다.</b>
    /// </summary>
    protected abstract void PhenomenonEnd();
    
}
