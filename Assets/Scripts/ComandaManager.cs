using System.Collections.Generic;
using UnityEngine;

public class ComandaManager : MonoBehaviour
{
    public TableManager table;
    public PlayerStars playerStars;
    public GameFlowManager flow;

    public Comanda currentComanda;

    public bool CheckOrder()
    {
        List<FlowerType> tableFlowers = table.GetFlowersOnTable();

        if (tableFlowers.Count != currentComanda.requiredFlowers.Count)
            return false;

        List<FlowerType> temp = new List<FlowerType>(tableFlowers);

        foreach (FlowerType req in currentComanda.requiredFlowers)
        {
            bool found = false;

            foreach (FlowerType f in temp)
            {
                if (f.name == req.name)
                {
                    found = true;
                    temp.Remove(f);
                    break;
                }
            }

            if (!found)
                return false;
        }

        return true;
    }

    public void ConfirmOrder()
    {
        if (CheckOrder())
        {
            int reward = CalculateReward(currentComanda);
            playerStars.addStars(reward);

            Debug.Log("Ram correcte! +" + reward + " estrelles");

            table.ClearTable();
            flow.OnOrderConfirmed();
        }
        else
        {
            Debug.Log("Ram incorrecte! No guanyes estrelles.");
        }
    }
    public int CalculateReward(Comanda c)
    {
        int total = 0;

        foreach (var f in c.requiredFlowers)
            total += f.stars;

        return total;
    }
}
