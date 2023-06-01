using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Anomaly�� �߻��ʿ� ���� ������ ������Ʈ��
/// '�ذ�' ������ �ɷ������� �ش� ������Ʈ�� �ذ� �� �ڽ��� ������Ų Anomaly�� ���� ī��Ʈ�� ���ҽ�Ų��.
/// 
/// ��ӹ��� Ŭ�������� �ذ� ������ �����ϰ� �ɰ� Resolve�� �����Ű�� �۾��� �ʿ���.
/// ��ӹ��� Ŭ������ �̸��� AnoObj_* ������ ����
/// </summary>
[System.Serializable]
public abstract class AnomalyObject : MonoBehaviour
{
    Anomaly from = null;
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
        from.Child_Resolved();
    }
}
