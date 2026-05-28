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

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        text.text = " ";
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

    public void shopScene()
    {
        SceneManager.LoadScene(shop);
    }
    public void closeBuild()
    {
        SceneManager.UnloadSceneAsync(build);
        isLoading = false;
    }
    public void creacioScene()
    {
        SceneManager.LoadScene(custom);
    }
}
