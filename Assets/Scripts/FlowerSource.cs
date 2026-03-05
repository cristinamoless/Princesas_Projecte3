using UnityEngine;
using UnityEngine.EventSystems;

public class FlowerSource : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject flowerPrefab;  
    public Transform tableArea;     

    private RectTransform currentFlower;

    public void OnBeginDrag(PointerEventData eventData)
    {

        GameObject clone = Instantiate(flowerPrefab, tableArea);
        currentFlower = clone.GetComponent<RectTransform>();

        currentFlower.position = Input.mousePosition;

        currentFlower.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentFlower != null)
        {
            currentFlower.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        currentFlower = null;
    }
}