using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroll : MonoBehaviour
{
    public float speed = 50f;
    public float stopY = 1000f;
    public float startDelay = 5f;
    public float fadeDuration = 2f;

    public string menuScene = "MenuInicial";

    private RectTransform rt;
    private CanvasGroup canvasGroup;

    private bool canScroll = false;
    private bool stopped = false;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;

        Invoke(nameof(StartFadeIn), startDelay);
    }

    void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    System.Collections.IEnumerator FadeIn()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        canScroll = true;
    }

    void Update()
    {
        if (stopped || !canScroll) return;

        rt.anchoredPosition += Vector2.up * speed * Time.deltaTime;

        if (rt.anchoredPosition.y >= stopY)
        {
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, stopY);
            stopped = true;
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
}