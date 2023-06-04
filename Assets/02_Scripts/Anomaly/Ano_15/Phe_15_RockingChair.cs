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

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    protected override void PhenomenonEnd()
    {
        throw new System.NotImplementedException();
    }

    protected override void PhenomenonStart()
    {
        throw new System.NotImplementedException();
    }

    protected override void Solution()
    {
        throw new System.NotImplementedException();
    }
}
