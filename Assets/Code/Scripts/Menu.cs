using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI currencyUI;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject gameOverScreen; // Game Over reference
    [SerializeField] private GameObject shopMenu;

    private bool isMenuOpen = true;

    private void Start()
    {
        ToggleMenu();
        ShowMenu(); // Show menu when a level loads, if needed
    }

    private void Update()
    {
        if (currencyUI != null && LevelManager.Main != null)
        {
            currencyUI.text = LevelManager.Main.currency.ToString();
        }
    }

    public void ShowMenu()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "MainMenu")
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu() //for the shop menu animation
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    public void ShowGameOverScreen() //might need to delete.
    {
        
        if (shopMenu != null) shopMenu.SetActive(false);
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
    }

    public void SetSelected()
    {
        // Implement selection logic here, if needed
    }
}