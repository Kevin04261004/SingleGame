using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ��ư�� ���콺�� ���ٴ��, �̹����� �������� ��ũ��Ʈ.
/// </summary>
public class ButtonMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.enabled = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        image.enabled = false;
    }
}