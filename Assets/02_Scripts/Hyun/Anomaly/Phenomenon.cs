using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Anomaly�� �߻��ʿ� ���� �߻��ϴ� �����
/// '�ذ�' ������ �ɷ������� �ش� ������Ʈ�� �ذ� �� �ڽ��� ������Ų Anomaly�� ���� ī��Ʈ�� ���ҽ�Ų��.
/// 
/// ��ӹ��� Ŭ�������� �ذ� ������ �����ϰ� �ɰ� Resolve�� �����Ű�� �۾��� �ʿ���.
/// ��ӹ��� Ŭ������ �̸��� AnoObj_* ������ ����
/// </summary>
[System.Serializable, System.Obsolete]
public class Phenomenon
{
    Anomaly from = null;
    public bool hasSolution = false;
    /// <summary>
    /// �ڽ��� ������Ų Anomaly�� Instance�� �ʱ�ȭ
    /// </summary>
    public virtual void Init(Anomaly anomalyInstance)
    {
        from = anomalyInstance;
    }
    /// <summary>
    /// �ش� ������Ʈ�� �ذ�� ��� 
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
