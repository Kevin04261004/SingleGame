using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_4_Key : MonoBehaviour
{
    public delegate void KeyPulled();
    /// <summary>���踦 ��װ� ���� ���� ���� ��</summary>
    public event KeyPulled event_keyPulled;

    public void Invoke_EventKeyPulled() => event_keyPulled?.Invoke();
}
