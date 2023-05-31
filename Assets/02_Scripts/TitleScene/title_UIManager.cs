using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class title_UIManager : MonoBehaviour
{
    [SerializeField] private Image Option_Image;
    public void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
    }
    public void OnClick_StartGame_Btn()
    {
        SceneManager.LoadScene("01_GamePlayScene");
    }
    public void OnClick_Option_Btn()
    {
        Option_Image.gameObject.SetActive(true);
    }
    public void OnClick_Option_Exit()
    {
        Option_Image.gameObject.SetActive(false);
    }
}
