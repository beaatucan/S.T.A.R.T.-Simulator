using UnityEngine;
using TMPro;

public class InteractionPrompt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private float fadeSpeed = 4f;
    [SerializeField] private float bobAmount = 0.5f;
    [SerializeField] private float bobSpeed = 2f;

    private Vector3 startPosition;
    private float bobTime;
    private CanvasGroup canvasGroup;
    private float targetAlpha;

    private void Start()
    {
        startPosition = transform.position;
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;
    }

    private void Update()
    {
        // Simple bobbing animation
        bobTime += Time.deltaTime * bobSpeed;
        transform.position = startPosition + Vector3.up * (Mathf.Sin(bobTime) * bobAmount);

        // Smooth fade effect
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
        }
    }

    public void SetPromptText(string text)
    {
        if (promptText != null)
            promptText.text = text;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        targetAlpha = 1f;
    }

    public void Hide()
    {
        targetAlpha = 0f;
        if (canvasGroup.alpha == 0)
        {
            gameObject.SetActive(false);
        }
    }
}