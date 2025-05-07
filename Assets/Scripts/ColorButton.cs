using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class ColorButton : MonoBehaviour
{
    [SerializeField] private Color color = Color.white;
    [SerializeField] private Image colorImage;

    public UnityEvent<Color> OnColorSelected = new UnityEvent<Color>();

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);

        if (colorImage != null)
        {
            colorImage.color = color;
        }
    }

    private void HandleClick()
    {
        OnColorSelected.Invoke(color);
    }

    public void SetColor(Color newColor)
    {
        color = newColor;
        if (colorImage != null)
        {
            colorImage.color = color;
        }
    }
}