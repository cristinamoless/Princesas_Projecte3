using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Dialogue")]
public class Dialogue : ScriptableObject
{
    public string characterName;
    public Sprite characterPixel;

    [TextArea(3, 10)]
    public string[] sentences;
}
