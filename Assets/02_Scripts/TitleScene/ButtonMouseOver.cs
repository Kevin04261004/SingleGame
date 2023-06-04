using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 버튼에 마우스가 갖다대면, 이미지가 보여지는 스크립트.
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