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

    private Dialogue dialogue;
    private int index = 0;

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
        dialogueText.text = dialogue.sentences[index];
    }

    void EndDialogue()
    {
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

}
