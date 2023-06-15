using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_4_Key : MonoBehaviour
{
    public delegate void KeyPulled();
    /// <summary>열쇠를 잠그고 빼는 동작 종료 후</summary>
    public KeyPulled event_keyPulled;
    public void SetKeyPullEvent(KeyPulled keyPulled)
    {
        event_keyPulled = keyPulled;
    }
    public void Invoke_EventKeyPulled() => event_keyPulled?.Invoke();
    public void Invoke_ClickedSound() => GetComponent<AudioSource>().Play();
}
