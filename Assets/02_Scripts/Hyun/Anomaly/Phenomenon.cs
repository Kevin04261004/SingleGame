using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("Dialogue�� ��üȭ �Կ� ���� �� �̻� ����� �ʿ䰡 ����", true)]
public interface IDialogue
{
    public void Fixed();
}
/// <summary>
/// Anomaly�� �߻��ʿ� ���� �߻��ϴ� �����<br/>
/// '�ذ�' ������ �ɷ������� �ش� ������Ʈ�� �ذ� �� �ڽ��� ������Ų Anomaly�� ���� ī��Ʈ�� ���ҽ�Ű�� �Լ��� ȣ���Ѵ�.<br/>
/// �ذ��� �� ������ '����', �ذ��� �� ������ '����'��� Ī�Ѵ�.
/// </summary>
[System.Serializable]
public abstract class Phenomenon : MonoBehaviour
{
    /// <summary>�ڽ��� �Ҽӵ�(�ڽ��� ������) Anomaly�� �ν��Ͻ�</summary>
    protected Anomaly from = null;
    /// <summary>
    /// �ش� ������ �ذ��� �� �ִ� ������ ��� true<br/>
    /// true�� ��� Anomaly�� �ش� ������ �ʱ�ȭ�ϴ� �������� ���� ī��Ʈ�� ������Ų��.
    /// </summary>
    public bool hasSolution = false;
    /// <summary>
    /// �ڽ��� ���(���� �Ǵ� ã��)�ϴ� Anomaly�� ȣ���Ѵ�.
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
    /// �ش� ������ �ذ����� ���� ������(hasSolution == false) �� �������� �����ص� ���� ����.<br/>
    /// <b>�ذ� ����� ���� ��� �ݵ�� �����ؾ� �ϸ� ������ �ذ��ߴٸ� TryFixThisPhenomenon�� ȣ���� �ش� ������ �ذ������ �˷��� �Ѵ�.</b>
    /// </summary>
    protected abstract void Solution();
    protected void TryFixThisPhenomenon()
    {
        if (!hasSolution)
        {
            Debug.LogError("�ش� ������ Solution�� ���� �ʴ� �����Դϴ�.\n" +
                "Anomaly �ذ��� ���� ī��Ʈ�� ������ �ǹǷ� hasSolution�� true�� �����ؾ� �մϴ�.");
            return;
        }
        from.ProblemSolved();
        PhenomenonEnd();
    }
    
    /// <summary>
    /// �ش� ������ ���۵� �� �ؾ��� ó��<br/>
    /// (��: ī�޶��� FOV���� �����ϱ�, �ڷ�ƾ ���� ��)
    /// </summary>
    protected abstract void PhenomenonStart();
    /// <summary>
    /// �ش� ������ ����� �� �ؾ��� ó��<br/>
    /// (��: ī�޶� ������� ��������, �ڷ�ƾ ���� ��)<br/>
    /// <b>�� hasSolution�� true�� ��� �ش� ������ �ذ�ʿ� ���� ����Ǹ�<br/>
    /// false�� ��� �ڽ��� ����� Anomaly�� ����� �� ����ȴ�.</b>
    /// </summary>
    protected abstract void PhenomenonEnd();
    
}
