using UnityEngine;
using TMPro;

public class LlibretaManager : MonoBehaviour
{
    public TMP_Text playerNameText;

    void OnEnable()
    {

        ActualitzarPerfil();
    }

    public void ActualitzarPerfil()
    {
        string nomJugador = PlayerPrefs.GetString("playerName", "Florista");
        if (playerNameText != null)
        {
            playerNameText.text = nomJugador;
        }

    }
}