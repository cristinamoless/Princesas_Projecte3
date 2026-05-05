using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image characterImage;

    public GameObject dialoguePanel;
    public GameObject agafarComandaButton;
    public bool isDialogueInici = true;
    private Dialogue dialogue;
    private int index = 0;

    public Button choiceButtonA;
    public Button choiceButtonB;

    public void StartDialogue(Dialogue d)
    {
        dialogue = d;
        index = 0;

        dialoguePanel.SetActive(true);
        agafarComandaButton.SetActive(false);

        nameText.text = dialogue.characterName;
        characterImage.sprite = dialogue.characterPixel;

        ShowSentence();
    }

    public void NextSentence()
    {
        index++;

        if (index >= dialogue.sentences.Length)
        {
            EndDialogue();
            return;
        }

        ShowSentence();
    }

    void ShowSentence()
    {
        string s = dialogue.sentences[index];

        s = s.Replace("{playerName}", PlayerPrefs.GetString("playerName"));

        dialogueText.text = s;
    }


    void EndDialogue()
    {
        if (dialogue.choices != null && dialogue.choices.Length > 0)
        {
            ShowSimpleChoices();
            return;
        }
        dialoguePanel.SetActive(false);
        agafarComandaButton.SetActive(true);
    }

    void Update()
    {
        if (!dialoguePanel.activeSelf) return;

        if (Input.GetMouseButtonDown(0) || 
            Input.GetKeyDown(KeyCode.Space) || 
            Input.GetKeyDown(KeyCode.Return))
        {
            NextSentence();
        }
    }
    void ShowSimpleChoices()
    {
        dialogueText.text = ""; 

        choiceButtonA.gameObject.SetActive(true);
        choiceButtonB.gameObject.SetActive(true);

        choiceButtonA.GetComponentInChildren<TMP_Text>().text = dialogue.choices[0].choiceText;

        if (dialogue.choices.Length > 1)
        {
            choiceButtonB.GetComponentInChildren<TMP_Text>().text = dialogue.choices[1].choiceText;
            choiceButtonB.gameObject.SetActive(true);
        }
        else
        {
            choiceButtonB.gameObject.SetActive(false);
        }

        choiceButtonA.onClick.RemoveAllListeners();
        choiceButtonA.onClick.AddListener(() =>
        {
            SelectChoice(0);
        });

        if (dialogue.choices.Length > 1)
        {
            choiceButtonB.onClick.RemoveAllListeners();
            choiceButtonB.onClick.AddListener(() =>
            {
                SelectChoice(1);
            });
        }
    }
    void SelectChoice(int index)
    {
        choiceButtonA.gameObject.SetActive(false);
        choiceButtonB.gameObject.SetActive(false);

        StartDialogue(dialogue.choices[index].nextDialogue);
    }
}
