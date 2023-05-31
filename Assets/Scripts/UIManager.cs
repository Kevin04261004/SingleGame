using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton <UIManager>
{
    [SerializeField] private TextMeshProUGUI timeClock_TMP;
    [SerializeField] private Image RuleBook_BackGround;
    [SerializeField] private Image CCTV_BackGround;
    public void Set_TimeClock_TMP(int time)
    {
        int hour = time / 60;
        int minute = time % 60;
        timeClock_TMP.text = string.Format("{0:D2} : {1:D2}", hour, minute);
    }
    public void Set_RuleBook_BackGround_TrueOrFalse()
    {
        if(RuleBook_BackGround.gameObject.activeSelf)
        {
            RuleBook_BackGround.gameObject.SetActive(false);
        }
        else
        {
            RuleBook_BackGround.gameObject.SetActive(true);
        }
    }
    public void Set_CCTV_BackGround_TrueOrFalse()
    {
        if (CCTV_BackGround.gameObject.activeSelf)
        {
            CCTV_BackGround.gameObject.SetActive(false);
        }
        else
        {
            CCTV_BackGround.gameObject.SetActive(true);
        }
    }
}
