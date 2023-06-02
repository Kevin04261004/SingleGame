using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_3_Door : Phenomenon
{
    [SerializeField, ReadOnly] private bool isOpened = false;
    [SerializeField] private Quaternion openedRotation;
    [SerializeField] private Quaternion closedRotation;
    [SerializeField] private float doorSpeed;

    public Phe_3_DoorKnob doorKnob { get; private set; } = null;

    private Coroutine doorCoroutine;
    public void ToggleDoor()
    {
        if (doorCoroutine != null)
            StopCoroutine(doorCoroutine);

        doorCoroutine = StartCoroutine(OpenCloseDoor());
    }
    private void Awake()
    {
        doorKnob = transform.GetChild(0).GetComponent<Phe_3_DoorKnob>();
    }

    protected override void PhenomenonEnd()
    {
        
    }

    protected override void PhenomenonStart()
    {
        
    }

    protected override void Solution()
    {
        
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
        if (!isOpened) TryFixThisPhenomenon();
    }
}
