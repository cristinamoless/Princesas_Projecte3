using UnityEngine;
using TMPro;

public class UIOrderDisplay : MonoBehaviour
{
    public TMP_Text clientText;
    public TMP_Text comandaText;

    public void ShowOrder(Comanda c)
    {
        clientText.text = c.clientName;

        comandaText.text = "";
        foreach (var f in c.requiredFlowers)
            comandaText.text += "- " + f.name + "\n";
    }
}
