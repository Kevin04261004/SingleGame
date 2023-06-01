using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Anomaly가 발생됨에 따라 발생하는 현상들
/// '해결' 조건이 걸려있으며 해당 오브젝트를 해결 시 자신을 발현시킨 Anomaly의 제거 카운트를 감소시킨다.
/// 
/// 상속받은 클래스에서 해결 조건을 적절하게 걸고 Resolve를 실행시키는 작업이 필요함.
/// 상속받은 클래스의 이름은 AnoObj_* 형식을 따름
/// </summary>
[System.Serializable, System.Obsolete]
public class Phenomenon
{
    Anomaly from = null;
    public bool hasSolution = false;
    /// <summary>
    /// 자신을 생성시킨 Anomaly의 Instance를 초기화
    /// </summary>
    public virtual void Init(Anomaly anomalyInstance)
    {
        from = anomalyInstance;
    }
    /// <summary>
    /// 해당 오브젝트가 해결될 경우 
    /// </summary>
    public void Resolve()
    {
        
    }
}

public abstract class Solution
{

}
public class Solution_StayingArea : Solution
{

}
