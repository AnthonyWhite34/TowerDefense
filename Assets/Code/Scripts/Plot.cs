using System.Xml.Serialization;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;


    private GameObject tower;
    private Color startColor;

    private void Start()
    {
        foreach (Transform GrassSquare in transform)
        {
            Plot plotComponent = GrassSquare.GetComponent<Plot>();
            if (plotComponent == null)
            {
                plotComponent = GrassSquare.gameObject.AddComponent<Plot>();
            }
        }
        if (sr == null) // Automatically assigns SpriteRenderer if not set in the Inspector
        {
            sr = GetComponent<SpriteRenderer>();
        }
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (tower != null) return;
        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if(towerToBuild.Cost > LevelManager.Main.currency)
        {
            Debug.Log("you can't afford this tower");
            return;
        }
        LevelManager.Main.SpendCurrency(towerToBuild.Cost);

        tower = Instantiate(towerToBuild.Prefab, transform.position, Quaternion.identity);
    }

}
