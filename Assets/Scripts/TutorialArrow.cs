using UnityEngine;

public class TutorialArrow : MonoBehaviour
{
    public int arrowID;

    [Header("Animació")]
    public float velocitat = 5f;
    public float amplitud = 20f; 
    public bool movimentVertical = true;

    private Vector3 posicioInicial;
    private bool initialized = false;

    void Start()
    {
        posicioInicial = transform.localPosition;
        initialized = true;
    }

    void Update()
    {
        if (!initialized) return;

        bool hauriaDeEstarVisible = FletxaHaDeEstarActiva();

        
        if (hauriaDeEstarVisible)
        {
            float desplaçament = Mathf.Sin(Time.time * velocitat) * amplitud;

            if (movimentVertical)
            {
                transform.localPosition = new Vector3(posicioInicial.x, posicioInicial.y + desplaçament, posicioInicial.z);
            }
            else
            {
                transform.localPosition = new Vector3(posicioInicial.x + desplaçament, posicioInicial.y, posicioInicial.z);
            }
        }
        else
        {
            transform.localPosition = new Vector3(-9999f, -9999f, -9999f);
        }
    }

    private bool FletxaHaDeEstarActiva()
    {
        if (TutorialManager.Instance == null) return false;

        TutorialManager.Step pasActual = TutorialManager.Instance.currentStep;

        switch (pasActual)
        {
            case TutorialManager.Step.GoToTable: return arrowID == 2;
            case TutorialManager.Step.DragFirstRose: return arrowID == 4;
            case TutorialManager.Step.CutRose: return arrowID == 5;
            case TutorialManager.Step.RotateRose: return arrowID == 6;
            case TutorialManager.Step.ChooseBow: return arrowID == 7;
            case TutorialManager.Step.DeleteRose: return arrowID == 8;
            case TutorialManager.Step.ConfirmBouquet: return arrowID == 9;
            default: return false;
        }
    }
}