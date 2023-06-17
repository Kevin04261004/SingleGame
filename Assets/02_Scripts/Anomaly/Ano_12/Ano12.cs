using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StageSystem;
using UnityEngine.UI;

public class Ano12 : Anomaly
{
    Stage stage;
    CCTV targetCCTV;
    Image targetImage;
    [SerializeField, Tooltip("�ش� ���� ���� �� �ش� ������ �����ϸ� �ذ�")] Area.AreaType need_to_enter = Area.AreaType.corrider_2F;

    public override void AnomalyEnd()
    {
        stage.event_player_area_enter -= Check_Player_Enter_Area;
        targetImage.fillAmount = 0f;
        if (coroutine_fill_skull != null) StopCoroutine(coroutine_fill_skull);
    }
    Coroutine coroutine_fill_skull;
    public override void AnomalyStart()
    {
        stage = FindObjectOfType<Stage>();
        targetCCTV = CCTVManager.instance.GetCCTVWithName("Corrider_2F");
        GameObject canvas = GameObject.Find("Canvas");
        GameObject portable_cctv_ui = null;
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            Transform cur = canvas.transform.GetChild(i);
            if (cur.gameObject.name == "cctv_background")
            {
                portable_cctv_ui = cur.gameObject;
                break;
            }
        }
        targetImage = portable_cctv_ui.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        targetImage.fillAmount = 0f;


        if (stage == null || targetCCTV == null || targetImage == null)
        {
            Debug.LogError("�ʿ��� ������Ʈ�� Scene���� ã�� ���Ͽ����ϴ�.");
            Force_SolveAnomaly();
            return;
        }
        stage.event_player_area_enter += Check_Player_Enter_Area;
        coroutine_fill_skull = StartCoroutine(FillSkull());
    }

    IEnumerator FillSkull()
    {
        float fill_per_sec = 1 / timeLimit;
        WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
        while (true)
        {
            if ((targetImage.fillAmount += fill_per_sec * Time.deltaTime) > 0.98f)
            {
                targetImage.fillAmount = 1f;
                break;
            }
            yield return waitFrame;
        }
    }

    void Check_Player_Enter_Area(Area.AreaType areaType)
    {
        if (areaType == need_to_enter)
        {
            Debug.Log($"'{need_to_enter}'������ �����Ͽ� '{anomalyName}'������ �ذ��Ͽ����ϴ�.");
            Force_SolveAnomaly();
        }
    }
}
