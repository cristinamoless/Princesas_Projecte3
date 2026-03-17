using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Input WASD / Fletxes
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Moviment en el pla XZ
        Vector3 move = transform.TransformDirection(new Vector3(h, 0f, v));

        // Moure el personatge
        controller.Move(move * speed * Time.deltaTime);

        // Gravetat per mantenir-lo enganxat a terra
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        float movementSpeed = move.magnitude * speed;
        animator.SetFloat("speed", movementSpeed);

    }
}
