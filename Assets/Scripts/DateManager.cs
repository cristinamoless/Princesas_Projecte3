using UnityEngine;
using TMPro;

public class DateManager : MonoBehaviour
{
    public TMP_Text dayText;
    public TMP_Text starsText;

    public GameFlowManager gfm;

    private static readonly string[] days =
    {
        "DILLUNS",
        "DIMARTS",
        "DIMECRES",
        "DIJOUS",
        "DIVENDRES",
        "DISSABTE",
        "DIUMENGE"
    };

    void Update()
    {
        dayText.text = GetDayName(gfm.currentDay);
        starsText.text = PlayerStars.Instance.totalStars.ToString();
    }

    string GetDayName(int day)
    {
        if (day <= 0 || day > days.Length)
            return "???";

        return days[day - 1];
    }
}