using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool isOpened = false;
    [SerializeField] private Quaternion openedRotation;
    [SerializeField] private Quaternion closedRotation;
    [SerializeField] private float doorSpeed;

    private Coroutine doorCoroutine;

    public void Start()
    {
        if(gameObject.transform.eulerAngles.y != 0)
        {
            isOpened = true;
        }
    }
    public void ToggleDoor()
    {
        if (doorCoroutine != null)
            StopCoroutine(doorCoroutine);

        doorCoroutine = StartCoroutine(OpenCloseDoor());
    }

    private IEnumerator OpenCloseDoor()
    {
        Quaternion targetRotation = isOpened ? closedRotation : openedRotation;
        Quaternion startRotation = transform.rotation;
        float startTime = Time.time;
        float endTime = startTime + doorSpeed;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / doorSpeed;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }

        transform.rotation = targetRotation;
        isOpened = !isOpened;
    }
}