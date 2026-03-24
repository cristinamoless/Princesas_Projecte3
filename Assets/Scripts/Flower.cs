using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Flower : MonoBehaviour
{
    private Image img;

    public Sprite withLeaves;
    public Sprite withoutLeaves;

    void Awake()
    {
        img = GetComponent<Image>();
        img.sprite = withLeaves;
    }

    public void RemoveLeaves()
    {
        if (img != null)
            img.sprite = withoutLeaves;
    }
    public void Rotate(float angle)
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.Rotate(0, 0, angle);
    }


}
