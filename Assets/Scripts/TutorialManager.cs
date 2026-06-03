using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;
    
    private List<TutorialArrow> activeArrows = new List<TutorialArrow>();

    public enum Step
    {
        None,
        MoveInShop,
        GoToWindow,
        PressFToTalk,
        GoToTable,
        PressFToBuild,
        DragFirstRose,
        CutRose,
        RotateRose,
        ChooseBow,
        DeleteRose,
        MakeFourRoses,
        ConfirmBouquet,
        EndTutorial
    }

    public Step currentStep = Step.None;
    private bool isWaitingTimer = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        CheckAndStart();
    }

    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        FindUIReferences();
        if (currentStep != Step.None)
        {
            ShowStepUI();
        }
    }

    void FindUIReferences()
    {
        if (tutorialPanel == null || tutorialText == null)
        {
            Canvas[] canvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
            foreach (Canvas canvas in canvases)
            {
                Transform panelTransform = canvas.transform.Find("TutorialPanel"); 
                if (panelTransform != null)
                {
                    tutorialPanel = panelTransform.gameObject;
                    tutorialText = tutorialPanel.GetComponentInChildren<TextMeshProUGUI>();
                    break;
                }
            }
        }
    }

    void CheckAndStart()
    {
        var flow = FindFirstObjectByType<GameFlowManager>();
        if (flow != null && flow.currentDay == 1 && flow.comandaIndex == 0)
        {
            StartTutorial();
        }
        else
        {
            if (tutorialPanel != null) tutorialPanel.SetActive(false);
            currentStep = Step.None;
        }
    }

    public void RegisterArrow(TutorialArrow arrow)
    {
        if (!activeArrows.Contains(arrow))
            activeArrows.Add(arrow);
            
        UpdateArrowVisibility(arrow);
    }

    public void UnregisterArrow(TutorialArrow arrow)
    {
        if (activeArrows.Contains(arrow))
            activeArrows.Remove(arrow);
    }

    public void ShowArrow(int id)
    {
        foreach (var arrow in activeArrows)
        {
            if (arrow != null)
                arrow.gameObject.SetActive(arrow.arrowID == id);
        }
    }

    void UpdateArrowVisibility(TutorialArrow arrow)
    {
        int requiredID = GetCurrentArrowID();
        arrow.gameObject.SetActive(arrow.arrowID == requiredID);
    }

    int GetCurrentArrowID()
    {
        switch (currentStep)
        {
            case Step.GoToWindow: return 0;
            case Step.PressFToTalk: return 1;
            case Step.GoToTable: return 2;
            case Step.PressFToBuild: return 3;
            case Step.DragFirstRose: return 4;
            case Step.CutRose: return 5;
            case Step.RotateRose: return 6;
            case Step.ChooseBow: return 7;
            case Step.DeleteRose: return 8;
            case Step.ConfirmBouquet: return 9;
            default: return -1;
        }
    }

    public void StartTutorial()
    {
        currentStep = Step.MoveInShop;
        FindUIReferences();
        ShowStepUI();
    }

    public void NextStep()
    {
        currentStep++;
        ShowStepUI();
    }

    void ShowStepUI()
    {
        if (tutorialPanel == null) FindUIReferences();
        if (tutorialPanel == null) return; 

        tutorialPanel.SetActive(true);

        switch (currentStep)
        {
            case Step.MoveInShop:
                tutorialText.text = "Per moure’t dins la botiga utilitza WASD o ← ↑ →.";
                ShowArrow(-1);
                break;

            case Step.GoToWindow:
                tutorialText.text = "Hi ha un client! Apropa’t a la finestra.";
                ShowArrow(0);
                break;

            case Step.PressFToTalk:
                tutorialText.text = "Prem la tecla F per parlar amb el client :)";
                ShowArrow(1);
                break;

            case Step.GoToTable:
                tutorialText.text = "Recorda la comanda i apropa’t a la taula de jardineria.";
                ShowArrow(2);
                break;

            case Step.PressFToBuild:
                tutorialText.text = "Prem la tecla F per començar a muntar el ram!";
                ShowArrow(3);
                break;

            case Step.DragFirstRose:
                tutorialText.text = "Amb aquesta eina podràs agafar flors i moure-les pel taulell. Arrossega una rosa!";
                ShowArrow(4);
                break;

            case Step.CutRose:
                tutorialText.text = "Aquesta eina serveix per tallar les fulles i espines de les flors! Fes clic sobre la rosa!";
                ShowArrow(5);
                break;

            case Step.RotateRose:
                tutorialText.text = "Gira les flors a l’angle que més t’agradi. Fes clic sobre la rosa i mou el ratolí!";
                ShowArrow(6);
                break;

            case Step.ChooseBow:
                tutorialText.text = "Pots acabar de decorar el ram amb un llaç! Fes clic sobre el llaç i escull el color!";
                ShowArrow(7);
                break;

            case Step.DeleteRose:
                tutorialText.text = "Si algo et molesta ho pots esborrar amb aquesta eina! Fes clic sobre la rosa!";
                ShowArrow(8);
                break;

            case Step.MakeFourRoses:
                tutorialText.text = "Aquest client vol un ram de roses! Així que agafa quatre roses i posa’t creatiu!";
                ShowArrow(-1);
                break;

            case Step.ConfirmBouquet:
                tutorialText.text = "Si al client li agraden els rams et pagarà en estrelles! Fes clic al botó per confirmar el ram!";
                ShowArrow(9);
                break;

            case Step.EndTutorial:
                tutorialText.text = "Molta sort en el dia d’avui :)";
                ShowArrow(-1);
                break;
        }
    }

    public void OnPlayerMoved()
    {
        if (currentStep == Step.MoveInShop && !isWaitingTimer)
        {
            StartCoroutine(WaitThreeSecondsPostMovement());
        }
    }

    private IEnumerator WaitThreeSecondsPostMovement()
    {
        isWaitingTimer = true;
        yield return new WaitForSeconds(3f); 
        if (currentStep == Step.MoveInShop) 
            NextStep();
        isWaitingTimer = false;
    }

    public void OnEnterWindowArea() 
    { 
        if (currentStep == Step.GoToWindow) 
            NextStep(); 
    }

    public void OnTalkedToClient() 
    { 
        if (currentStep == Step.PressFToTalk) 
            NextStep(); 
    }

    public void OnEnterTableArea() 
    { 
        if (currentStep == Step.GoToTable) 
            NextStep(); 
    }

    public void OnPressedFToBuild() 
    { 
        if (currentStep == Step.PressFToBuild) 
            NextStep(); 
    }

    public void OnDraggedFirstRose() 
    { 
        if (currentStep == Step.DragFirstRose) 
            NextStep(); 
    }

    public void OnCutRose() 
    { 
        if (currentStep == Step.CutRose) 
            NextStep(); 
    }

    public void OnRotateRose() 
    { 
        if (currentStep == Step.RotateRose) 
            NextStep(); 
    }

    public void OnChooseBow() 
    { 
        if (currentStep == Step.ChooseBow) 
            NextStep(); 
    }

    public void OnDeleteRose() 
    { 
        if (currentStep == Step.DeleteRose) 
            NextStep(); 
    }
    
    public void OnFourRoses() 
    { 
        if (currentStep == Step.MakeFourRoses) 
            NextStep(); 
    }

    public void OnConfirmBouquet() 
    { 
        if (currentStep == Step.ConfirmBouquet) 
            NextStep(); 
    }
}