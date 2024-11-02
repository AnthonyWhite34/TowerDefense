using UnityEngine;

public class UIDebugger : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements;  // Assign your UI GameObjects in the inspector

    public void CheckUIActiveStatus()
    {
        foreach (GameObject uiElement in uiElements)
        {
            Debug.Log($"{uiElement.name} is {(uiElement.activeSelf ? "Active" : "Inactive")}");
        }
    }

    // Call this method where you need to check the UI status, such as in Update or on a button press
}