using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1); //Change to SceneManager.LoadSceneAsync(1); if code isnt working
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
