using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public List<FlowerType> GetFlowersOnTable()
    {
        List<FlowerType> bouquet = new List<FlowerType>();

        foreach (Transform child in transform)
        {
            Flower f = child.GetComponent<Flower>();
            if (f != null)
                bouquet.Add(f.flowerType);
        }

        return bouquet;
    }

    public void ClearTable()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }



}
