using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLookInput : MonoBehaviour
{
    public CinemachinePanTilt panTilt;

    public float sensitivity = 1.5f;

    public void OnLook(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        if (panTilt != null)
        {
            panTilt.PanAxis.Value += input.x * sensitivity;
            panTilt.TiltAxis.Value += input.y * sensitivity;
        }
    }
}