using UnityEngine;
using UnityEngine.EventSystems;

public class EditBow : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (ToolManager.activeTool == ToolManager.ToolType.Delete)
        {
            AudioManager.Instance.Play("EliminarFlor"); 
            Destroy(gameObject);
        }
    }
}
