using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Anomaly�� �߻��ʿ� ���� �߻��ϴ� �����<br/>
/// '�ذ�' ������ �ɷ������� �ش� ������Ʈ�� �ذ� �� �ڽ��� ������Ų Anomaly�� ���� ī��Ʈ�� ���ҽ�Ű�� �Լ��� ȣ���Ѵ�.<br/>
/// �ذ��� �� ������ '����', �ذ��� �� ������ '����'��� Ī�Ѵ�.
/// </summary>
[System.Serializable]
public abstract class Phenomenon : MonoBehaviour
{
    /// <summary>�ڽ��� �Ҽӵ�(�ڽ��� ������) Anomaly�� �ν��Ͻ�</summary>
    Anomaly from = null;
    /// <summary>
    /// �ش� ������ �ذ��� �� �ִ� ������ ��� true<br/>
    /// true�� ��� Anomaly�� �ش� ������ �ʱ�ȭ�ϴ� ��������
    /// ���� ī��Ʈ�� ������Ų��.
    /// </summary>
    public bool hasSolution = false;

    private void OnDestroy()
    {
        PhenomenonEnd();
    }
    /// <summary>
    /// �ڽ��� ������Ų Anomaly�� ȣ���Ѵ�.
    /// </summary>
    public void Init(Anomaly anomalyInstance)
    {
        from = anomalyInstance;
        PhenomenonStart();
    }
    /// <summary>
    /// �ش� ������ �ذ����� ���� ������(hasSolution == false) �� �������� �����ص� ���� ����.<br/>
    /// �ƴ� ��� �ݵ�� �����ؾ� �ϸ� ������ �ذ��ߴٸ� TryFixThisPhenomenon�� ȣ���� �ش� ������ ������ �Ѵ�.<br/>
    /// TODO<br/>
    /// hasSolution���η� �Լ��� ���� ���ΰ� �������µ� �̰� �ٶ������� �����غ� �ʿ䰡 �ִ�.
    /// Ŭ������ ����, ������ �����簡 �ϴ� ����� ã�ƺ� �� ����
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
        from.FixProblem();
    }
    
    /// <summary>
    /// �ش� ������ ���۵� �� �ؾ��� ó��<br/>
    /// (��: ī�޶��� FOV���� �����ϱ�, �ڷ�ƾ ���� ��)
    /// </summary>
    protected abstract void PhenomenonStart();
    /// <summary>
    /// �ش� ������ ����� �� �ؾ��� ó��<br/>
    /// (��: ī�޶� ������� ��������, �ڷ�ƾ ���� ��)
    /// </summary>
    protected abstract void PhenomenonEnd();
    
}
