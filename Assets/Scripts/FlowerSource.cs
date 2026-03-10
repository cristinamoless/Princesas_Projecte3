using UnityEngine;
using UnityEngine.EventSystems;

public class FlowerSource : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject flowerPrefab;
    public RectTransform tableArea;

    private RectTransform currentFlower;

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject clone = Instantiate(flowerPrefab, tableArea);
        currentFlower = clone.GetComponent<RectTransform>();

        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            tableArea,
            Input.mousePosition,
            eventData.pressEventCamera,
            out localPos
        );

        currentFlower.anchoredPosition = localPos;
        currentFlower.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentFlower != null)
        {
            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                tableArea,
                Input.mousePosition,
                eventData.pressEventCamera,
                out localPos
            );

            currentFlower.anchoredPosition = localPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        currentFlower = null;
    }
}
