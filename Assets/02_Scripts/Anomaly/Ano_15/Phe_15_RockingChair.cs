using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_15_RockingChair : Phenomenon, IFlashLight
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

    [SerializeField, Tooltip("플래시 라이트로 비춰야 하는 횟수"), Min(1)] int need_flashLight_cnt = 2;
    [SerializeField, ReadOnly] int remain_flashLight_cnt = -1;

    private void Awake()
    {
        cur_swingAngle = maxSwingAngle_usual;
        cur_swingSpeed = swingSpeed_usual;
        StartCoroutine(Swing());
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
        remain_flashLight_cnt = need_flashLight_cnt;
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
    /// <summary>
    /// 의자를 바라본채로 불을 킬 경우 true,
    /// 그 이후 불을 다시 끌 경우 false로 변하며 카운트 감소
    /// </summary>
    bool isLightOn = false;

    public void OnLighting_Start()
    {
        if (!isLightOn) isLightOn = true;
    }

    public void OnLighting()
    {
        
    }

    public void OnLighting_End()
    {
        if (isLightOn)
        {
            isLightOn = false;
            if (--remain_flashLight_cnt == 0)
            {
                TryFixThisPhenomenon();
            }
        }
    }
}
