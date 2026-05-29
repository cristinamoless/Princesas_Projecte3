using UnityEngine;

public class MenuConfiguracio : MonoBehaviour
{
    public GameObject panelConfiguracio;
    public GameObject Titol;
    public GameObject Button;

    void Start()
    {
        panelConfiguracio.SetActive(false);
        Titol.SetActive(true);
        Button.SetActive(true);
    }

    public void ToggleConfiguracio()
    {
        bool actiu = panelConfiguracio.activeSelf;
        panelConfiguracio.SetActive(!actiu);
        Titol.SetActive(actiu);
        Button.SetActive(actiu);
    }
    public void TancarConfiguracio()
    {
        panelConfiguracio.SetActive(false);
        Titol.SetActive(true);
        Button.SetActive(true);
    }
}