using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSceneUIManager : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private float speed;
    public Coroutine UpdateCoroutine;
    private void Start()
    {
        StartCoroutine(WaitForSecondAndStartUpdate(3));
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            ExitGame();
        }
    }
    private IEnumerator WaitForSecondAndStartUpdate(int howMuch)
    {
        yield return new WaitForSeconds(howMuch);
        UpdateCoroutine = StartCoroutine(CoroutineUpdate());
    }
    private IEnumerator CoroutineUpdate()
    {
        while(true)
        {
            text.transform.Translate(new Vector3(0,1,0) * Time.deltaTime * speed);
            yield return null;
        }
    }
    private void ExitGame()
    {
        GameManager.instance.ExitGame();
    }
}
