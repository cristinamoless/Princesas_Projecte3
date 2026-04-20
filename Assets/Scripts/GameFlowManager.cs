using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public DadesComanda database;
    public ComandaManager comandaManager;
    public UIOrderDisplay uiOrder;

    public GameObject repartidor;
    public BuyFlower buyFlower;
    public GameObject dialeg;
    public ComandaArea comandaArea;

    public int currentDay = 1;
    private int comandaIndex = 0;
    public static bool start = true;

    void Start()
    {
        if (start)
        {
            StartDay(1);
            start = false;
        }
    }

    public void StartDay(int day)
    {
        currentDay = day;
        comandaIndex = 0;

        buyFlower.showFlowers();
        repartidor.SetActive(true);
    }
    public void BeginClients()
    {
        repartidor.SetActive(false);
    }
    public void GetComanda()
    {
        dialeg.SetActive(true);
    }

    public void LoadNextComanda()
    {
        dialeg.SetActive(false);
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
            StartDay(2);
            return;
        }

        if (currentDay == 2 && comandaIndex >= database.day2Orders.Count)
            return;

        LoadNextComanda();
        comandaArea.hasTalked = false;
    }
}
