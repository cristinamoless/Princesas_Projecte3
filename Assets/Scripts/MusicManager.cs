using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource musicSource;

    [Header("Cançons")]
    public AudioClip musicaNormal;
    public AudioClip musicaBuildFlower;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (musicSource != null && musicaNormal != null && !musicSource.isPlaying)
        {
            musicSource.clip = musicaNormal;
            musicSource.Play();
        }
    }

    public void CanviarAMusicaBuild()
    {
        if (musicSource == null || musicaBuildFlower == null) return;
        if (musicSource.clip == musicaBuildFlower) return; 

        musicSource.Stop();
        musicSource.clip = musicaBuildFlower;
        musicSource.Play();
    }

    public void CanviarAMusicaNormal()
    {
        if (musicSource == null || musicaNormal == null) return;
        if (musicSource.clip == musicaNormal) return; 

        musicSource.Stop();
        musicSource.clip = musicaNormal;
        musicSource.Play();
    }
}