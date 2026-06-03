using UnityEngine;

public class TutorialArrow : MonoBehaviour
{
    public int arrowID;

    void OnEnable()
    {
        if (TutorialManager.Instance != null)
            TutorialManager.Instance.RegisterArrow(this);
    }

    void OnDisable()
    {
        if (TutorialManager.Instance != null)
            TutorialManager.Instance.UnregisterArrow(this);
    }
}
