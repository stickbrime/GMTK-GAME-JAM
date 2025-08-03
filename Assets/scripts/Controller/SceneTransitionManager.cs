using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    [SerializeField] private float defaultFadeDuration = 1f;
    private CanvasGroup fadeCanvas;
    private Image fadeImage;
    private Action onCompleteCallback;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeFadeCanvas();
    }

    public void TransitionToScene(
        string sceneName,
        float duration = -1,
        Action onComplete = null
    )
    {
        if (duration < 0) duration = defaultFadeDuration;
        onCompleteCallback = onComplete;
        StartCoroutine(TransitionRoutine(sceneName, duration));
    }

    IEnumerator TransitionRoutine(string sceneName, float duration)
    {
        // 淡出到黑屏
        yield return StartCoroutine(Fade(0f, 1f, duration / 2, true));

        // 加载新场景
        SceneManager.LoadScene(sceneName);

        // 确保场景完全加载
        yield return null;
        yield return null;

        // 淡入显示新场景
        yield return StartCoroutine(Fade(1f, 0f, duration / 2, false));

        // 执行完成回调
        onCompleteCallback?.Invoke();
    }

    IEnumerator Fade(float from, float to, float duration, bool isFadeOut)
    {
        if (fadeCanvas == null) yield break;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            fadeCanvas.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        fadeCanvas.alpha = to;

        // 如果是淡出，在这里可以添加额外效果
        if (isFadeOut && to >= 1f)
        {
            // 可以在这里添加音效或其他效果
        }
    }

    void InitializeFadeCanvas()
    {
        GameObject canvasObj = new GameObject("FadeCanvas");
        canvasObj.transform.SetParent(transform);

        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1000;

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        fadeCanvas = canvasObj.AddComponent<CanvasGroup>();
        fadeCanvas.alpha = 0f;
        fadeCanvas.blocksRaycasts = false;

        GameObject imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(canvasObj.transform);

        fadeImage = imageObj.AddComponent<Image>();
        fadeImage.color = Color.black;
        fadeImage.raycastTarget = false;

        RectTransform rt = imageObj.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }

    public void ForceFadeOut(float duration = 0.5f)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(fadeCanvas.alpha, 1f, duration, true));
    }

    public void ForceFadeIn(float duration = 0.5f)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(fadeCanvas.alpha, 0f, duration, false));
    }
}