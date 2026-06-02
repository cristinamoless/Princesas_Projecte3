using UnityEngine;

public class Toggler : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        panel.SetActive(false);
    }

    public void Toggle()
    {
        bool actiu = panel.activeSelf;
        panel.SetActive(!actiu);
    }
    public void Tancar()
    {
        panel.SetActive(false);
    }
}