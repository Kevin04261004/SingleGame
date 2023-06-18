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
        [Tooltip("Interactable tag�� ������Ʈ�� ��ȣ�ۿ뿡 ����ϴ� �Է�")] interact,
        [Tooltip("�޴�� CCTV UI�� (��)Ȱ��ȭ �ϴ� �Է�")] toggle_portableCCTV,
        [Tooltip("��Ģ�� UI�� (��)Ȱ��ȭ �ϴ� �Է�")] toggle_ruleBook,
        [Tooltip("�������� (��)Ȱ��ȭ �ϴ� �Է�")] toggle_flashLight,
        [Tooltip("�Ͻ� ���� UI�� (��)Ȱ��ȭ �ϴ� �Է�")] toggle_pauseMenu,
        [Tooltip("����׿� �ؽ�Ʈ UI�� (��)Ȱ��ȭ �ϴ� �Է�")] toggle_debug_text,
        [Tooltip("Dialogue�� �������� ��, �������� ���� ������ ��� ���� ������ ����ϱ� ���� �Է�")] progress_dialogue
    }
    /// <summary>
    /// Ű ����, ������ ����, ����� ����
    /// </summary>
    public enum InputType
    {
        [Tooltip("Ű�� ������ ����")] down,
        [Tooltip("Down�� Up������ ������ �ִ� ����")] hold,
        [Tooltip("Ű�� ���� ����")] up
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
        //Dictionary<KeyCode, KeyType> keyCodeToKeyType = new Dictionary<KeyCode, KeyType>(16);
        Dictionary<KeyCode, List<KeyType>> keyCodeToKeyType = new Dictionary<KeyCode, List<KeyType>>(16);

        protected override void Awake()
        {
            base.Awake();
            List<KeyCode> kcList = new List<KeyCode>(16);
            for (int i = 0; i < keybinds.Length; i++)
            {
                if (keyCodeToKeyType.ContainsKey(keybinds[i].keyCode))
                {
                    keyCodeToKeyType[keybinds[i].keyCode].Add(keybinds[i].keyType);
                }
                else
                {
                    kcList.Add(keybinds[i].keyCode);

                    List<KeyType> ktList = new List<KeyType>(4);
                    ktList.Add(keybinds[i].keyType);
                    keyCodeToKeyType.Add(keybinds[i].keyCode, ktList);
                }
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
            List<KeyType> ktList = keyCodeToKeyType[keyCode];

            //for (int i = 0; i < ktList.Count; i++)
            //{
            //    if (Input.GetKeyDown(keyCode)) event_keyInput?.Invoke(ktList[i], InputType.down);
            //    if (Input.GetKey(keyCode)) event_keyInput?.Invoke(ktList[i], InputType.hold);
            //    if (Input.GetKeyUp(keyCode)) event_keyInput?.Invoke(ktList[i], InputType.up);
            //}
            if (Input.GetKeyDown(keyCode))
            {
                for (int i = 0; i < ktList.Count; i++)
                {
                    event_keyInput?.Invoke(ktList[i], InputType.down);
                }
            }
            if (Input.GetKey(keyCode))
            {
                for (int i = 0; i < ktList.Count; i++)
                {
                    event_keyInput?.Invoke(ktList[i], InputType.hold);
                }
            }
            if (Input.GetKeyUp(keyCode))
            {
                for (int i = 0; i < ktList.Count; i++)
                {
                    event_keyInput?.Invoke(ktList[i], InputType.up);
                }
            }
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
                else if (keyType == KeyType.toggle_debug_text)
                {
                    UIManager.instance.Toggle_Debug_Text();
                }
            }
        }
    }

}
