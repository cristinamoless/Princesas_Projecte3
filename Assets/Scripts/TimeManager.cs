using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TMP_Text timeText;
    public int currentHour = 10;

    [Header("Exterior Light")]
    public Light sunLight;
    public Color morningColor = new Color(1f, 0.95f, 0.8f);
    public Color afternoonColor = new Color(1f, 1f, 0.9f);
    public Color eveningColor = new Color(1f, 0.6f, 0.4f);

    public void SetTime(int hour)
    {
        currentHour = hour;
        timeText.text = hour.ToString("00") + ":00";

        UpdateLighting(hour);
    }

    void UpdateLighting(int hour)
    {
        if (hour < 12)
        {
            sunLight.color = morningColor;
            sunLight.intensity = 1.2f;
        }
        else if (hour < 17)
        {
            sunLight.color = afternoonColor;
            sunLight.intensity = 1.5f;
        }
        else
        {
            sunLight.color = eveningColor;
            sunLight.intensity = 0.8f;
        }
    }
}
