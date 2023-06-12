using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phe_15_RockingChair : Phenomenon, IFlashLight
{
    [SerializeField] Transform rotationPivot = null;
    // �ִ� ȸ����: ���, ����
    // ȸ�� �ӵ�: ���, ����

    [Header("Properties")]
    [SerializeField, Min(0)] float maxSwingAngle_usual = 10;
    [SerializeField, Min(0.01f)] float swingSpeed_usual = 2f;

    [SerializeField, Min(0)] float maxSwingAngle_anomaly = 30;
    [SerializeField, Min(0.01f)] float swingSpeed_anomaly = 20f;

    [SerializeField, ReadOnly] float cur_swingAngle;
    [SerializeField, ReadOnly] float cur_swingSpeed;

    [SerializeField] AnimationCurve animCurve;

    [SerializeField, Tooltip("�÷��� ����Ʈ�� ����� �ϴ� Ƚ��"), Min(1)] int need_flashLight_cnt = 2;
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
    /// ���ڸ� �ٶ�ä�� ���� ų ��� true,
    /// �� ���� ���� �ٽ� �� ��� false�� ���ϸ� ī��Ʈ ����
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
