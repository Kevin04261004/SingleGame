using System.Collections;
using DialogueSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton <UIManager>
{
    [SerializeField] private PlayerMovementController playerController;
    [SerializeField] private Text timeClock_text;
    [SerializeField] private Image RuleBook_BackGround;
    [SerializeField] private Image CCTV_BackGround;
    [SerializeField] private Image RuleBookIcon_Image;
    [SerializeField] private Image CCTVIcon_Image;
    [SerializeField] private Image timeClock_Image;
    [SerializeField] private Image middlePoint_Image;
    [SerializeField] private GameObject Dialogue_GameObject;
    [SerializeField] private Text content_text;
    [SerializeField] private Text name_text;
    [SerializeField] private Text[] answer_text;
    [SerializeField] private Phenomenon phenomenon;
    [SerializeField] private GameObject playerMesh_GameObject;
    // HYUN
    StageSystem.Stage stageComp = null;
    [SerializeField] LayoutGroup selectionButtonAlignment;
    [SerializeField] Button prefab_buttonForSelection;
    [SerializeField] public TMPro.TextMeshProUGUI tmp_areaName;
    [SerializeField] TextMeshProUGUI tmp_debug_text;
    // HYUN END
    [field:SerializeField] public Image Died_Image { get; private set; }

    [Tooltip("크로스헤어 기본 색상")] [SerializeField] private Color baseColor;
    [Tooltip("크로스헤어가 상호작용 가능할 때의 색상")] [SerializeField] private Color changeColor;
    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerMovementController>();
        stageComp = GameObject.FindObjectOfType<StageSystem.Stage>();
        stageComp.event_player_area_enter += Player_Area_Enter;
        tManager = GameObject.FindObjectOfType<TimeManager>();
    }
    void Player_Area_Enter(StageSystem.Area.AreaType areaType)
    {
        tmp_areaName.text = stageComp.playerPos.areaName;
    }
    public void Set_TimeClock_TMP(int time)
    {
        int hour = time / 60;
        int minute = time % 60;
        timeClock_text.text = string.Format("{0:D2} : {1:D2}", hour, minute);
    }
    public void Set_RuleBook_BackGround_TrueOrFalse()
    {
        if(RuleBook_BackGround.gameObject.activeSelf)
        {
            RuleBook_BackGround.gameObject.SetActive(false);
            middlePoint_Image.gameObject.SetActive(true);
            timeClock_Image.gameObject.SetActive(true);
            RuleBookIcon_Image.gameObject.SetActive(true);
            CCTVIcon_Image.gameObject.SetActive(true);
            //playerController.Set_canMove_Bool(true);
            playerController.PreventMovement_SubtractStack();
        }
        else
        {
            RuleBook_BackGround.gameObject.SetActive(true);
            middlePoint_Image.gameObject.SetActive(false);
            timeClock_Image.gameObject.SetActive(false);
            RuleBookIcon_Image.gameObject.SetActive(false);
            CCTVIcon_Image.gameObject.SetActive(false);
            //playerController.Set_canMove_Bool(false);
            playerController.PreventMovement_AddStack();
        }
    }
    public void Set_CCTV_BackGround_TrueOrFalse()
    {
        if (CCTV_BackGround.gameObject.activeSelf)
        {
            playerMesh_GameObject.SetActive(false);
            CCTV_BackGround.gameObject.SetActive(false);
            middlePoint_Image.gameObject.SetActive(true);
            timeClock_Image.gameObject.SetActive(true);
            RuleBookIcon_Image.gameObject.SetActive(true);
            CCTVIcon_Image.gameObject.SetActive(true);
            //playerController.Set_canMove_Bool(true);
            playerController.PreventMovement_SubtractStack();
        }
        else
        {
            playerMesh_GameObject.SetActive(true);
            CCTV_BackGround.gameObject.SetActive(true);
            middlePoint_Image.gameObject.SetActive(false);
            RuleBookIcon_Image.gameObject.SetActive(false);
            CCTVIcon_Image.gameObject.SetActive(false);
            //playerController.Set_canMove_Bool(false);
            playerController.PreventMovement_AddStack();
        }
    }
    public void Set_middlePoint_Image_Color(bool canInteract)
    {
        middlePoint_Image.color = (canInteract ? changeColor : baseColor);
    }
    public void Set_DialogueGameObject_Bool(bool Open)
    {
        Dialogue_GameObject.SetActive(Open);
        middlePoint_Image.gameObject.SetActive(!Open);
        RuleBookIcon_Image.gameObject.SetActive(!Open);
        CCTVIcon_Image.gameObject.SetActive(!Open);
    }
    public void Set_DialogueText_Change(string name, string content,int typeIndex = 0)
    {
        name_text.text = name;
        content_text.text = content.Substring(0, typeIndex);
    }
    public void Set_Buttons_Bool(bool Open, string str1 = null, bool btn1_true = false, string str2 = null, bool btn2_true = false, string str3 = null, bool btn3_true = false)
    {
        if(!Open)
        {
            answer_text[0].transform.parent.gameObject.SetActive(false);
            answer_text[1].transform.parent.gameObject.SetActive(false);
            answer_text[2].transform.parent.gameObject.SetActive(false);
            return;
        }
        answer_text[0].transform.parent.gameObject.SetActive(Open);
        answer_text[0].transform.parent.gameObject.GetComponent<Btn_Bool>().isAnswer = btn1_true;
        answer_text[1].transform.parent.gameObject.SetActive(Open);
        answer_text[1].transform.parent.gameObject.GetComponent<Btn_Bool>().isAnswer = btn2_true;
        answer_text[2].transform.parent.gameObject.SetActive(Open);
        answer_text[2].transform.parent.gameObject.GetComponent<Btn_Bool>().isAnswer = btn3_true;
        answer_text[0].text = str1;
        answer_text[1].text = str2;
        answer_text[2].text = str3;
    }
    //public void OnClick_Btn()
    //{
    //    if(EventSystem.current.currentSelectedGameObject.GetComponent<Btn_Bool>().isAnswer)
    //    {
    //        if (phenomenon is IDialogue)
    //        {
    //            (phenomenon as IDialogue).Fixed();
    //            //playerController.Set_canMove_Bool(true);
    //            playerController.PreventMovement_SubtractStack();
    //        }
    //    }
    //    else
    //    {
    //        GameManager.instance.Died();
    //    }
    //}
    public void Btns()
    {
        for(int i = 0; i < 3;i++)
        {
            
        }
    }
    [System.Obsolete("" ,true)]
    public void Set_Phenomenom(Phenomenon instance)
    {
        phenomenon = instance;
    }
    public void OnClick_Retry_Btn()
    {
        print(1);
        SceneManager.LoadScene("00_TitleScene");
    }
    Coroutine coroutine_typeWriter = null;
    IEnumerator TypeWriter(Text dest, string src, float typingDel, TypingTextEnded callback_typingEnded = null)
    {
        dest.text = string.Empty;
        WaitForSeconds waitDel = new WaitForSeconds(typingDel);

        System.Text.StringBuilder sb = new System.Text.StringBuilder(256);
        int cur = 0;

        while ((dest.text.Length != src.Length))
        {
            sb.Append(src[cur++]);
            dest.text = sb.ToString();
            yield return waitDel;
        }
        callback_typingEnded?.Invoke();
        coroutine_typeWriter = null;
    }
    public delegate void TypingTextEnded();
    /// <summary>
    /// 내용과 타이핑 딜레이를 매개변수로 받아, Dialogue UI에 출력시킨다.
    /// </summary>
    /// <param name="src">출력할 전체 문장</param>
    /// <param name="typingDel">각 문자를 차례대로 출력할 때, 문자 간 딜레이.<br/>0으로 지정 시 텍스트가 즉시 출력되고 콜백도 함께 호출된다.</param>
    /// <param name="callback_typingEnded">텍스트 출력이 완료되면 실행할 콜백<br/>
    /// (예: 끝까지 출력되면 선택지 버튼들을 출력한다.)
    /// </param>
    public void RequestTypingDialogueSummary(string speaker, string src, float typingDel, TypingTextEnded callback_typingEnded = null)
    {
        if (!isDialogueUIActivated)
        {
            SetActiveDialogueSummaryUI(true);
        }
        if (coroutine_typeWriter != null)
        {
            StopCoroutine(coroutine_typeWriter);
        }
        name_text.text = speaker;
        if (typingDel == float.Epsilon)
        {
            content_text.text = src;
            callback_typingEnded?.Invoke();
        }
        else
        {
            coroutine_typeWriter = StartCoroutine(TypeWriter(content_text, src, typingDel, callback_typingEnded));
        }
    }
    public bool isDialogueUIActivated => content_text.enabled;
    public void SetActiveDialogueSummaryUI(bool value)
    {
        Dialogue_GameObject.SetActive(value);
        content_text.gameObject.SetActive(value);
    }
    public void RequestCreateSelectionButtons(H_DialogueData.Summary targetSummary)
    {
        H_DialogueData.Summary.Selection[] selections = targetSummary.TryGetSelections();
        for (int i = 0; i < selections?.Length; i++)
        {
            Button btn = Instantiate(prefab_buttonForSelection);
            btn.gameObject.transform.SetParent(selectionButtonAlignment.transform);

            H_DialogueData.Summary.Selection selection = selections[i];
            selection.callback_buttonClicked += Callback_DisableButtons;
            btn.onClick.AddListener(selection.WhenButtonClicked);
            btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = selection.context;
        }
    }
    /// <summary>
    /// 버튼 중 하나를 누르면(선택 완료) 모든 버튼을 제거하는 함수
    /// </summary>
    void Callback_DisableButtons(ValueWhenClicked valueWhenClicked)
    {
        for (int i = selectionButtonAlignment.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(selectionButtonAlignment.transform.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        if (tmp_debug_text.gameObject.activeSelf)
        {
            if (AnomalyManager.instance != null)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder(128);
                sb.Append($"게임 플레이 시간: {string.Format("{0:0.00}", tManager.playTime)}sec\n");
                sb.Append($"활성화된 Anomaly 개수: {AnomalyManager.instance.effectiveAnomalys.Count}개\n");
                sb.Append($"해결한 Anomaly 개수: {AnomalyManager.instance.solvedAnomalyCount}개\n");
                sb.Append($"다음 Anomaly 생성까지: {string.Format("{0:0.00}", AnomalyManager.instance.remain_delay_of_generation_anomaly)} / {string.Format("{0:0.00}", AnomalyManager.instance.delay_of_generation_anomaly)}sec");

                tmp_debug_text.text = sb.ToString();
            }
            else
            {
                tmp_debug_text.text = string.Empty;
            }
        }
    }
    TimeManager tManager;
    public void Toggle_Debug_Text()
    {
        tmp_debug_text.gameObject.SetActive(!tmp_debug_text.gameObject.activeSelf);
        if (tmp_debug_text.gameObject.activeSelf)
        {
            tManager = GameObject.FindObjectOfType<TimeManager>();
        }
    }
}
