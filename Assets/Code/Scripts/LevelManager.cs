using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;


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

}
