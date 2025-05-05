using UnityEngine;
using TMPro;

public class VitalSignsInputValidator : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField pulseInput;
    [SerializeField] private TMP_InputField breathingInput;

    [Header("Validation Ranges")]
    [SerializeField] private int minPulse = 0;
    [SerializeField] private int maxPulse = 200;
    [SerializeField] private int minBreathing = 0;
    [SerializeField] private int maxBreathing = 40;

    private void Start()
    {
        if (pulseInput != null)
        {
            pulseInput.contentType = TMP_InputField.ContentType.IntegerNumber;
            pulseInput.onValueChanged.AddListener(ValidatePulse);
        }

        if (breathingInput != null)
        {
            breathingInput.contentType = TMP_InputField.ContentType.IntegerNumber;
            breathingInput.onValueChanged.AddListener(ValidateBreathing);
        }
    }

    private void ValidatePulse(string value)
    {
        if (string.IsNullOrEmpty(value))
            return;

        if (int.TryParse(value, out int pulseValue))
        {
            if (pulseValue < minPulse || pulseValue > maxPulse)
            {
                pulseInput.text = Mathf.Clamp(pulseValue, minPulse, maxPulse).ToString();
            }
        }
        else
        {
            pulseInput.text = "";
        }
    }

    private void ValidateBreathing(string value)
    {
        if (string.IsNullOrEmpty(value))
            return;

        if (int.TryParse(value, out int breathingValue))
        {
            if (breathingValue < minBreathing || breathingValue > maxBreathing)
            {
                breathingInput.text = Mathf.Clamp(breathingValue, minBreathing, maxBreathing).ToString();
            }
        }
        else
        {
            breathingInput.text = "";
        }
    }
}