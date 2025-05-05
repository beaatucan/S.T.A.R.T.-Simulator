using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button continueButton;
    [SerializeField] private string welcomeMessage = "Welcome to the level! Get ready for your mission.";
    
    private void Start()
    {
        ShowWelcomePopup();
    }

    private void ShowWelcomePopup()
    {
        popupPanel.SetActive(true);
        messageText.text = welcomeMessage;
        Time.timeScale = 0f; // Pause the game while popup is shown
    }

    public void OnContinueButtonClick()
    {
        popupPanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
}