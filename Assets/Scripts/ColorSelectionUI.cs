using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private float selectedScale = 1.2f;
    
    private ColorOption currentSelection;

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
    }

    public void ResetSelection()
    {
        if (currentSelection != null)
        {
            currentSelection.button.transform.localScale = Vector3.one;
            currentSelection = null;
        }
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