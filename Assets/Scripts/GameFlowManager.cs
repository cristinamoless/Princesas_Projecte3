using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public DadesComanda database;
    public ComandaManager comandaManager;
    public UIOrderDisplay uiOrder;

    private int currentDay = 1;
    private int comandaIndex = 0;

    void Start()
    {
        StartDay(1);
    }

    public void StartDay(int day)
    {
        currentDay = day;
        comandaIndex = 0;
        LoadNextComanda();
    }

    public void LoadNextComanda()
    {
        Comanda next = null;

        if (currentDay == 1)
            next = database.day1Orders[comandaIndex];
        else if (currentDay == 2)
            next = database.day2Orders[comandaIndex];

        comandaManager.currentComanda = next;
        uiOrder.ShowOrder(next);
    }

    public void OnOrderConfirmed()
    {
        comandaIndex++;

        if (currentDay == 1 && comandaIndex >= database.day1Orders.Count)
        {
            Debug.Log("Dia 1 complet!");
            StartDay(2);
            return;
        }

        if (currentDay == 2 && comandaIndex >= database.day2Orders.Count)
        {
            Debug.Log("Dia 2 complet!");
            return;
        }

        LoadNextComanda();
    }
}
