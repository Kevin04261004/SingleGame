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
    /// ��ư�� ������ �� ������ �������� ����
    /// </summary>
    public enum ValueWhenClicked
    {
        [Tooltip("���� Summary�� �ڵ� ����ȴ�.")]
        NextSummary,
        [Tooltip("")]
        False,
        [Tooltip("")]
        True,
        [Tooltip("�����ִ� Summary�� ��� ���� �����Ѵ�.")]
        StopRead
    }
    /// <summary>
    /// ����� �ʿ��� �������� �ʵ�� �ش� Ŭ������ ������ �ν����Ϳ��� �ۼ�<br/>
    /// ��ư�� ���� �� Return���� ���� �Ǻ��� �ʿ��� ���, ��ũ��Ʈ ������ Callback�� �����ϴ� �Լ��� ���<br/>
    /// (��: True��ư�� ������ �� Dialogue�� ������Ű�� ���� ���, �ش� ó���� �ۼ��� �Լ��� �ݹ����� ����)
    /// </summary>
    [System.Serializable]
    public class H_DialogueData
    {
        public Summary[] summaries;
        public delegate void ButtonClicked(ValueWhenClicked value);

        // SetCallbacksToAllSelections�Լ��� �ݹ� �߰��ϴ°Ÿ�
        // H_Dialogue�� �� ��(�б� ����, �б� ����)���� �߰����༭
        // delegate�� ���� �� ���� ������ �߻���
        // [DiaManager.Callback_ButtonClicked,
        // UIManager.Callback_DisableButtons �� �ߺ����� ��]
        //
        // �ٸ� �� ������ '���� �ν��Ͻ�'�� ����� ���� �߻��ϹǷ�
        // �ذ��ϸ� ������� Anomaly���� ������ ����
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
                /// ��ư�� ������ event�� ����ȴ�.
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
        /// ���� �а��ִ� Summaries�� �ߴ�.<br/>
        /// ���� Summary�� ���� ���ƾ� �� �� ����� �� �ִ�.<br/>
        /// (��: �д� �� False�� �������� �����ߴٸ� �ش� �Լ��� ȣ���Ͽ� Summary�� ���߿� ���� �� �ִ�.)
        /// </summary>
        public void TryStopReadingSummaries()
        {
            if (!isReadingSomething)
            {
                Debug.Log("���𰡸� �а����� �ʾ� ������ �� �����ϴ�.");
                return;
            }
            StopReadingDialogue();
            Debug.Log("�а��ִ� Dialogue�� �������׽��ϴ�.");
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
        /// <param name="force">true: ���� �а��ִ� �ٸ� Dialogue�� �ִٸ� ������� ����.<br/>
        /// ���� Dialogue�� ������ ����Ǿ�� �ϴ� ��� ������ �߻��� �� �����Ƿ� ��뿡 ����.</param>
        /// <returns></returns>
        public bool Try_RequestStartReadingDialogueData(H_DialogueData data, bool force = false)
        {
            if (isReadingSomething)
            {
                if (force)
                {
                    Debug.LogWarning("DialogueManager�� �̹� ���𰡸� �а� �־����� ������ ��ü�Ͽ����ϴ�.");
                    StartReadingDialogue(data);
                    return true;
                }
                else
                {
                    Debug.LogWarning("DialogueManager�� �̹� ���𰡸� �а� �ֽ��ϴ�.");
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
        /// ���� �а��ִ� Summaries���� �������� �Ѿ �� ���<br/>
        /// (��: Mouse0 �Է� �� �ش� �Լ��� ȣ���Ͽ� ���� ������ �����Ѵ�.)
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
        /// �ؽ�Ʈ ����� ������ ��ư���� ����� ��û�ϱ� ���� �Լ�
        /// </summary>
        void Callback_ShowButtons()
        {
            UIManager.instance.RequestCreateSelectionButtons(current_reading.summaries[readIndex]);
        }
    }
}
