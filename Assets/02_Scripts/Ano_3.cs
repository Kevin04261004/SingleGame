using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모텔 문이 열림.
/// 안을 들여다보면 사망.
/// </summary>
public class Ano_3 : Anomaly
{
    private Phe_3_Door Door;
    public override void AnomalyStart()
    {
        // 새로 생성하는 Phenomenon뿐만 아니라
        // Scene에 존재하던 Phenomenon오브젝트를 사용할 수 있게 FindPhenomenonObjectInScene<T>()함수를 구현했으니 사용 바람.

        // 기존의 FindObjectOfType<Phe_3_Door>()를 FindPhenomenonObjectInScene<Phe_3_Door>()로 번경
        //Door = FindObjectOfType<Phe_3_Door>();
        Door = FindPhenomenonObjectInScene<Phe_3_Door>();

        // 아래 두 줄은 Door Phenomenon이 시작될 때 하는 행위이므로 Door의 PhenomenonStart()로 처리로직을 이동
        //Door.doorKnob.tag = "Interactable";
        //Door.GetComponent<Phe_3_Door>().ToggleDoor();

        // FindPhenomenonObjectInScene<T>()을 사용하면 자동으로 Init()을 호출함
        //Door.Init(this);
    }
    public override void AnomalyEnd()
    {

    }
}
