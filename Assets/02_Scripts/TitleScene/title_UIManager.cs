using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class title_UIManager : Singleton<title_UIManager>
{
    [SerializeField] private GameObject Option_GO;
    [SerializeField] private Image HowToPlay_Image;
    [SerializeField] private Image RuleBook_Image;
    public void Start()
    {
        Screen.SetResolution(1920, 1080, true);
    }
    public void OnClick_StartGame_Btn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("01_GamePlayScene");
    }
    public void OnClick_Option_Btn()
    {
        Time.timeScale = 0;
        Option_GO.SetActive(true);
    }
    public void OnClick_HowToPlay_Btn()
    {
        HowToPlay_Image.gameObject.SetActive(true);
    }
    public void OnClick_Option_Exit()
    {
        Time.timeScale = 1;
        Option_GO.SetActive(false);
        if(SceneManager.GetActiveScene().name != "00_TitleScene")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void OnClick_HowToPlay_Exit()
    {
        HowToPlay_Image.gameObject.SetActive(false);
    }
    public void OnClick_RuleBook_Open()
    {
        RuleBook_Image.gameObject.SetActive(true);
    }
    public void OnClick_RuleBook_Exit()
    {
        RuleBook_Image.gameObject.SetActive(false);
    }
    
}
