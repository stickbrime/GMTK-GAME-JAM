using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private Image fadeImage;
    private bool isTransitioning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 自动创建淡入淡出UI
            if (fadeImage == null)
            {
                CreateFadeCanvas();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneWithFade(string sceneName, float duration)
    {
        if (!isTransitioning)
        {
            StartCoroutine(PerformSceneTransition(sceneName, duration));
        }
    }

    private IEnumerator PerformSceneTransition(string sceneName, float duration)
    {
        isTransitioning = true;

        // 淡出
        yield return StartCoroutine(Fade(0, 1, duration / 2));

        // 加载场景
        SceneManager.LoadScene(sceneName);

        // 淡入
        yield return StartCoroutine(Fade(1, 0, duration / 2));

        isTransitioning = false;
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float time = 0;
        Color color = fadeImage.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }

    private void CreateFadeCanvas()
    {
        GameObject canvasObj = new GameObject("FadeCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        GameObject imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(canvasObj.transform);

        fadeImage = imageObj.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.rectTransform.anchorMin = Vector2.zero;
        fadeImage.rectTransform.anchorMax = Vector2.one;
        fadeImage.rectTransform.offsetMin = Vector2.zero;
        fadeImage.rectTransform.offsetMax = Vector2.zero;

        DontDestroyOnLoad(canvasObj);
    }
}