using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


[Serializable]
public class SlowTower : Tower
{
    public float SlowAmount; // Percentage to slow the enemy's speed
    public float SlowDuration; // Duration of the slow effect in seconds

    
    // Constructor for SlowTower
    public SlowTower(string _name, int _cost, GameObject _prefab, float _slowAmount, float _slowDuration)
        : base(_name, _cost, _prefab) // Call the base constructor to set common tower properties
    {
        SlowAmount = _slowAmount;
        SlowDuration = _slowDuration;
    }

    // Example of unique behavior: Apply slowing effect
    //public void ApplySlowEffect(EnemyBase enemy)
    //{
        
    //    if (enemy != null)
    //    {
    //        enemy.ApplySlow(SlowAmount, SlowDuration); // Assuming Enemy has an ApplySlow method
    //    }
    //}
}

//  delete class if it doesnt work. 
