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
            tmp_causeOfDeath.text = $"��� ����: {CauseOfDeathToString(causeOfDeath)}";
            tmp_deathSummary.text = "";
        }
        else
        {
            tmp_causeOfDeath.text = $"��� ����: {CauseOfDeathToString(causeOfDeath)}";
            tmp_deathSummary.text = summary;
        }
    }
    string CauseOfDeathToString(GameManager.CauseOfDeath causeOfDeath)
    {
        switch (causeOfDeath)
        {
            case GameManager.CauseOfDeath.timeOver: return "Ÿ�� ����";
            case GameManager.CauseOfDeath.wrongChoice: return "�߸��� ����";
            case GameManager.CauseOfDeath.detectedBySomething: return "�߰�";
            case GameManager.CauseOfDeath.violationRule: return "���� ����";
            default: return string.Empty;
        }
    }
}
