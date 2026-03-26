using System.Collections.Generic;
using UnityEngine;

public class ComandaManager : MonoBehaviour
{
    public TableManager table;          
    //public PlayerStars playerStars;     

    public List<FlowerType> requiredFlowers; 
    //public int reward = 200;                que es calculin 

 
    public bool CheckOrder()
    {

        List<FlowerType> tableFlowers = table.GetFlowersOnTable();

        if (tableFlowers.Count != requiredFlowers.Count)
            return false; //fer que simplement no doni monedes

        List<FlowerType> temp = new List<FlowerType>(tableFlowers);

        foreach (FlowerType req in requiredFlowers)
        {
            if (!temp.Contains(req))
                return false;

            temp.Remove(req);
        }

        return true;
    }

    public void ConfirmOrder()
    {
        if (CheckOrder())
        {
          //  Debug.Log("Ram correcte! +" + reward + " estrelles");

            // Sumar estrelles
           // playerStars.AddStars(reward);

            // Netejar taula per al següent client
            table.ClearTable();
        }
        else
        {
            Debug.Log("Ram incorrecte! No guanyes estrelles.");
        }
    }
}
