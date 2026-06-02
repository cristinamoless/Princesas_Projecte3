using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ComandaManager : MonoBehaviour
{
    public TableManager table;

    private GameFlowManager flow;
    public Comanda currentComanda;
    public SwitchScene switchScene;
    public GameObject confirmButton;

    void Start()
    {
        flow = FindFirstObjectByType<GameFlowManager>();
        currentComanda = flow.currentComanda;
        switchScene = FindFirstObjectByType<SwitchScene>();
    }

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
       AudioManager.Instance.Play("Clic");
       bool correct = CheckOrder();
        flow.lastOrderWasCorrect = correct;

        if (correct)
        {
            int reward = currentComanda.reward;
            PlayerStars.Instance.addStars(reward);
        }
        switchScene.closeBuild();
        table.ClearTable();
        flow.OnOrderConfirmed();
    
    }

    public void Update()
    {
        UpdateConfirmButton();
    }
    public void UpdateConfirmButton()
    {
        GameFlowManager flow = FindFirstObjectByType<GameFlowManager>();

        bool tutorial = (flow.currentDay == 1 && flow.comandaIndex == 0);

        List<FlowerType> tableFlowers = table.GetFlowersOnTable();
        if (tutorial && !CheckOrder()){
            confirmButton.SetActive(false);
        }
        else
        {
            confirmButton.SetActive(true);
        }
    }


}
