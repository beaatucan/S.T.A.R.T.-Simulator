using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI correctCounterText;
    [SerializeField] private TextMeshProUGUI incorrectCounterText;
    [SerializeField] private TextMeshProUGUI victimCounterText;

    private int correctCount = 0;
    private int incorrectCount = 0;
    private int totalVictims = 0;

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
        UpdateUI();
    }

    private void CountTotalVictims()
    {
        // Find all interactable victims in the scene
        InteractableSprite[] victims = FindObjectsByType<InteractableSprite>(FindObjectsSortMode.None);
        totalVictims = victims.Length;
    }

    public void IncrementCorrect()
    {
        correctCount++;
        UpdateUI();
    }

    public void IncrementIncorrect()
    {
        incorrectCount++;
        UpdateUI();
    }

    public void SetTotalVictims(int count)
    {
        totalVictims = count;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (correctCounterText != null)
            correctCounterText.text = $"Correct: {correctCount}";
        if (incorrectCounterText != null)
            incorrectCounterText.text = $"Incorrect: {incorrectCount}";
        if (victimCounterText != null)
            victimCounterText.text = $"Victims Found: {correctCount + incorrectCount}/{totalVictims}";

        // Check if all victims have been found
        if (correctCount + incorrectCount >= totalVictims && totalVictims > 0)
        {
            // All victims have been processed, end the game
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndGame");
        }
    }

    public (int correct, int incorrect, int total) GetScores()
    {
        return (correctCount, incorrectCount, totalVictims);
    }

    public void ResetScores()
    {
        correctCount = 0;
        incorrectCount = 0;
        totalVictims = 0;
        UpdateUI();
    }

    // Called when loading a new level/scene
    public void ResetCounters()
    {
        correctCount = 0;
        incorrectCount = 0;
        totalVictims = 0;
        CountTotalVictims();
        UpdateUI();
    }
}