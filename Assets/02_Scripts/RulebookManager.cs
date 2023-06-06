using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulebookManager : MonoBehaviour
{
    [SerializeField] private Button leftBtn;
    [SerializeField] private Button rightBtn;
    [SerializeField] private GameObject[] pages;
    [SerializeField] private int curpage = 1;

    private void Start()
    {
        SetBtns();
    }
    public void OnClickLeftBtn()
    {
        if(curpage == 2)
        {
            pages[2].gameObject.SetActive(false);
            pages[3].gameObject.SetActive(false);
        }
        SetBtns();
    }

    public void SetBtns()
    {
        if(curpage == 1)
        {
            leftBtn.gameObject.SetActive(false);
            rightBtn.gameObject.SetActive(true);
        }
        else if (curpage == pages.Length/2)
        {
            leftBtn.gameObject.SetActive(true);
            rightBtn.gameObject.SetActive(false);
        }
    }
}
