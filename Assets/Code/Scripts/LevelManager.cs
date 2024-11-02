using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Main;

    public Transform startPoint;
    public Transform[] path;


    public int currency;
    public static bool gameOver = false;

    



    private void Awake()
    {
        Main = this;
    }

    private void Start()
    {
        currency = 100;
    }



    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }
    public bool SpendCurrency(int amount)
    {
        if (amount <=  currency)
        {
            // Buy item
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have  enough to purchase this item");
            return false;
        }
    }
    //When called it will end the game. need to call from Base Health
    public void EndGame()
    {
        Debug.Log("EndGame Called in Level manager");
        gameOver = true;
        EnemySpawner enemySpawner = gameObject.GetComponent<EnemySpawner>();
        enemySpawner.StopSpawning();
        // now we need to hid the Menu and Show the GameOVerScreen    need to get from menu .ShowGameOverScreen() 
        gameObject.GetComponent<Menu>().ShowGameOverScreen(); // calls the gameoverscreen and sets it as active while deactivating the other menu. 
    }
}
