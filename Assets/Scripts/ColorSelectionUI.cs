using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ColorSelectionUI : MonoBehaviour
{
    [System.Serializable]
    public class ColorOption
    {
        public string name;
        public Color color;
        public Button button;
    }

    [SerializeField] private ColorOption[] colorOptions;
    [SerializeField] private Image selectedIndicator;
    [SerializeField] private TextMeshProUGUI selectedText;
    [SerializeField] private float selectedScale = 1.2f;
    
    private ColorOption currentSelection;
    public event Action<Color> OnColorSelected;

    private void Start()
    {
        foreach (var option in colorOptions)
        {
            // Set the button's color
            var buttonImage = option.button.GetComponent<Image>();
            if (buttonImage != null)
                buttonImage.color = option.color;

            // Add click listener
            option.button.onClick.AddListener(() => SelectColor(option));
        }

        // Reset selection state
        ResetSelection();
    }

    private void SelectColor(ColorOption option)
    {
        // Reset previous selection
        if (currentSelection != null)
        {
            currentSelection.button.transform.localScale = Vector3.one;
        }

        // Update current selection
        currentSelection = option;
        currentSelection.button.transform.localScale = Vector3.one * selectedScale;

        // Update UI
        if (selectedText != null)
            selectedText.text = $"{option.name} Selected";
        
        if (selectedIndicator != null)
            selectedIndicator.color = option.color;

        // Notify listeners
        OnColorSelected?.Invoke(option.color);
    }

    public void ResetSelection()
    {
        if (currentSelection != null)
        {
            currentSelection.button.transform.localScale = Vector3.one;
            currentSelection = null;
        }

        if (selectedText != null)
            selectedText.text = "No Color Selected";
            
        if (selectedIndicator != null)
            selectedIndicator.color = Color.white;
    }

    public bool HasSelection()
    {
        return currentSelection != null;
    }

    public Color? GetSelectedColor()
    {
        return currentSelection?.color;
    }
}