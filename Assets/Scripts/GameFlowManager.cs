using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameFlowManager : MonoBehaviour
{
    public DadesComanda database;
    public OrderDisplay uiOrder;
    public BuyFlower buyFlower;
    public ComandaArea comandaArea;

    public GameObject repartidor;
    public GameObject dialeg;
    public GameObject fiDia;
    public GameObject date;
    public GameObject toDo;
    public GameObject notEnough;

    public Comanda currentComanda;
    public int currentDay = 1;
    public int comandaIndex = 0;

    public DialogueManager dialogueManager;
    public DialogueManager dialogueManagerRepartidor;
    public Dialogue currentDialogue;
    public Dialogue[] allDialogues;

    public CustomCursor cc;

    public bool lastOrderWasCorrect;
    private bool waitingForFinalDialogue = false;

    public List<CompletedOrderInfo> completedOrders = new List<CompletedOrderInfo>();
    public bool firstOrderCompleted = false;
    private bool deliverySeenThisRun = false;
    private bool isDeliveryDialogue = false;
    public Transform playerSpawn;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentDay = 0;
        StartDay();
    }

    public void StartDay()
    {

        currentDay++;
        comandaIndex = 0;
        deliverySeenThisRun = false;
        RespawnPlayer();
        SetupDayNPCs();
        
        date.SetActive(true);
        fiDia.SetActive(false);
        toDo.SetActive(false);

        buyFlower.showFlowers();
        if (!deliverySeenThisRun)
        {
            deliverySeenThisRun = true;

            repartidor.SetActive(true);

            currentDialogue = GetDialogue(currentDay, 0, DialogueType.Repartidor);

            isDeliveryDialogue = true;
            dialogueManagerRepartidor.StartDialogue(currentDialogue);
        }
        else
        {
            repartidor.SetActive(false);
            
        }
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

        isDeliveryDialogue = false;
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
            toDo.SetActive(true);
        }
    }

    public void OnOrderConfirmed()
    {
        bool correct = lastOrderWasCorrect;

        var list = currentDay == 1 ? database.day1Orders : database.day2Orders;

        completedOrders.Add(new CompletedOrderInfo
        {
            comanda = list[comandaIndex],
            wasCorrect = correct
        });

        comandaIndex++;

        if (currentDay == 1 && comandaIndex == 1)
        {
            firstOrderCompleted = true;
        }

        currentComanda = null;
        uiOrder.ClearUI();
        toDo.SetActive(false);



        Dialogue result = null;

        if (correct)
        {
            result = GetDialogue(currentDay, comandaIndex - 1, DialogueType.Choice);

            if (result == null)
                result = GetDialogue(currentDay, comandaIndex - 1, DialogueType.Happy);
        }
        else
        {
            result = GetDialogue(currentDay, comandaIndex - 1, DialogueType.Sad);
        }

        dialogueManager.isDialogueInici = false;
        dialeg.SetActive(true);

        isDeliveryDialogue = false;
        dialogueManager.StartDialogue(result);


        if (comandaIndex >= list.Count)
        {
            waitingForFinalDialogue = true;
        }


        comandaArea.hasTalked = false;
        cc.SetCursor();
    }

    public void OnDialogueEnded()
    {
        if (isDeliveryDialogue)
        {
            isDeliveryDialogue = false;
            return;
        }

        if (!dialogueManager.isDialogueInici && !waitingForFinalDialogue)
        {
            var npcManager = FindFirstObjectByType<NPCManager>();
            if (npcManager != null)
                npcManager.MakeCurrentClientLeave();
        }

        if (waitingForFinalDialogue)
        {
            EndDay();
        }
    }

    public void EndDay()
    {
        bool hasEnoughStars = PlayerStars.Instance.totalStars >= GetMinimumStarsForNextDay();

        if (CheckGameEnd())
        {
            StopAllFlow();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
            return;
        }

        if (!hasEnoughStars)
        {
            StopAllFlow();
            notEnough.SetActive(true);
            currentDay--;
            return;
        }

        fiDia.SetActive(true);
        date.SetActive(false);
        dialeg.SetActive(false);

        uiOrder.ShowEndOfDay(completedOrders);
        waitingForFinalDialogue = false;

        comandaArea.hasTalked = false;
        completedOrders.Clear();
        lastOrderWasCorrect = false;

        var timeManager = FindFirstObjectByType<TimeManager>();
        timeManager.ResetDay();

        SetupDayNPCs();
        RespawnPlayer();
    }
    private void StopAllFlow()
    {
        comandaArea.hasTalked = false;
        waitingForFinalDialogue = false;
    }
    private bool CheckGameEnd()
    {
        if (currentDay >= 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
            return true;
        }

        return false;
    }

    private void SetupDayNPCs()
    {
        var npcManager = FindFirstObjectByType<NPCManager>();

        if (npcManager == null) return;

        if (currentDay == 1)
            npcManager.SetClients(npcManager.day1Clients);
        else
            npcManager.SetClients(npcManager.day2Clients);

        npcManager.ResetToFirstClient();
        npcManager.StartFirstClient();
    }
    private int GetMinimumStarsForNextDay()
    {
        int total = 0;

        foreach (var flower in buyFlower.allFlowers)
        {
            if (flower.availableDay == currentDay + 1 && !flower.unlocked)
                total += flower.seedPrice;
        }

        return total;
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

    private void RespawnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null) return;

        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null)
        {
            cc.enabled = false;
            player.transform.position = playerSpawn.position;
            player.transform.rotation = playerSpawn.rotation;
            cc.enabled = true;
        }
        else
        {
            player.transform.position = playerSpawn.position;
            player.transform.rotation = playerSpawn.rotation;
        }
    }
    public void Update()
    {
        if (notEnough.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                notEnough.SetActive(false);
            }
        }
    }
    
    
}
