using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class VitalSignsMenu : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button continueButton;
    [SerializeField] private CanvasGroup menuCanvasGroup;
    [SerializeField] private ColorSelectionUI colorSelection;
    [SerializeField] private GameObject menuPanel;

    [Header("UI Text Elements")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Transform vitalSignsContainer;
    [SerializeField] private TextMeshProUGUI vitalSignTextPrefab;

    [Header("Feedback UI")]
    [SerializeField] private GameObject correctFeedback;
    [SerializeField] private GameObject incorrectFeedback;
    [SerializeField] private float feedbackDisplayTime = 1.5f;

    private PlayerController playerController;
    private bool isOpen = false;
    private InteractableSprite currentSource;
    private VitalSignsInteractable currentInteractable;

    private void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        InitializeComponents();
        SetInitialState();
    }

    private void Start()
    {
        Hide();
    }

    private void InitializeComponents()
    {
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
            continueButton.interactable = true;

        if (correctFeedback != null) correctFeedback.SetActive(false);
        if (incorrectFeedback != null) incorrectFeedback.SetActive(false);

        if (colorSelection != null)
            colorSelection.ResetSelection();
    }

    private void HandleSubmit()
    {
        if (currentSource != null && colorSelection != null)
        {
            Color? selectedColor = colorSelection.GetSelectedColor();
            if (!selectedColor.HasValue)
            {
                Debug.Log("No color selected!");
                return;
            }

            bool isCorrect = ColorMatches(selectedColor.Value, currentSource.AssignedColor);
            ShowFeedback(isCorrect);
            StartCoroutine(HideAfterFeedback(isCorrect));
            
            // Disable the interactable
            if (currentInteractable != null)
            {
                currentInteractable.enabled = false;
            }
        }
        else
        {
            Hide();
        }
    }

    private void ShowFeedback(bool isCorrect)
    {
        if (isCorrect)
        {
            if (correctFeedback != null)
            {
                correctFeedback.SetActive(true);
                incorrectFeedback?.SetActive(false);
            }
            ScoreManager.Instance?.IncrementCorrect();
            Debug.Log("Correct color selected!");
        }
        else
        {
            if (incorrectFeedback != null)
            {
                incorrectFeedback.SetActive(true);
                correctFeedback?.SetActive(false);
            }
            ScoreManager.Instance?.IncrementIncorrect();
            Debug.Log("Incorrect color selected.");
        }
    }

    private System.Collections.IEnumerator HideAfterFeedback(bool wasCorrect)
    {
        yield return new WaitForSeconds(feedbackDisplayTime);
        Hide();
    }

    public void Show(InteractableSprite interactable, string title, string description, string[] vitalSigns)
    {
        if (isOpen) return;

        currentSource = interactable;
        currentInteractable = interactable.GetComponent<VitalSignsInteractable>();
        
        // Update UI texts
        if (titleText != null)
            titleText.text = title;
        if (descriptionText != null)
            descriptionText.text = description;

        // Clear existing vital signs
        if (vitalSignsContainer != null)
        {
            foreach (Transform child in vitalSignsContainer)
            {
                Destroy(child.gameObject);
            }

            // Add new vital signs
            if (vitalSignTextPrefab != null)
            {
                foreach (string vitalSign in vitalSigns)
                {
                    TextMeshProUGUI vitalSignText = Instantiate(vitalSignTextPrefab, vitalSignsContainer);
                    vitalSignText.text = vitalSign;
                }
            }
        }

        Debug.Log("Showing Vitals Menu");
        isOpen = true;
        ShowMenuCanvas();

        if (colorSelection != null)
            colorSelection.ResetSelection();

        if (playerController != null)
            playerController.DisableMovementInput();

        GameManager.Instance.SetPaused(true);
    }

    public void Hide()
    {
        if (!isOpen) return;

        Debug.Log("Hiding Vitals Menu");
        isOpen = false;

        if (menuCanvasGroup != null)
        {
            menuCanvasGroup.alpha = 0f;
            menuCanvasGroup.interactable = false;
            menuCanvasGroup.blocksRaycasts = false;
        }

        if (correctFeedback != null) correctFeedback.SetActive(false);
        if (incorrectFeedback != null) incorrectFeedback.SetActive(false);

        if (colorSelection != null)
            colorSelection.ResetSelection();

        gameObject.SetActive(false);

        if (playerController != null)
            playerController.EnableMovementInput();

        GameManager.Instance.SetPaused(false);
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

    private bool ColorMatches(Color a, Color b)
    {
        // Compare colors with a small tolerance for floating-point differences
        const float tolerance = 0.01f;
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance &&
               Mathf.Abs(a.a - b.a) < tolerance;
    }
}