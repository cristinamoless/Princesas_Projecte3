using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public float volume = 1f;
        public float pitch = 1f;
    }

    public Sound[] sounds;
    public AudioSource source;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, x => x.name == soundName);

        if (s != null && source != null)
        {
            source.volume = s.volume;
            source.pitch = s.pitch;
            source.PlayOneShot(s.clip);
        }
    }
}