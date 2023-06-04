using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.AI;

public class DialogueManager : Singleton<DialogueManager>
{
    public float typeSpeed;
    public string content;
    public string name;
    public string Btn_01;
    public string Btn_02;
    public string Btn_03;
    public void StartReadDialogue(string _name, string _content, string btn1, string btn2, string btn3)
    {
        name = _name;
        content = _content;
        Btn_01 = btn1;
        Btn_02 = btn2;
        Btn_03 = btn3;
        StartCoroutine(Reader());
    }
    IEnumerator Reader()
    {
        int i;
        for (i = 1; i < content.Length; i++)
        {
            UIManager.instance.Set_DialogueText_Change(name, content, i+1);
            yield return new WaitForSeconds(typeSpeed);
        }
        UIManager.instance.Set_Buttons_Bool(true,Btn_01, Btn_02, Btn_03);
    }

}
