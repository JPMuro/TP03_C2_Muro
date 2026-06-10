using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float crouchSpeed = 2f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    CharacterController controller;

    Vector3 velocity;

    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundMask
        );

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move =
            transform.right * x +
            transform.forward * z;

        float speed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed = runSpeed;

        if (Input.GetKey(KeyCode.LeftControl))
            speed = crouchSpeed;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -1.5f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}