using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DiedImage : MonoBehaviour
{
    [SerializeField] Text tmp_causeOfDeath;
    [SerializeField] Text tmp_deathSummary;

    public void SetSummaries(GameManager.CauseOfDeath causeOfDeath, string summary)
    {
        if (causeOfDeath == GameManager.CauseOfDeath.timeOver && summary == "")
        {
            tmp_causeOfDeath.text = $"사망 원인: {CauseOfDeathToString(causeOfDeath)}";
            tmp_deathSummary.text = "";
        }
        else
        {
            tmp_causeOfDeath.text = $"사망 원인: {CauseOfDeathToString(causeOfDeath)}";
            tmp_deathSummary.text = summary;
        }
    }
    string CauseOfDeathToString(GameManager.CauseOfDeath causeOfDeath)
    {
        switch (causeOfDeath)
        {
            case GameManager.CauseOfDeath.timeOver: return "타임 오버";
            case GameManager.CauseOfDeath.wrongChoice: return "잘못된 선택";
            case GameManager.CauseOfDeath.detectedBySomething: return "발각";
            case GameManager.CauseOfDeath.violationRule: return "규정 위반";
            default: return string.Empty;
        }
    }
}
