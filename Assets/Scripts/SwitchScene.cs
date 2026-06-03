using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SwitchScene : MonoBehaviour
{
    public string build = "BuildFlower";
    public string custom = "CreacioPersonatge";
    public string shop = "Floristeria";
    public string start = "MenuInicial";
    public bool isLoading = false;

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnTriggerStay(Collider other)
    {

        if (Input.GetKeyDown(KeyCode.F) && !isLoading)
        {
            isLoading = true;


            if (TutorialManager.Instance != null)
            {
                TutorialManager.Instance.OnEnterTableArea();
            }

            if (MusicManager.instance != null)
            {
                MusicManager.instance.CanviarAMusicaBuild();
            }

            SceneManager.LoadScene(build, LoadSceneMode.Additive);
        }
    }


    public void closeBuild()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.Play("Clic");
        SceneManager.UnloadSceneAsync(build);
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (scene.name == build)
        {
            isLoading = false;

            if (MusicManager.instance != null)
            {
                MusicManager.instance.CanviarAMusicaNormal();
            }
        }
    }

    public void shopScene()
    {
        if (AudioManager.Instance != null) AudioManager.Instance.Play("Clic");
        SceneManager.LoadScene(shop);
        isLoading = false;
    }

    public void creacioScene()
    {
        SceneManager.LoadScene(custom);
    }
}