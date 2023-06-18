using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    /// <summary>
    /// 입력 키가 사용되는 타입
    /// </summary>
    public enum KeyType
    {
        [Tooltip("Interactable tag의 오브젝트와 상호작용에 사용하는 입력")] interact,
        [Tooltip("휴대용 CCTV UI를 (비)활성화 하는 입력")] toggle_portableCCTV,
        [Tooltip("규칙서 UI를 (비)활성화 하는 입력")] toggle_ruleBook,
        [Tooltip("손전등을 (비)활성화 하는 입력")] toggle_flashLight,
        [Tooltip("일시 정지 UI를 (비)활성화 하는 입력")] toggle_pauseMenu,
        [Tooltip("디버그용 텍스트 UI를 (비)활성화 하는 입력")] toggle_debug_text,
        [Tooltip("Dialogue가 진행중일 때, 선택지가 없는 내용의 경우 다음 내용을 출력하기 위한 입력")] progress_dialogue
    }
    /// <summary>
    /// 키 누름, 누르고 있음, 떼어냄의 구분
    /// </summary>
    public enum InputType
    {
        [Tooltip("키를 누르는 순간")] down,
        [Tooltip("Down과 Up사이의 누르고 있는 순간")] hold,
        [Tooltip("키를 떼는 순간")] up
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

            Debug.Log($"입력받을 키 타입의 종류: {keyCodeToKeyType.Count}종류 | 입력받을 키: {needToGetInput.Length}개");

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
