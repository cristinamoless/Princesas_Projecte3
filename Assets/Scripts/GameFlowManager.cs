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
    public DialogueManager dialogueManagerRepartidor;
    public Dialogue currentDialogue;
    public Dialogue[] allDialogues;

    public CustomCursor cc;

    public bool lastOrderWasCorrect;


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
        currentDialogue = GetDialogue(currentDay, 0, DialogueType.Repartidor);
        dialogueManagerRepartidor.StartDialogue(currentDialogue);
    }
    public void BeginClients()
    {
        repartidor.SetActive(false);
    }
    public void TalkClients()
    {
        dialeg.SetActive(true);
        dialogueManager.isDialogueInici = true;
        currentDialogue = GetDialogue(currentDay, comandaIndex, DialogueType.Initial);
        dialogueManager.StartDialogue(currentDialogue);
    }
    public void GetComanda()
    {
        dialeg.SetActive(false);
        if (dialogueManager.isDialogueInici)
        {
            if (currentDay == 1)
                currentComanda = database.day1Orders[comandaIndex];
            else
                currentComanda = database.day2Orders[comandaIndex];
        
            uiOrder.ShowOrder(currentComanda);
        }
    }

    public void OnOrderConfirmed()
    {
        bool correct = lastOrderWasCorrect;

        comandaIndex++;
        currentComanda = null;
        uiOrder.ClearUI();

       Dialogue result = GetDialogue(
            currentDay,
            comandaIndex - 1,
            correct ? DialogueType.Happy : DialogueType.Sad
        );

        
        dialogueManager.isDialogueInici = false;
        dialeg.SetActive(true);
        dialogueManager.StartDialogue(result);

        if (currentDay == 1 && comandaIndex >= database.day1Orders.Count)
        {
            fiDia.SetActive(true);
            return;
        }

        comandaArea.hasTalked = false;
        cc.SetCursor();
    }


    public Dialogue GetDialogue(int day, int index, DialogueType type)
    {
        foreach (Dialogue d in allDialogues)
        {
            if (d.day == day && d.orderIndex == index && d.type == type)
                return d;
        }

        return null;
    }


}
