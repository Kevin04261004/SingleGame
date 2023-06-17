using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using InputSystem;

namespace DialogueSystem
{
    [System.Serializable]
    public class DialogueData
    {
        public string name;
        public string content;
        public string btn1;
        public bool btn1_true;
        public string btn2;
        public bool btn2_true;
        public string btn3;
        public bool btn3_true;
    }
    /// <summary>
    /// 버튼을 눌렀을 때 실행할 행위들의 집합
    /// </summary>
    public enum ValueWhenClicked
    {
        [Tooltip("다음 Summary로 자동 진행된다.")]
        NextSummary,
        [Tooltip("")]
        False,
        [Tooltip("")]
        True,
        [Tooltip("남아있는 Summary에 상관 없이 종료한다.")]
        StopRead
    }
    /// <summary>
    /// 사용이 필요한 곳에서는 필드로 해당 클래스를 지정해 인스펙터에서 작성<br/>
    /// 버튼을 누를 때 Return값에 따른 판별이 필요할 경우, 스크립트 내에서 Callback을 지정하는 함수를 사용<br/>
    /// (예: True버튼이 눌렸을 때 Dialogue를 중지시키고 싶을 경우, 해당 처리를 작성한 함수를 콜백으로 지정)
    /// </summary>
    [System.Serializable]
    public class H_DialogueData
    {
        public Summary[] summaries;
        public delegate void ButtonClicked(ValueWhenClicked value);

        // SetCallbacksToAllSelections함수로 콜백 추가하는거를
        // H_Dialogue를 쓸 때(읽기 시작, 읽기 종료)마다 추가해줘서
        // delegate에 여러 번 들어가는 문제가 발생함
        // [DiaManager.Callback_ButtonClicked,
        // UIManager.Callback_DisableButtons 가 중복으로 들어감]
        //
        // 다만 위 오류는 '같은 인스턴스'를 사용할 때만 발생하므로
        // 해결하면 사라지는 Anomaly에는 문제가 없음
        public void AddCallbacksToAllSelections(ButtonClicked callback)
        {
            for (int i = 0; i < summaries.Length; i++)
            {
                summaries[i].TrySetCallbackToSelections_returnValue(callback);
            }
        }

        [System.Serializable]
        public class Summary
        {
            [Tooltip("출력할 이름")]
            public string speaker = "Default";
            [Tooltip("출력될 내용")]
            public string context = "Default Summary Context";
            [Tooltip("내용 간 출력 딜레이\n(0: 즉시 모든 텍스트를 출력)")]
            [Min(0.00f)] public float printDel = 0.09f;
            public bool TrySetCallbackToSelections_returnValue(ButtonClicked callback)
            {
                if (selections == null || selections.Count == 0)
                {
                    return false;
                }
                for (int i = 0; i < selections.Count; i++)
                {
                    selections[i].callback_buttonClicked += callback;
                }
                return true;
            }
            [SerializeField] List<Selection> selections = null;
            public Selection[] TryGetSelections()
            {
                if (selections == null || selections.Count == 0) return null;
                return selections.ToArray();
            }
            [System.Serializable]
            public class Selection
            {
                public string context = "Default Selection Context";
                /// <summary>
                /// 버튼이 눌리면 event가 실행된다.
                /// </summary>
                public event ButtonClicked callback_buttonClicked = null;
                [Tooltip("버튼이 클릭되면 Callback으로 등록된 함수들의 매개변수로 전달될 값.\nCallback에 의해 호출되더라도 값에 대한 처리를 따로 해주지 않으면 값 자체는 의미가 없다.")]

                public ValueWhenClicked valueWhenClicked = ValueWhenClicked.NextSummary;
                public void WhenButtonClicked()
                {
                    callback_buttonClicked?.Invoke(valueWhenClicked);
                }
            }
        }

    }

    public class DialogueManager : Singleton<DialogueManager>
    {
        //public float typeSpeed;
        //public string content;
        //public string char_name;
        //public string Btn_01;
        //public bool btn_01_true;
        //public string Btn_02;
        //public bool btn_02_true;
        //public string Btn_03;
        //public bool btn_03_true;
        //public void StartReadDialogue(DialogueData data)
        //{
        //    char_name = data.name;
        //    content = data.content;
        //    Btn_01 = data.btn1;
        //    btn_01_true = data.btn1_true;
        //    Btn_02 = data.btn2;
        //    btn_02_true = data.btn2_true;
        //    Btn_03 = data.btn3;
        //    btn_03_true = data.btn3_true;
        //    StartCoroutine(Reader());
        //}
        //IEnumerator Reader()
        //{
        //    int i;
        //    for (i = 1; i < content.Length; i++)
        //    {
        //        UIManager.instance.Set_DialogueText_Change(char_name, content, i + 1);
        //        yield return new WaitForSeconds(typeSpeed);
        //    }
        //    UIManager.instance.Set_Buttons_Bool(true, Btn_01, btn_01_true, Btn_02, btn_02_true, Btn_03, btn_03_true);
        //}
        private H_DialogueData current_reading = null;
        private int readIndex = -1;
        public bool isReadingSomething => current_reading != null;

        private void Start()
        {
            InputManager.instance.event_keyInput += GetInput;
        }

