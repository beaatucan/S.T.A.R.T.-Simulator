using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Timer Settings")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float gameDuration = 1200f; // 20 minutes in seconds
    private float remainingTime;
    private bool isTimerRunning;
    private bool isPaused;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeTimer();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeTimer()
    {
        remainingTime = gameDuration;
        isTimerRunning = true;
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (isTimerRunning && !isPaused)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerDisplay();

            if (remainingTime <= 0)
            {
                GameOver("Time's up!");
            }

            // Check if all victims have been found
            if (ScoreManager.Instance != null)
            {
                var (correct, _, total) = ScoreManager.Instance.GetScores();
                if (correct >= total && total > 0)
                {
                    GameOver("All victims have been found!");
                }
            }
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }

    public void GameOver(string message)
    {
        isTimerRunning = false;
        SetPaused(true);
        Debug.Log(message);
        SceneManager.LoadScene("EndGame");
    }

    public void ExitEarly()
    {
        GameOver("Early exit requested");
    }

    public void SetPaused(bool paused)
    {
        isPaused = paused;
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    // Reset game state when starting a new game
    public void ResetGame()
    {
        InitializeTimer();
        SetPaused(false);
    }
}