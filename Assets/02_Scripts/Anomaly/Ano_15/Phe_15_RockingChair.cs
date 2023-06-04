using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_15_RockingChair : Phenomenon, IInteratable
{
    [SerializeField] Transform rotationPivot = null;
    // 최대 회전각: 평소, 현상
    // 회전 속도: 평소, 현상

    [Header("Properties")]
    [SerializeField, Min(0)] float maxSwingAngle_usual = 10;
    [SerializeField, Min(0.01f)] float swingSpeed_usual = 2f;

    [SerializeField, Min(0)] float maxSwingAngle_anomaly = 30;
    [SerializeField, Min(0.01f)] float swingSpeed_anomaly = 20f;

    [SerializeField, ReadOnly] float cur_swingAngle;
    [SerializeField, ReadOnly] float cur_swingSpeed;

    [SerializeField] AnimationCurve animCurve;

    private void Awake()
    {
        cur_swingAngle = maxSwingAngle_usual;
        cur_swingSpeed = swingSpeed_usual;
        StartCoroutine(Swing());
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    protected override void PhenomenonEnd()
    {
        cur_swingAngle = maxSwingAngle_usual;
        cur_swingSpeed = swingSpeed_usual;
    }

    protected override void PhenomenonStart()
    {
        cur_swingAngle = maxSwingAngle_anomaly;
        cur_swingSpeed = swingSpeed_anomaly;
    }

    protected override void Solution()
    {
        
    }

    IEnumerator Swing()
    {
        WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
        Transform rpivot = rotationPivot.transform;

        float cur = 0, target;
        float acceleration = 1;

        while (true)
        {
            if (cur > 0)
            {
                target = -cur_swingAngle;
                while (true)
                {
                    cur -= cur_swingSpeed * Time.deltaTime * acceleration;
                    if (cur <= target)
                    {
                        cur = target;
                        rpivot.rotation = Quaternion.Euler(target, rpivot.rotation.eulerAngles.y, rpivot.rotation.eulerAngles.z);
                        break;
                    }
                    rpivot.rotation = Quaternion.Euler(cur, rpivot.rotation.eulerAngles.y, rpivot.rotation.eulerAngles.z);
                    yield return waitFrame;
                }
            }
            else
            {
                target = cur_swingAngle;
                while (true)
                {
                    cur += cur_swingSpeed * Time.deltaTime * acceleration;
                    if (cur >= target)
                    {
                        cur = target;
                        rpivot.rotation = Quaternion.Euler(target, rpivot.rotation.eulerAngles.y, rpivot.rotation.eulerAngles.z);
                        break;
                    }
                    rpivot.rotation = Quaternion.Euler(cur, rpivot.rotation.eulerAngles.y, rpivot.rotation.eulerAngles.z);
                    yield return waitFrame;
                }
            }
            yield return waitFrame;
        }
    }
}
