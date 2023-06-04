using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� ����.
/// ���� �鿩�ٺ��� ���.
/// </summary>
public class Ano_3 : Anomaly
{
    private Phe_3_Door Door;
    public override void AnomalyStart()
    {
        // ���� �����ϴ� Phenomenon�Ӹ� �ƴ϶�
        // Scene�� �����ϴ� Phenomenon������Ʈ�� ����� �� �ְ� FindPhenomenonObjectInScene<T>()�Լ��� ���������� ��� �ٶ�.

        // ������ FindObjectOfType<Phe_3_Door>()�� FindPhenomenonObjectInScene<Phe_3_Door>()�� ����
        //Door = FindObjectOfType<Phe_3_Door>();
        Door = FindPhenomenonObjectInScene<Phe_3_Door>();

        // �Ʒ� �� ���� Door Phenomenon�� ���۵� �� �ϴ� �����̹Ƿ� Door�� PhenomenonStart()�� ó�������� �̵�
        //Door.doorKnob.tag = "Interactable";
        //Door.GetComponent<Phe_3_Door>().ToggleDoor();

        // FindPhenomenonObjectInScene<T>()�� ����ϸ� �ڵ����� Init()�� ȣ����
        //Door.Init(this);
    }
    public override void AnomalyEnd()
    {

    }
}
