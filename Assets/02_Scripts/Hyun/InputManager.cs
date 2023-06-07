using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] BindKey[] keybinds = null;
    [System.Serializable]
    public class BindKey
    {
        public KeyType keyType;
        public KeyCode keyCode;
    }
    public enum KeyType
    {
        interact, toggle_portableCCTV
    }
    public enum InputType
    {
        down, hold, up
    }
    public delegate void KeyInput(KeyType keyType, InputType inputType);
    public event KeyInput event_keyInput;

    KeyCode[] needToGetInput;
    Dictionary<KeyCode, KeyType> keyCodeToKeyType = new Dictionary<KeyCode, KeyType>(16);

    protected override void Awake()
    {
        base.Awake();
        List<KeyCode> kcList = new List<KeyCode>(16);
        for (int i = 0; i < keybinds.Length; i++)
        {
            kcList.Add(keybinds[i].keyCode);
            keyCodeToKeyType.Add(keybinds[i].keyCode, keybinds[i].keyType);
        }
    }

    private void Update()
    {
        for (int i = 0; i < needToGetInput.Length; i++)
        {
            GetInputTypes(needToGetInput[i]);
        }
    }

    void GetInputTypes(KeyCode keyCode)
    {
        KeyType kt = keyCodeToKeyType[keyCode];
        if (Input.GetKeyDown(keyCode)) event_keyInput?.Invoke(kt, InputType.down);
        if (Input.GetKey(keyCode)) event_keyInput?.Invoke(kt, InputType.hold);
        if (Input.GetKeyUp(keyCode)) event_keyInput?.Invoke(kt, InputType.up);
    }
}
