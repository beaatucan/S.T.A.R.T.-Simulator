using UnityEngine;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(InteractableSprite))]
public class VitalSignsInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private VitalSignsMenu menu;

    [Header("Interaction UI")]
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private GameObject popupBackground;
    [SerializeField] private string interactionPrompt = "Press E to check vital signs";

    [Header("Patient Information")]
    [SerializeField] private string menuTitle = "Patient Status";
    [SerializeField, TextArea(3, 10)] private string menuDescription = "Examine the patient's vital signs carefully.";
    [SerializeField] private string[] vitalSigns = new string[] { 
        "Blood Pressure: --/--",
        "Heart Rate: -- bpm",
        "Respiratory Rate: -- /min",
        "Temperature: --°C",
        "SpO2: --%"
    };

    private bool canInteract = false;
    private InteractableSprite interactableSprite;
    private bool hasInteracted = false;

    private void Awake()
    {
        // Make sure the collider is set as trigger
        var collider = GetComponent<Collider2D>();
        if (collider != null)
            collider.isTrigger = true;

        // Get reference to InteractableSprite
        interactableSprite = GetComponent<InteractableSprite>();
        
        // Hide UI elements at start
        HideUI();
    }

    private void Start()
    {
        // Verify menu reference
        if (menu == null)
        {
            Debug.LogError("VitalSignsMenu reference is missing on " + gameObject.name);
        }
    }

    private void HideUI()
    {
        if (promptText != null)
            promptText.gameObject.SetActive(false);
        if (popupBackground != null)
            popupBackground.SetActive(false);
    }

    private void ShowUI()
    {
        if (promptText != null)
        {
            promptText.gameObject.SetActive(true);
            promptText.text = interactionPrompt;
        }
        if (popupBackground != null)
            popupBackground.SetActive(true);
    }

    public void Interact()
    {
        if (!canInteract) return;
        
        Debug.Log("Interact called on " + gameObject.name);
        
        if (menu == null)
        {
            Debug.LogError("Cannot interact - VitalSignsMenu is null on " + gameObject.name);
            return;
        }

        menu.Show(interactableSprite, menuTitle, menuDescription, vitalSigns);
        HideUI(); // Hide the popup when interacting
    }

    public void OnPlayerApproach()
    {
        // Check both local and InteractableSprite interaction states
        if (hasInteracted || interactableSprite == null || interactableSprite.HasBeenInteracted || !enabled)
            return;
            
        Debug.Log("Player approached " + gameObject.name);
        canInteract = true;
        ShowUI();
    }

    public void OnPlayerExit()
    {
        Debug.Log("Player exited " + gameObject.name);
        canInteract = false;
        HideUI();
    }
}