using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class EditFlower : MonoBehaviour, IPointerClickHandler
{
    private Flower flower;

    void Awake()
    {
        flower = GetComponent<Flower>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ToolManager.scissorsActive)
        {
            Debug.Log("tallant flor!");
            flower.RemoveLeaves();
        }
        if (ToolManager.deleteActive)
        {
            Debug.Log("eliminant flor!");
            Destroy(gameObject);
        }
    }
}

