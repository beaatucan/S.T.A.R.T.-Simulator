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

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Simple bobbing animation
        bobTime += Time.deltaTime * bobSpeed;
        transform.position = startPosition + Vector3.up * (Mathf.Sin(bobTime) * bobAmount);
    }

    public void SetPromptText(string text)
    {
        if (promptText != null)
            promptText.text = text;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}