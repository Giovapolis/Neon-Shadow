using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;
    public float verticalLimit = 60f;

    private PlayerInputActions inputActions;
    private Vector2 lookInput;

    private float xRotation = 0f;

    void Awake()
    {
        inputActions = new PlayerInputActions();

        inputActions.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    void OnEnable() => inputActions.Enable();
    void OnDisable() => inputActions.Disable();

    void LateUpdate()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLimit, verticalLimit);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}