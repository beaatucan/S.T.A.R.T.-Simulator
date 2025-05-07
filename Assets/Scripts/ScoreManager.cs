using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI correctCounterText;
    [SerializeField] private TextMeshProUGUI incorrectCounterText;

    private int totalCorrect = 0;
    private int totalIncorrect = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateCounterDisplay();
    }

    public void IncrementCorrect()
    {
        totalCorrect++;
        UpdateCounterDisplay();
    }

    public void IncrementIncorrect()
    {
        totalIncorrect++;
        UpdateCounterDisplay();
    }

    private void UpdateCounterDisplay()
    {
        if (correctCounterText != null)
            correctCounterText.text = $"Correct: {totalCorrect}";
        if (incorrectCounterText != null)
            incorrectCounterText.text = $"Incorrect: {totalIncorrect}";
    }
}