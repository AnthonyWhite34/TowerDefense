using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HoverTextChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Text Settings")]
    [SerializeField] private TextMeshProUGUI buttonText;  // Reference to the Text component on the button
    [SerializeField] private string defaultText = "Default Text"; // Text to display normally
    [SerializeField] private string hoverText = "Hovered Text"; // Text to display on hover

    private void Start()
    {
        // Set the default text initially
        if (buttonText != null)
        {
            buttonText.text = defaultText;
        }
    }

    // Called when the mouse pointer enters the button's area
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.text = hoverText;
        }
    }

    // Called when the mouse pointer exits the button's area
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.text = defaultText;
        }
    }
}