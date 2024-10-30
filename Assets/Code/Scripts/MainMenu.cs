using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        
        SceneManager.LoadScene("Level1"); //Change to SceneManager.LoadSceneAsync(1); if code isnt working
        //LevelManager levelManager = FindFirstObjectByType<LevelManager>(); // Delete if it dont work
        //if(levelManager != null )
        //{
        //    levelManager.StartGame();
        //}
        //Delete if it dont work

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
