using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI correctText;
    [SerializeField] private TextMeshProUGUI incorrectText;
    [SerializeField] private TextMeshProUGUI totalVictimsText;
    [SerializeField] private TextMeshProUGUI accuracyText;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);

        DisplayFinalScores();
    }

    private void DisplayFinalScores()
    {
        if (ScoreManager.Instance == null)
            return;

        var (correct, incorrect, total) = ScoreManager.Instance.GetScores();
        float accuracy = total > 0 ? (correct / (float)total) * 100 : 0;

        if (correctText != null)
            correctText.text = $"Correct Classifications: {correct}";
        if (incorrectText != null)
            incorrectText.text = $"Incorrect Classifications: {incorrect}";
        if (totalVictimsText != null)
            totalVictimsText.text = $"Total Victims Found: {correct + incorrect}/{total}";
        if (accuracyText != null)
            accuracyText.text = $"Accuracy: {accuracy:F1}%";
    }

    private void ReturnToMainMenu()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.ResetScores();
        SceneManager.LoadScene("StartMenu");
    }
}