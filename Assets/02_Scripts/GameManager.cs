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

    public enum CauseOfDeath
    {
        timeOver, wrongChoice, detectedBySomething, violationRule
    }

    /// <summary>
    /// 6시 되면 게임이 클리어됨.
    /// </summary>
    public void GameClear()
    {
        StartCoroutine(LoadClearScene());
    }
    public void Died(CauseOfDeath causeOfDeath, string deathSummary = "")
    {
        if (UIManager.instance.Died_Image.gameObject.activeSelf)
        {
            return;
        }
        //playerMovementController.Set_canMove_Bool(false);
        DialogueSystem.DialogueManager.instance.TryStopReadingSummaries();
        playerMovementController.PreventMovement_AddStack();
        Time.timeScale = 0;
        StartCoroutine(DiedImageOpen(causeOfDeath, deathSummary));
    }
    public IEnumerator LoadClearScene()
    {
        FadeManager.instance.StartCoroutine(FadeManager.instance.FadeOut());
        yield return new WaitForSeconds(FadeManager.instance.FadeInSpeed +0.5f);
        SceneManager.LoadScene("02_ClearScene");
    }
    public IEnumerator DiedImageOpen(CauseOfDeath causeOfDeath, string deathSummary)
    {
        gameoverVolume = Instantiate(prefab_gameoverVolume);
        gameoverSound?.Play();

        UIManager.instance.tmp_areaName.gameObject.SetActive(false);

        FadeManager.instance.StartCoroutine(FadeManager.instance.FadeOut());
        yield return new WaitForSecondsRealtime(FadeManager.instance.FadeInSpeed + 0.5f);
        Destroy(gameoverVolume.gameObject);
        UIManager.instance.Died_Image.gameObject.SetActive(true);
        UIManager.instance.Died_Image.GetComponent<DiedImage>().SetSummaries(causeOfDeath, deathSummary);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
