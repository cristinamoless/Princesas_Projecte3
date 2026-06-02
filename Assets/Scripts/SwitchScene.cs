using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SwitchScene : MonoBehaviour
{
    public string build = "BuildFlower";
    public string custom = "CreacioPersonatge";
    public string shop = "Floristeria";
    public string start = "MenuInicial";
    public TMP_Text text;
    public bool isLoading = false;

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void Start()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        text.text = "Donali a la F si vols muntar flors";

        if (Input.GetKeyDown(KeyCode.F) && !isLoading)
        {
            isLoading = true;
            SceneManager.LoadScene(build, LoadSceneMode.Additive);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.text = " ";
    }

    public void closeBuild()
    {
        AudioManager.Instance.Play("Clic");
        SceneManager.UnloadSceneAsync(build);
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (scene.name == build)
        {
            isLoading = false;
            text.text = " ";
        }
    }

    public void shopScene()
    {
        AudioManager.Instance.Play("Clic");
        SceneManager.LoadScene(shop);
        isLoading = false;
    }

    public void creacioScene()
    {
        SceneManager.LoadScene(custom);
    }
}
