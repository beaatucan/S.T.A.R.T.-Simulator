using UnityEngine;
using TMPro;

public class InteractableSprite : MonoBehaviour, IInteractable
{
    [SerializeField] private VitalSignsMenu vitalSignsMenu;
    [SerializeField] private TextMeshProUGUI interactPrompt;
    [SerializeField] private string promptText = "Press E to interact";

    private void Start()
    {
        if (interactPrompt != null)
            interactPrompt.gameObject.SetActive(false);
    }

    public void Interact()
    {
        if (vitalSignsMenu != null)
        {
            vitalSignsMenu.Show();
            if (interactPrompt != null)
                interactPrompt.gameObject.SetActive(false);
        }
    }

    public void OnPlayerApproach()
    {
        if (interactPrompt != null)
        {
            interactPrompt.text = promptText;
            interactPrompt.gameObject.SetActive(true);
        }
    }

    public void OnPlayerExit()
    {
        if (interactPrompt != null)
            interactPrompt.gameObject.SetActive(false);
    }
}