using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Toggler : MonoBehaviour
{
    public GameObject panel;
    public List<GameObject> panellsATancar;

    void Start()
    {
        panel.SetActive(false);
    }

    public void Obrir()
    {
        if (panellsATancar != null)
        {
            foreach (GameObject p in panellsATancar)
            {
                if (p != null) p.SetActive(false);
            }
        }
        panel.SetActive(true);
    }
    public void Toggle()
    {
        bool actiu = panel.activeSelf;
        panel.SetActive(!actiu);
        if (panellsATancar != null)
        {
            foreach (GameObject p in panellsATancar)
            {
                if (p != null) p.SetActive(false);
            }
        }
    }
    public void Tancar()
    {
        panel.SetActive(false);
    }
}