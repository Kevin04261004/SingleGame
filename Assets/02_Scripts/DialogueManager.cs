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
    public string btn2;
    public string btn3;
}

public class DialogueManager : Singleton<DialogueManager>
{
    public float typeSpeed;
    public string content;
    public string char_name;
    public string Btn_01;
    public string Btn_02;
    public string Btn_03;
    public void StartReadDialogue(DialogueData data)
    {
        char_name = data.name;
        content = data.content;
        Btn_01 = data.btn1;
        Btn_02 = data.btn2;
        Btn_03 = data.btn3;
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
        UIManager.instance.Set_Buttons_Bool(true,Btn_01, Btn_02, Btn_03);
    }

}
