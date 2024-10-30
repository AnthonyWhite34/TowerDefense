using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Main;

    public Transform startPoint;
    public Transform[] path;

    //public GameObject uiElements;
    //public EnemySpawner spawner;

    public int currency;


    private void Awake()
    {
        Main = this;
    }

    private void Start()
    {
        //if (spawner != null)//delete between if it dont work 
        //{
        //    spawner.gameObject.SetActive(false);
        //}//delete between if it dont work 
        currency = 100;
    }

    //public void StartGame()//delete between if it dont work 
    //{
    //    if(spawner != null)
    //    {
    //        spawner.gameObject.SetActive(true);
    //        spawner.Start();
    //    }
    //}//delete between if it dont work 

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

}
