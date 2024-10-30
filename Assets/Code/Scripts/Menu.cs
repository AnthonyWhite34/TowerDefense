using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = true;

    public GameObject menu;

    private void Start()// might want to remove.
    {
        ToggleMenu();
        ShowMenu(); // Shows menu once the a level has been called 
        
    }

    public void ShowMenu() // might want to remove
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "MainMenu")
        {
            ToggleMenu();
            //menu.SetActive(true);
        }
        
    }// might want to remove 

    public void ToggleMenu()
    {
        
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
        
        
    }

    private void OnGUI()
    {
        currencyUI.text = LevelManager.Main.currency.ToString();
    }
    public void SetSelected()
    {

    }
}
