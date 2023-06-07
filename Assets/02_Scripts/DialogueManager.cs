using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

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

public class DialogueManager : Singleton<DialogueManager>
{
    public float typeSpeed;
    public string content;
    public string char_name;
    public string Btn_01;
    public bool btn_01_true;
    public string Btn_02;
    public bool btn_02_true;
    public string Btn_03;
    public bool btn_03_true;
    public void StartReadDialogue(DialogueData data)
    {
        char_name = data.name;
        content = data.content;
        Btn_01 = data.btn1;
        btn_01_true = data.btn1_true;
        Btn_02 = data.btn2;
        btn_02_true = data.btn2_true;
        Btn_03 = data.btn3;
        btn_03_true = data.btn3_true;
        StartCoroutine(Reader());
    }
    IEnumerator Reader()
    {
        int i;
        for (i = 1; i < content.Length; i++)
        {
            UIManager.instance.Set_DialogueText_Change(char_name, content, i+1);
            yield return new WaitForSeconds(typeSpeed);
        }
        UIManager.instance.Set_Buttons_Bool(true,Btn_01,btn_01_true, Btn_02, btn_02_true, Btn_03,btn_03_true);
    }

}
