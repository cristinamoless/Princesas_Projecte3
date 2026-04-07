using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Comanda
{
    public string clientName;
    public List<FlowerType> requiredFlowers;
    public int reward;
}
