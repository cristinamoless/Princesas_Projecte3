using UnityEngine;
using TMPro;

public class LlibretaManager : MonoBehaviour
{
    public TMP_Text playerNameText;
    public TMP_Text currentDayText;

    private DateManager dateManager;

    void OnEnable()
    {
        if (dateManager == null)
        {
            dateManager = FindFirstObjectByType<DateManager>();
        }

        ActualitzarPerfil();
    }

    public void ActualitzarPerfil()
    {
        string nomJugador = PlayerPrefs.GetString("playerName", "Florista");
        if (playerNameText != null)
        {
            playerNameText.text = nomJugador;
        }

        if (dateManager != null && dateManager.dayText != null && currentDayText != null)
        {
            currentDayText.text = dateManager.dayText.text;
        }
    }
}