using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    [Tooltip("크로스헤어 기본 색상")] [SerializeField] private Color baseColor;
    [Tooltip("크로스헤어가 상호작용 가능할 때의 색상")] [SerializeField] private Color changeColor;
    private void Awake()
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
        }
        else
        {
            RuleBook_BackGround.gameObject.SetActive(true);
            middlePoint_Image.gameObject.SetActive(false);
            timeClock_Image.gameObject.SetActive(false);
            RuleBookIcon_Image.gameObject.SetActive(false);
            CCTVIcon_Image.gameObject.SetActive(false);
        }
    }
    public void Set_CCTV_BackGround_TrueOrFalse()
    {
        if (CCTV_BackGround.gameObject.activeSelf)
        {
            CCTV_BackGround.gameObject.SetActive(false);
            middlePoint_Image.gameObject.SetActive(true);
            timeClock_Image.gameObject.SetActive(true);
            RuleBookIcon_Image.gameObject.SetActive(true);
            CCTVIcon_Image.gameObject.SetActive(true);
        }
        else
        {
            CCTV_BackGround.gameObject.SetActive(true);
            middlePoint_Image.gameObject.SetActive(false);
            timeClock_Image.gameObject.SetActive(false);
            RuleBookIcon_Image.gameObject.SetActive(false);
            CCTVIcon_Image.gameObject.SetActive(false);
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
        timeClock_Image.gameObject.SetActive(!Open);
        RuleBookIcon_Image.gameObject.SetActive(!Open);
        CCTVIcon_Image.gameObject.SetActive(!Open);
    }
    public void Set_DialogueText_Change(string name, string content,int typeIndex = 0)
    {
        name_text.text = name;
        content_text.text = content.Substring(0, typeIndex);
    }
    public void Set_Buttons_Bool(bool Open,string str1 = null, string str2 = null, string str3 = null)
    {
        if(!Open)
        {
            answer_text[0].transform.parent.gameObject.SetActive(false);
            answer_text[1].transform.parent.gameObject.SetActive(false);
            answer_text[2].transform.parent.gameObject.SetActive(false);
            return;
        }
        answer_text[0].transform.parent.gameObject.SetActive(Open);
        answer_text[1].transform.parent.gameObject.SetActive(Open);
        answer_text[2].transform.parent.gameObject.SetActive(Open);
        answer_text[0].text = str1;
        answer_text[1].text = str2;
        answer_text[2].text = str3;
    }
    public void OnClick_Bool_Btn(bool isTrue)
    {
        answer_text[0].transform.parent.gameObject.SetActive(false);
        answer_text[1].transform.parent.gameObject.SetActive(false);
        answer_text[2].transform.parent.gameObject.SetActive(false);

        if (isTrue)
        {
            playerController.Set_canMove_Bool(true);
        }
    }
}
