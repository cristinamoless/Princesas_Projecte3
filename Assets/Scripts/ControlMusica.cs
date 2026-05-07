using UnityEngine;
using UnityEngine.UI;

public class ControlMusica : MonoBehaviour
{
    public AudioSource musica;
    public Slider sliderMusica;

    void Awake()
    {
        Debug.Log("CONTROL MUSICA ACTIVAT");
    }

    void Update()
    {
        if (musica == null || sliderMusica == null) return;

        musica.volume = sliderMusica.value;
    }
}