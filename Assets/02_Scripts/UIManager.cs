using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [field:SerializeField] public Image Died_Image { get; private set; }

    [Tooltip("크로스헤어 기본 색상")] [SerializeField] private Color baseColor;
    [Tooltip("크로스헤어가 상호작용 가능할 때의 색상")] [SerializeField] private Color changeColor;
    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerMovementController>();
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
            playerController.Set_canMove_Bool(true);
        }
        else
        {
            RuleBook_BackGround.gameObject.SetActive(true);
            middlePoint_Image.gameObject.SetActive(false);
            timeClock_Image.gameObject.SetActive(false);
            RuleBookIcon_Image.gameObject.SetActive(false);
            CCTVIcon_Image.gameObject.SetActive(false);
            playerController.Set_canMove_Bool(false);
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
            playerController.Set_canMove_Bool(true);
        }
        else
        {
            playerMesh_GameObject.SetActive(true);
            CCTV_BackGround.gameObject.SetActive(true);
            middlePoint_Image.gameObject.SetActive(false);
            RuleBookIcon_Image.gameObject.SetActive(false);
            CCTVIcon_Image.gameObject.SetActive(false);
            playerController.Set_canMove_Bool(false);
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
    public void OnClick_Btn()
    {
        if(EventSystem.current.currentSelectedGameObject.GetComponent<Btn_Bool>().isAnswer)
        {
            if (phenomenon is IDialogue)
            {
                (phenomenon as IDialogue).Fixed();
                playerController.Set_canMove_Bool(true);
            }
        }
        else
        {
            Debug.Log("죽음");
        }
    }
    public void Btns()
    {
        for(int i = 0; i < 3;i++)
        {
            
        }
    }
    public void Set_Phenomenom(Phenomenon instance)
    {
        phenomenon = instance;
    }
    public void OnClick_Retry_Btn()
    {
        print(1);
        SceneManager.LoadScene("00_TitleScene");
    }
}
