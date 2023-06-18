using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerMovementController playerMovementController;
    [SerializeField] GameObject prefab_gameoverVolume;
    GameObject gameoverVolume;
    AudioSource gameoverSound;
    /// <summary>
    /// 6시 되면 게임이 클리어됨.
    /// </summary>
    public void GameClear()
    {
        StartCoroutine(LoadClearScene());
    }
    public void Died()
    {
        //playerMovementController.Set_canMove_Bool(false);
        DialogueSystem.DialogueManager.instance.TryStopReadingSummaries();
        playerMovementController.PreventMovement_AddStack();
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
        gameoverVolume = Instantiate(prefab_gameoverVolume);
        gameoverSound?.Play();

        FadeManager.instance.StartCoroutine(FadeManager.instance.FadeOut());
        yield return new WaitForSecondsRealtime(FadeManager.instance.FadeInSpeed + 0.5f);
        Destroy(gameoverVolume.gameObject);
        UIManager.instance.Died_Image.gameObject.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
