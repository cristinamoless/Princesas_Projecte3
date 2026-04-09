using UnityEngine;

public class PlayerStars : MonoBehaviour
{
    public int totalStars = 0;

    public void addStars(int amount)
    {
        totalStars += amount;
    }
}