using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI correctCounterText;
    [SerializeField] private TextMeshProUGUI incorrectCounterText;
    [SerializeField] private TextMeshProUGUI victimCounterText;

    private int totalCorrect = 0;
    private int totalIncorrect = 0;
    private int totalVictims = 0;
    private int processedVictims = 0;

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
        CountTotalVictims();
        UpdateCounterDisplay();
    }

    private void CountTotalVictims()
    {
        // Find all interactable victims in the scene
        InteractableSprite[] victims = FindObjectsByType<InteractableSprite>(FindObjectsSortMode.None);
        totalVictims = victims.Length;
    }

    public void IncrementCorrect()
    {
        totalCorrect++;
        processedVictims++;
        UpdateCounterDisplay();
    }

    public void IncrementIncorrect()
    {
        totalIncorrect++;
        processedVictims++;
        UpdateCounterDisplay();
    }

    private void UpdateCounterDisplay()
    {
        if (correctCounterText != null)
            correctCounterText.text = $"Correct: {totalCorrect}";
        if (incorrectCounterText != null)
            incorrectCounterText.text = $"Incorrect: {totalIncorrect}";
        if (victimCounterText != null)
            victimCounterText.text = $"Victims: {processedVictims}/{totalVictims}";
    }

    // Called when loading a new level/scene
    public void ResetCounters()
    {
        totalCorrect = 0;
        totalIncorrect = 0;
        processedVictims = 0;
        CountTotalVictims();
        UpdateCounterDisplay();
    }
}