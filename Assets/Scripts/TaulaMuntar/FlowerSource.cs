using UnityEngine;
using UnityEngine.EventSystems;

public class FlowerSource : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject flowerPrefab;
    public RectTransform tableArea;
    public bool lockedInitially = false;
    private RectTransform currentFlower;
    public FlowerType flowerType;

    public void Start()
    {
        if (!flowerType.unlocked)
        {
            gameObject.SetActive(false);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (ToolManager.activeTool != ToolManager.ToolType.Hand) return;

        GameFlowManager flow = FindFirstObjectByType<GameFlowManager>();

        if (flow != null && !flow.firstOrderCompleted)
        {
            if (lockedInitially)
            {
                return;
            }
        }

        GameObject clone = Instantiate(flowerPrefab, tableArea);

        FlowerSource fs = clone.GetComponent<FlowerSource>();
        if (fs != null)
            Destroy(fs);

        if (clone.GetComponent<DragDrop>() == null)
            clone.AddComponent<DragDrop>();
        if (clone.GetComponent<EditFlower>() == null)
            clone.AddComponent<EditFlower>();

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
        AudioManager.Instance.Play("ArrosegarFlor");
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
