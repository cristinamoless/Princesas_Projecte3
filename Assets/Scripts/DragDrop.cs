using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	private RectTransform rectTransform;
	private Image image;
	public Canvas canvas;

	public void OnBeginDrag(PointerEventData eventData)
	{
		image.color = new Color32(255, 255, 255, 170);
	}
	public void OnDrag(PointerEventData eventData)
	{
		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
	}
	public void OnEndDrag(PointerEventData eventData)
	{
		image.color = new Color32(255, 255, 255, 255);
	}

	void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		image = GetComponent<Image>();
	}


}
