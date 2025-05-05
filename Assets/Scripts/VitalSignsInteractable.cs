using UnityEngine;
using TMPro;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class VitalSignsInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private VitalSignsMenu menu;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private GameObject promptObject;
    [SerializeField] private string interactionPrompt = "Press E to check vital signs";

    private bool canInteract = false;

    private void Awake()
    {
        // Ensure prompt is hidden at start
        if (promptObject != null)
            promptObject.SetActive(false);

        // Make sure the collider is set as trigger
        var collider = GetComponent<Collider2D>();
        if (collider != null)
            collider.isTrigger = true;
    }

    private void Start()
    {
        if (promptObject != null)
            promptObject.SetActive(false);

        // Make sure the collider is set as trigger
        GetComponent<Collider2D>().isTrigger = true;

        // Verify menu reference
        if (menu == null)
        {
            Debug.LogError("VitalSignsMenu reference is missing on " + gameObject.name);
        }
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

        menu.Show();
        if (promptObject != null)
            promptObject.SetActive(false);
    }

    public void OnPlayerApproach()
    {
        Debug.Log("Player approached " + gameObject.name);
        canInteract = true;
        
        if (promptObject != null)
        {
            promptObject.SetActive(true);
            if (promptText != null)
                promptText.text = interactionPrompt;
        }
    }

    public void OnPlayerExit()
    {
        Debug.Log("Player exited " + gameObject.name);
        canInteract = false;
        
        if (promptObject != null)
            promptObject.SetActive(false);
    }
}