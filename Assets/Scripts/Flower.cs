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


}
