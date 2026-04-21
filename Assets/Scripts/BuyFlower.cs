using System.Collections.Generic;
using UnityEngine;

public class BuyFlower : MonoBehaviour
{
    public List<FlowerType> allFlowers;
    public GameFlowManager gfm;

    [System.Serializable]
    public class FlowerButton
    {
        public FlowerType flower;
        public GameObject button;
    }

    public List<FlowerButton> flowerButtons;

    public void showFlowers()
    {
        List<FlowerType> available = new List<FlowerType>();

        foreach (var flower in allFlowers)
        {
            if (flower.availableDay <= gfm.currentDay && !flower.unlocked)
                available.Add(flower);
        }

        foreach (var fb in flowerButtons)
        {
            fb.button.SetActive(available.Contains(fb.flower));
        }
    }

    public void buyFlower(FlowerType flower)
    {
        if (PlayerStars.Instance.totalStars >= flower.seedPrice)
        {
            PlayerStars.Instance.totalStars -= flower.seedPrice;
            flower.unlocked = true;
            Debug.Log("Has comprat: " + flower.flowerName);
        }
        else
        {
            Debug.Log("No tens prou estrelles!");
        }
    }
}
