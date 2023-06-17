using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

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
        public void SetCallbacksToAllSelections(ButtonClicked callback)
        {
            for (int i = 0; i < summaries.Length; i++)
            {
                summaries[i].TrySetCallbackToSelections_returnValue(callback);
            }
        }

        [System.Serializable]
        public class Summary
        {
            public string context = "Default Summary Context";
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
            readIndex = -1;
            current_reading.SetCallbacksToAllSelections(Callback_ButtonClicked);
            TryReadNextSummary();
        }
        void StopReadingDialogue()
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMovementController>().PreventMovement_SubtractStack();
            current_reading = null;
            UIManager.instance.SetActiveDialogueSummaryUI(false);
        }
        /// <summary>
        /// 
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
        private void Update()
        {
#warning temp
            if (Input.GetKeyDown(KeyCode.K))
            {
                TryReadNextSummary();
            }
        }

        /// <summary>
        /// 현재 읽고있는 Summaries에서 다음으로 넘어갈 때 사용<br/>
        /// (예: Mouse0 입력 시 해당 함수를 호출하여 다음 내용을 진행한다.)
        /// </summary>
        public bool TryReadNextSummary()
        {
            if (current_reading == null)
            {
                return false;
            }
            else if (++readIndex < current_reading.summaries.Length)
            {
                UIManager.instance.RequestTypingDialogueSummary(current_reading.summaries[readIndex].context, 0.17f, (current_reading.summaries[readIndex].TryGetSelections() != null ? Callback_ShowButtons : null));
                return true;
            }
            else
            {
                StopReadingDialogue();
                return false;
            }
        }
        void Callback_ButtonClicked(ValueWhenClicked valueWhenClicked)
        {
            if (valueWhenClicked == ValueWhenClicked.StopRead)
            {
                StopReadingDialogue();
            }
            else
            {
                TryReadNextSummary();
            }
        }
        /// <summary>
        /// 텍스트 출력이 끝나면 버튼들의 출력을 요청하기 위한 함수
        /// </summary>
        void Callback_ShowButtons()
        {
            UIManager.instance.RequestCreateSelectionButtons(current_reading.summaries[readIndex]);
        }
    }
}
