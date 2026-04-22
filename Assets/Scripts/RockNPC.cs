using UnityEngine;

public class RockNPC : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim.SetBool("isWaving", true);   // Comença saludant
    }

    public void StopWaving()
    {
        anim.SetBool("isWaving", false);
    }
}