        /// <summary>
        /// 현재 읽고있던 Summaries를 중단.<br/>
        /// 다음 Summary를 읽지 말아야 할 때 사용할 수 있다.<br/>
        /// (예: 읽던 중 False인 선택지를 선택했다면 해당 함수를 호출하여 Summary를 도중에 끊을 수 있다.)
        /// </summary>
        public void TryStopReadingSummaries()
        {
            if (!isReadingSomething)
            {
                Debug.Log("무언가를 읽고있지 않아 중지할 수 없습니다.");
                return;
            }
            StopReadingDialogue();
            Debug.Log("읽고있던 Dialogue를 중지시켰습니다.");
        }
        void StartReadingDialogue(H_DialogueData data)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementController>().PreventMovement_AddStack();
            current_reading = data;
            UIManager.instance.SetActiveDialogueSummaryUI(true);
            readIndex = 0;
            current_reading.AddCallbacksToAllSelections(Callback_ButtonClicked);
            UIManager.instance.RequestTypingDialogueSummary(current_reading.summaries[readIndex].speaker, current_reading.summaries[readIndex].context, current_reading.summaries[readIndex].printDel, (current_reading.summaries[readIndex].TryGetSelections() != null ? Callback_ShowButtons : null));
        }
        void StopReadingDialogue()
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementController>().PreventMovement_SubtractStack();
            current_reading = null;
            UIManager.instance.SetActiveDialogueSummaryUI(false);
        }
        /// <summary>
        /// Dialogue창이 활성화될 땐 플레이어를 움직이지 못하게 하고, 마우스를 노출시킨다.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="force">true: 현재 읽고있는 다른 Dialogue가 있다면 덮어씌워서 실행.<br/>
        /// 기존 Dialogue가 끝까지 진행되어야 하는 경우 오류가 발생할 수 있으므로 사용에 주의.</param>
        /// <returns></returns>
        public bool Try_RequestStartReadingDialogueData(H_DialogueData data, bool force = false)
        {
            if (isReadingSomething)
            {
                if (force)
                {
                    Debug.LogWarning("DialogueManager가 이미 무언가를 읽고 있었으나 강제로 교체하였습니다.");
                    StartReadingDialogue(data);
                    return true;
                }
                else
                {
                    Debug.LogWarning("DialogueManager가 이미 무언가를 읽고 있습니다.");
                    return false;
                }

            }
            StartReadingDialogue(data);
            return true;
        }

        /// <summary>
        /// 현재 읽고있는 Summaries에서 다음으로 넘어갈 때 사용<br/>
        /// (예: Mouse0 입력 시 해당 함수를 호출하여 다음 내용을 진행한다.)
        /// </summary>
        /// <param name="ignoreSelectionButton">true: 선택지에 의해 넘어가는 Summary를 진행중이더라도 강제로 다음으로 넘김<br/>
        /// <b>※ 현재 강제로 넘길 경우 선택지 버튼이 사라지는 처리가 없으므로 가급적 사용 금지</b></param>
        public bool TryReadNextSummary(bool ignoreSelectionButton = false)
        {
            Debug.Log($"READ: {readIndex}");
            if (current_reading == null)
            {
                Debug.LogWarning("현재 읽고있는 로그가 없어");
                return false;
            }
            if (readIndex < current_reading.summaries.Length - 1)
            {
                if (current_reading.summaries[readIndex].TryGetSelections() != null && !ignoreSelectionButton)
                {
                    Debug.LogWarning("지금 보고있는 Summary의 선택지가 NULL이 아니고 강제도 아님");
                    return false;
                }
                else
                {
                    Debug.LogWarning("보고있는 Summary의 선택지가 NULL이라 지나감");
                    ++readIndex;
                    UIManager.instance.RequestTypingDialogueSummary(current_reading.summaries[readIndex].speaker, current_reading.summaries[readIndex].context, current_reading.summaries[readIndex].printDel, (current_reading.summaries[readIndex].TryGetSelections() != null ? Callback_ShowButtons : null));
                    return true;
                }
            }
            else
            {
                if (current_reading.summaries[readIndex].TryGetSelections() == null)
                {
                    Debug.LogWarning("마지막까지 읽었음");
                    TryStopReadingSummaries();
                    return false;
                }
                else if (ignoreSelectionButton)
                {
                    Debug.LogWarning("마지막선택지를 선택함");
                    TryStopReadingSummaries();
                    return false;
                }
                return true;
            }
        }
        void Callback_ButtonClicked(ValueWhenClicked valueWhenClicked)
        {
            if (valueWhenClicked == ValueWhenClicked.StopRead)
            {
                TryStopReadingSummaries();
            }
            else
            {
                TryReadNextSummary(true);
            }
        }
        /// <summary>
        /// 텍스트 출력이 끝나면 버튼들의 출력을 요청하기 위한 함수
        /// </summary>
        void Callback_ShowButtons()
        {
            UIManager.instance.RequestCreateSelectionButtons(current_reading.summaries[readIndex]);
        }
        void GetInput(KeyType keyType, InputType inputType)
        {
            if (keyType == KeyType.progress_dialogue && inputType == InputType.down)
            {
                Debug.Log("다이얼로그 넘기는 키 입력박음");
                if (isReadingSomething)
                {
                    TryReadNextSummary();
                }
            }
        }
    }
}
