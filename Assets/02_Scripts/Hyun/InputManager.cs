using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    /// <summary>
    /// �Է� Ű�� ���Ǵ� Ÿ��
    /// </summary>
    public enum KeyType
    {
        interact, toggle_portableCCTV, toggle_ruleBook, toggle_flashLight, toggle_pauseMenu
    }
    /// <summary>
    /// Ű ����, ������ ����, ����� ����
    /// </summary>
    public enum InputType
    {
        down, hold, up
    }

    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] BindKey[] keybinds = null;
        [System.Serializable]
        public class BindKey
        {
            public KeyType keyType;
            public KeyCode keyCode;
        }
        
        public delegate void KeyInput(KeyType keyType, InputType inputType);
        /// <summary>void (KeyType, InputType)</summary>
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
            needToGetInput = kcList.ToArray();

            Debug.Log($"�Է¹��� Ű Ÿ���� ����: {keyCodeToKeyType.Count}���� | �Է¹��� Ű: {needToGetInput.Length}��");

            event_keyInput += GetInputs;
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

        void GetInputs(KeyType keyType, InputType inputType)
        {
            if (inputType == InputType.down)
            {
                if (keyType == KeyType.toggle_portableCCTV)
                {
                    UIManager.instance.Set_CCTV_BackGround_TrueOrFalse();
                }
                else if (keyType == KeyType.toggle_ruleBook)
                {
                    UIManager.instance.Set_RuleBook_BackGround_TrueOrFalse();
                }
            }
        }
    }

}
