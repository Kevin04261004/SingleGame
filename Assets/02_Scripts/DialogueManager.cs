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
            [Tooltip("����� �̸�")]
            public string speaker = "Default";
            [Tooltip("��µ� ����")]
            public string context = "Default Summary Context";
            [Tooltip("���� �� ��� ������\n(0: ��� ��� �ؽ�Ʈ�� ���)")]
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
                /// ��ư�� ������ event�� ����ȴ�.
                /// </summary>
                public event ButtonClicked callback_buttonClicked = null;
                [Tooltip("��ư�� Ŭ���Ǹ� Callback���� ��ϵ� �Լ����� �Ű������� ���޵� ��.\nCallback�� ���� ȣ��Ǵ��� ���� ���� ó���� ���� ������ ������ �� ��ü�� �ǹ̰� ����.")]

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
        /// Dialogueâ�� Ȱ��ȭ�� �� �÷��̾ �������� ���ϰ� �ϰ�, ���콺�� �����Ų��.
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

        /// <summary>
        /// ���� �а��ִ� Summaries���� �������� �Ѿ �� ���<br/>
        /// (��: Mouse0 �Է� �� �ش� �Լ��� ȣ���Ͽ� ���� ������ �����Ѵ�.)
        /// </summary>
        /// <param name="ignoreSelectionButton">true: �������� ���� �Ѿ�� Summary�� �������̴��� ������ �������� �ѱ�<br/>
        /// <b>�� ���� ������ �ѱ� ��� ������ ��ư�� ������� ó���� �����Ƿ� ������ ��� ����</b></param>
        public bool TryReadNextSummary(bool ignoreSelectionButton = false)
        {
            Debug.Log($"READ: {readIndex}");
            if (current_reading == null)
            {
                Debug.LogWarning("���� �а��ִ� �αװ� ����");
                return false;
            }
            if (readIndex < current_reading.summaries.Length - 1)
            {
                if (current_reading.summaries[readIndex].TryGetSelections() != null && !ignoreSelectionButton)
                {
                    Debug.LogWarning("���� �����ִ� Summary�� �������� NULL�� �ƴϰ� ������ �ƴ�");
                    return false;
                }
                else
                {
                    Debug.LogWarning("�����ִ� Summary�� �������� NULL�̶� ������");
                    ++readIndex;
                    UIManager.instance.RequestTypingDialogueSummary(current_reading.summaries[readIndex].speaker, current_reading.summaries[readIndex].context, current_reading.summaries[readIndex].printDel, (current_reading.summaries[readIndex].TryGetSelections() != null ? Callback_ShowButtons : null));
                    return true;
                }
            }
            else
            {
                if (current_reading.summaries[readIndex].TryGetSelections() == null)
                {
                    Debug.LogWarning("���������� �о���");
                    TryStopReadingSummaries();
                    return false;
                }
                else if (ignoreSelectionButton)
                {
                    Debug.LogWarning("�������������� ������");
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
        /// �ؽ�Ʈ ����� ������ ��ư���� ����� ��û�ϱ� ���� �Լ�
        /// </summary>
        void Callback_ShowButtons()
        {
            UIManager.instance.RequestCreateSelectionButtons(current_reading.summaries[readIndex]);
        }
        void GetInput(KeyType keyType, InputType inputType)
        {
            if (keyType == KeyType.progress_dialogue && inputType == InputType.down)
            {
                Debug.Log("���̾�α� �ѱ�� Ű �Է¹���");
                if (isReadingSomething)
                {
                    TryReadNextSummary();
                }
            }
        }
    }
}
