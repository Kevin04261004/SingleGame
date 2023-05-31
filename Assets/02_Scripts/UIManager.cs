using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton <UIManager>
{
    [SerializeField] private Text timeClock_text;
    [SerializeField] private Image RuleBook_BackGround;
    [SerializeField] private Image CCTV_BackGround;
    [SerializeField] private Image RuleBookIcon_Image;
    [SerializeField] private Image CCTVIcon_Image;
    [SerializeField] private Image timeClock_Image;
    [SerializeField] private Image middlePoint_Image;

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
    public void Set_middlePoint_Image_Color(Color color)
    {
        middlePoint_Image.color = color;
    }
}
