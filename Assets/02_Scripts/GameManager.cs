using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerMovementController playerMovementController;
    /// <summary>
    /// 6시 되면 게임이 클리어됨.
    /// </summary>
    public void GameClear()
    {
        StartCoroutine(LoadClearScene());
    }
    public void Died()
    {
        playerMovementController.Set_canMove_Bool(false);
        Time.timeScale = 0;
        StartCoroutine(DiedImageOpen());
    }
    public IEnumerator LoadClearScene()
    {
        FadeManager.instance.StartCoroutine(FadeManager.instance.FadeOut());
        yield return new WaitForSeconds(FadeManager.instance.FadeInSpeed +0.5f);
        SceneManager.LoadScene("02_ClearScene");
    }
    public IEnumerator DiedImageOpen()
    {
        FadeManager.instance.StartCoroutine(FadeManager.instance.FadeOut());
        yield return new WaitForSecondsRealtime(FadeManager.instance.FadeInSpeed + 0.5f);
        UIManager.instance.Died_Image.gameObject.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
