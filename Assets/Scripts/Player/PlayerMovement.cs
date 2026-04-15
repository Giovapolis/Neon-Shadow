using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;

    private CharacterController controller;
    private Vector3 velocity;

    private PlayerInputActions inputActions;
    private Vector2 moveInput;

    void Awake()
    {
        inputActions = new PlayerInputActions();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        RotateWithCamera();
        Move();
    }

    void Move()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * moveInput.y + right * moveInput.x).normalized;

        if (move.magnitude >= 0.1f)
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        // gravedad
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void RotateWithCamera()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;

        if (cameraForward.sqrMagnitude < 0.01f) return;

        cameraForward.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}