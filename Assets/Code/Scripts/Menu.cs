using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI currencyUI;
    [SerializeField] private TextMeshProUGUI waveUI;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject shopMenu;
    [SerializeField] private GameObject youWon;

    private bool isMenuOpen = true;


    private void OnGUI() // Dont Change
    {
        currencyUI.text = LevelManager.Main.currency.ToString();
        waveUI.text = EnemySpawner.Instance.GetCurrentWave().ToString();
    }
    public void SetSelected()//Dont Change 
    {

    }
    //public void ShowMenu()
    //{
    //    Scene currentScene = SceneManager.GetActiveScene();
    //    if (currentScene.name != "MainMenu")
    //    {
    //        ToggleMenu();
    //    }
    //}

    public void ToggleMenu()
    {
        Debug.Log(anim.name);
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    public void ShowGameOverScreen()
    {
        if (shopMenu != null) shopMenu.SetActive(false);
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowYouWonScreen()
    {
        if (shopMenu != null) shopMenu.SetActive(false);
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (youWon != null) youWon.SetActive(true);
    }
    public void PlayNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

}