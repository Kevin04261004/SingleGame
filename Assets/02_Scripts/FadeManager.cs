using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : Singleton<FadeManager>
{
    public Image FadeImage;
    public bool isFading = false;
    public float FadeInSpeed = 1f;
    Color temp;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        isFading = true;
        FadeImage.gameObject.SetActive(true);
        FadeImage.color = new Color(0, 0, 0, 1);
        temp = new Color(0, 0, 0, 0.01f);
        for (int i = 0; i < 100; i++)
        {
            FadeImage.color -= temp;
            yield return new WaitForSeconds(FadeInSpeed / 100);
        }
        FadeImage.color = new Color(0, 0, 0, 0);
        FadeImage.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        isFading = false;
    }
    public IEnumerator FadeOut()
    {
        isFading = true;
        FadeImage.gameObject.SetActive(true);
        FadeImage.color = new Color(0, 0, 0, 0);
        temp = new Color(0, 0, 0, 0.01f);
        for (int i = 0; i < 100; i++)
        {

            FadeImage.color += temp;
            yield return new WaitForSeconds(FadeInSpeed / 100);
        }
        FadeImage.color = new Color(0, 0, 0, 1);
        FadeImage.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        isFading = false;
    }
    public IEnumerator FlashIn()
    {
        isFading = true;
        FadeImage.gameObject.SetActive(true);
        FadeImage.color = new Color(1, 1, 1, 1);
        temp = new Color(0, 0, 0, 0.01f);
        for (int i = 0; i < 100; i++)
        {

            FadeImage.color -= temp;
            yield return new WaitForSeconds(FadeInSpeed / 100);
        }
        FadeImage.color = new Color(1, 1, 1, 0);
        FadeImage.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        isFading = false;
    }
    public IEnumerator FlashOut()
    {
        isFading = true;
        FadeImage.gameObject.SetActive(true);
        FadeImage.color = new Color(1, 1, 1, 0);
        temp = new Color(0, 0, 0, 0.01f);
        for (int i = 0; i < 100; i++)
        {

            FadeImage.color += temp;
            yield return new WaitForSeconds(FadeInSpeed / 100);
        }
        FadeImage.color = new Color(1, 1, 1, 1);
        FadeImage.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        isFading = false;
    }
}
