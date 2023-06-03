using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteratable
{
    public abstract void Interact();
    /// <summary>
    /// 현재 상호작용이 가능한 상태인지 판별한다.<br/>
    /// 예를 들어, 문이 열리고 닫히는 도중에는(isOpening, isClosing) 해당 함수의 반환값을 false로 반환하여<br/>
    /// UI가 붉은 점이 되지 않도록 조건을 걸 수 있다.
    /// </summary>
    public virtual bool IsInteractable() => true;
}
