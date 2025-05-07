using UnityEngine;
using TMPro;

public class InteractableSprite : MonoBehaviour, IInteractable
{
    [SerializeField] private VitalSignsMenu vitalSignsMenu;
    [SerializeField] private TextMeshProUGUI interactPrompt;
    [SerializeField] private string promptText = "Press E to interact";
    [SerializeField] private Color assignedColor;
    
    [Header("Patient Details")]
    [SerializeField] private PatientInformation patientInfo;

    public Color AssignedColor => assignedColor;

    private bool _hasBeenInteracted = false;
    public bool HasBeenInteracted => _hasBeenInteracted;
    private bool canInteract = false;

    private int correctAttempts = 0;
    private int incorrectAttempts = 0;

    private void Start()
    {
        if (patientInfo == null)
        {
            patientInfo = GetComponent<PatientInformation>();
        }
    }

    public void Interact()
    {
        if (!canInteract || _hasBeenInteracted)
            return;

        if (vitalSignsMenu != null && patientInfo != null)
        {
            vitalSignsMenu.Show(this, patientInfo.GetPatientName(), patientInfo.GetPatientDescription(), patientInfo.GetVitalSigns());
            _hasBeenInteracted = true;
        }
    }

    public void IncrementCorrectAttempts()
    {
        correctAttempts++;
        Debug.Log($"{gameObject.name} correct attempts: {correctAttempts}");
    }

    public void IncrementIncorrectAttempts()
    {
        incorrectAttempts++;
        Debug.Log($"{gameObject.name} incorrect attempts: {incorrectAttempts}");
    }

    public void OnPlayerApproach()
    {
        if (_hasBeenInteracted)
            return;
            
        canInteract = true;
        if (interactPrompt != null)
            interactPrompt.text = promptText;
    }

    public void OnPlayerExit()
    {
        canInteract = false;
    }
}