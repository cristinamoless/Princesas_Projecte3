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
    public GameObject fiDia;

    public Comanda currentComanda;
    public int currentDay = 1;
    private int comandaIndex = 0;

    public DialogueManager dialogueManager;
    public Dialogue currentDialogue;
    public Dialogue[] day1Dialogues;
    public Dialogue[] day2Dialogues;

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

        if (currentDay == 1)
            currentDialogue = day1Dialogues[comandaIndex];
        else
            currentDialogue = day2Dialogues[comandaIndex];

        dialogueManager.StartDialogue(currentDialogue);
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
        currentComanda = null;
        uiOrder.ClearUI();

        if (currentDay == 1 && comandaIndex >= database.day1Orders.Count)
        {
            fiDia.SetActive(true);
            return;
        }

        if (currentDay == 2 && comandaIndex >= database.day2Orders.Count)
            return;

        comandaArea.hasTalked = false;
    }
}
