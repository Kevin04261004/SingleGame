using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteratable
{
    public abstract void Interact();
    /// <summary>
    /// ���� ��ȣ�ۿ��� ������ �������� �Ǻ��Ѵ�.<br/>
    /// ���� ���, ���� ������ ������ ���߿���(isOpening, isClosing) �ش� �Լ��� ��ȯ���� false�� ��ȯ�Ͽ�<br/>
    /// UI�� ���� ���� ���� �ʵ��� ������ �� �� �ִ�.
    /// </summary>
    public virtual bool IsInteractable() => true;
}
