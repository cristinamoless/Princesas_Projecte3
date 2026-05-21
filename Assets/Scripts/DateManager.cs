using UnityEngine;
using TMPro;

public class DateManager : MonoBehaviour
{
    public TMP_Text dayText;
    public TMP_Text starsText;

    public GameFlowManager gfm;
    void Update()
    {
        dayText.text = GetDayName(gfm.currentDay);
        starsText.text = PlayerStars.Instance.totalStars.ToString();
    }


    string GetDayName(int day)
    {
        switch (day)
        {
            case 1: return "DILLUNS";
            case 2: return "DIMARTS";
            default: return "???";
        }
    }
}
