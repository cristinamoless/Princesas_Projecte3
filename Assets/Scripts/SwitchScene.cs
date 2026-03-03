using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SwitchScene : MonoBehaviour
{
    public string sceneToLoad = "BuildFlower";
    public TMP_Text text;

    private void Start()
    {
        text.text = " ";
    }
    private void OnTriggerStay(Collider other)
    {
        text.text = "Donali a la F si vols muntar flors";
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        text.text = " ";
    }
}
