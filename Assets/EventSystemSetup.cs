using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemSetup : MonoBehaviour
{
    void Awake()
    {
        if (FindFirstObjectByType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }
    }
}
