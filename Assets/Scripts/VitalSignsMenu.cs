using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class VitalSignsMenu : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ColorSelectionUI colorSelection;
    [SerializeField] private VitalSignsInputValidator inputValidator;
    [SerializeField] private Button continueButton;
    [SerializeField] private CanvasGroup menuCanvasGroup;

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField pulseInput;
    [SerializeField] private TMP_InputField breathingInput;

    public event Action<VitalSignsData> OnVitalSignsSubmitted;
    private bool isOpen = false;
    
    private void Awake()
    {
        InitializeComponents();
        SetInitialState();
    }

    private void Start()
    {
        Hide();
    }

    private void InitializeComponents()
    {
        if (pulseInput != null)
            pulseInput.onValueChanged.AddListener(_ => ValidateInputs());
        
        if (breathingInput != null)
            breathingInput.onValueChanged.AddListener(_ => ValidateInputs());
        
        if (colorSelection != null)
            colorSelection.OnColorSelected += _ => ValidateInputs();

        if (continueButton != null)
            continueButton.onClick.AddListener(HandleSubmit);
    }

    private void SetInitialState()
    {
        if (menuCanvasGroup != null)
        {
            menuCanvasGroup.alpha = 0f;
            menuCanvasGroup.interactable = false;
            menuCanvasGroup.blocksRaycasts = false;
        }
        
        if (continueButton != null)
            continueButton.interactable = false;

        isOpen = false;
    }

    private void ValidateInputs()
    {
        bool isPulseValid = !string.IsNullOrEmpty(pulseInput?.text);
        bool isBreathingValid = !string.IsNullOrEmpty(breathingInput?.text);
        bool hasColorSelected = colorSelection != null && colorSelection.HasSelection();

        if (continueButton != null)
            continueButton.interactable = isPulseValid && isBreathingValid && hasColorSelected;
    }

    private void HandleSubmit()
    {
        if (!continueButton.interactable) return;

        var data = new VitalSignsData
        {
            Pulse = int.Parse(pulseInput.text),
            Breathing = int.Parse(breathingInput.text),
            SelectedColor = colorSelection.GetSelectedColor() ?? Color.white
        };

        // First notify listeners
        OnVitalSignsSubmitted?.Invoke(data);

        // Then close menu
        Hide();
    }

    public void Show()
    {
        if (isOpen) return;

        Debug.Log("Showing Vitals Menu");
        isOpen = true;

        // Reset inputs first
        ResetInputs();
        
        // Then show and enable canvas
        ShowMenuCanvas();

        // Notify player to pause
        GameManager.Instance.SetPaused(true);
    }

    private void ResetInputs()
    {
        if (pulseInput != null)
            pulseInput.text = "";
        if (breathingInput != null)
            breathingInput.text = "";
        if (colorSelection != null)
            colorSelection.ResetSelection();
        if (continueButton != null)
            continueButton.interactable = false;
    }

    private void ShowMenuCanvas()
    {
        gameObject.SetActive(true);
        
        if (menuCanvasGroup != null)
        {
            menuCanvasGroup.alpha = 1f;
            menuCanvasGroup.interactable = true;
            menuCanvasGroup.blocksRaycasts = true;
        }
    }

    public void Hide()
    {
        if (!isOpen) return;

        Debug.Log("Hiding Vitals Menu");
        isOpen = false;

        // First hide the canvas
        if (menuCanvasGroup != null)
        {
            menuCanvasGroup.alpha = 0f;
            menuCanvasGroup.interactable = false;
            menuCanvasGroup.blocksRaycasts = false;
        }

        // Then deactivate the GameObject
        gameObject.SetActive(false);

        // Finally unpause the game
        GameManager.Instance.SetPaused(false);
    }
}

[System.Serializable]
public struct VitalSignsData
{
    public int Pulse;
    public int Breathing;
    public Color SelectedColor;
}