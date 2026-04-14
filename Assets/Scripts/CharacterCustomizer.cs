using UnityEngine;

public class CharacterCustomizer : MonoBehaviour
{
    public Renderer shirtRenderer;        // El mesh renderer de la samarreta
    public Texture[] shirtTextures;       // Les textures disponibles

    private int index = 0;

    public void NextShirt()
    {
        index++;
        if (index >= shirtTextures.Length)
            index = 0;

        shirtRenderer.material.mainTexture = shirtTextures[index];
    }
}
