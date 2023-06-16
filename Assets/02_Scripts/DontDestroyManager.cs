using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyManager : Singleton<DontDestroyManager>
{
    public float tempBGMSound = 0.5f;
    public float tempEffectSound = 0.5f;

    public GameObject BGMCanvas;
    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            BGMCanvas.SetActive(true);
        }
        if (BGMCanvas.gameObject.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
