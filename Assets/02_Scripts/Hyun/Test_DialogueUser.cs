using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;

public class Test_DialogueUser : MonoBehaviour
{
    [SerializeField] H_DialogueData dData;
    private void Start()
    {
        dData.SetCallbacksToAllSelections(Clickjd);
    }
    private void Update()
    {
#warning DialogueTester
        if (Input.GetKeyDown(KeyCode.J))
        {
            falseCnt = 0;
            DialogueManager.instance.Try_RequestStartReadingDialogueData(dData);
        }
    }
    int falseCnt = 0;
    void Clickjd(ValueWhenClicked val)
    {
        if (val == ValueWhenClicked.False)
        {
            Debug.Log("FALSE�� ������!");
            if (++falseCnt == 2)
            {
                Debug.Log("False�� �� �� ���� Ż��");
                DialogueManager.instance.TryStopReadingSummaries();
            }
        }
    }
}
