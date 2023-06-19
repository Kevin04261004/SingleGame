using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyManager : MonoBehaviour
{
    public float tempBGMSound = 0.5f;
    public float tempEffectSound = 0.5f;

    public GameObject BGMCanvas;
    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = 0;
            BGMCanvas.SetActive(true);
        }
        if (BGMCanvas.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
