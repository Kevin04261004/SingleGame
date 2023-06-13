using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class title_UIManager : MonoBehaviour
{
    [SerializeField] private Image Option_Image;
    [SerializeField] private Image HowToPlay_Image;
    [SerializeField] private Image RuleBook_Image;
    public void Awake()
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
        Option_Image.gameObject.SetActive(true);
    }
    public void OnClick_HowToPlay_Btn()
    {
        HowToPlay_Image.gameObject.SetActive(true);
    }
    public void OnClick_Option_Exit()
    {
        Option_Image.gameObject.SetActive(false);
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
