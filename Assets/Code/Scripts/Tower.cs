using System;
using UnityEngine;



[Serializable]
public class Tower
{
    public string Name;
    public int Cost;
    public GameObject Prefab;

    public Tower (string _name, int _cost, GameObject _prefab)
    {
        Name = _name;
        Cost = _cost;
        Prefab = _prefab;
    }

}
