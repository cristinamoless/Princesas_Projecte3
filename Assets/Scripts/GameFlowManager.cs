using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    public DadesComanda database;
    public UIOrderDisplay uiOrder;
    public BuyFlower buyFlower;
    public ComandaArea comandaArea;

    public GameObject repartidor;
    public GameObject dialeg;

    public Comanda currentComanda;
    public int currentDay = 1;
    private int comandaIndex = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartDay(1);
    }
    public void StartDay(int day)
    {
        currentDay = day;
        comandaIndex = 0;

        GetComanda();
        buyFlower.showFlowers();
        repartidor.SetActive(true);
    }
    public void BeginClients()
    {
        repartidor.SetActive(false);
    }
    public void TalkClients()
    {
        dialeg.SetActive(true);
    }
    public void GetComanda()
    {
        dialeg.SetActive(false);

        if (currentDay == 1)
            currentComanda = database.day1Orders[comandaIndex];
        else
            currentComanda = database.day2Orders[comandaIndex];

        uiOrder.ShowOrder(currentComanda);
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

        GetComanda();
        comandaArea.hasTalked = false;
    }
}
