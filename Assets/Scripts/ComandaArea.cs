using UnityEngine;
using TMPro;

public class ComandaArea : MonoBehaviour
{
    public GameFlowManager gameFlow;

    public bool hasTalked = false;


    private void OnTriggerStay(Collider other)
    {
        if (hasTalked) return;
        if (Input.GetKeyDown(KeyCode.F))
        {
            hasTalked = true;
            gameFlow.TalkClients();
            FindFirstObjectByType<RockNPC>().StopWaving();
            AudioManager.Instance.Play("Dialeg");
        }

        TutorialManager.Instance.OnEnterWindowArea();
    }
}
