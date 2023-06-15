using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_4_Door : Phenomenon
{
    public bool isOpen { get; private set; } = false;
    [SerializeField] float hingeAngle_opened;
    [SerializeField] float hingeAngle_closed;
    [SerializeField] float time_for_openAndClosing = 1f;

    protected override void PhenomenonEnd()
    {
        isRunning = false;
    }

    protected override void PhenomenonStart()
    {
        transform.GetChild(0).GetComponent<Phe_4_DoorKnob>().Init(Solution);
        isRunning = true;
        ToggleDoor();
    }

    Coroutine coroutine_toggleDoor = null;
    bool isRunning = false;
    public bool isInteractable => coroutine_toggleDoor == null && isRunning;

    public void ToggleDoor()
    {
        if (isInteractable)
        {
            coroutine_toggleDoor = StartCoroutine(RotateDoor());
        }
    }

    IEnumerator RotateDoor()
    {
        WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();

        float start = Time.time;
        float end = start + time_for_openAndClosing;

        float curAngle;

        float rot_start = (isOpen ? hingeAngle_opened : hingeAngle_closed);
        float rot_end = (!isOpen ? hingeAngle_opened : hingeAngle_closed);

        while (Time.time < end)
        {
            float t = (Time.time - start) / time_for_openAndClosing;
            curAngle = Mathf.Lerp(rot_start, rot_end, t);
            transform.rotation = Quaternion.Euler(0, curAngle, 0);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, rot_end, 0);

        isOpen = !isOpen;
        coroutine_toggleDoor = null;
    }

    protected override void Solution()
    {
        TryFixThisPhenomenon();
    }
}
