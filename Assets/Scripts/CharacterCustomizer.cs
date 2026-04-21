using UnityEngine;

public class CharacterCustomizer : MonoBehaviour
{
    public Renderer shirtRenderer;     
    public Texture[] shirtTextures;      

    private int index = 0;

    public void NextShirt()
    {
        index++;
        if (index >= shirtTextures.Length)
            index = 0;

        shirtRenderer.material.mainTexture = shirtTextures[index];
    }
    public void PreviousShirt()
    {
        index--;
        if (index < 0)
            index = shirtTextures.Length - 1;

        shirtRenderer.material.mainTexture = shirtTextures[index];
    }

}
