using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputBridge : MonoBehaviour
{
    public Vector2 lookInput;

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
        Debug.Log("LOOK: " + lookInput);
    }
}