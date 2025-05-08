using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button exitButton;

    private void Start()
    {
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitButtonClick);
        }
    }

    private void OnExitButtonClick()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ExitEarly();
        }
    }
}