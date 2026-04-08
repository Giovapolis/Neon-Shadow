using Unity.Cinemachine;
using UnityEngine;

public class CinemachinePanTiltInput : MonoBehaviour
{
    public CameraInputBridge input;

    private CinemachinePanTilt panTilt;

    public float sensitivity = 2f;

    void Start()
    {
        panTilt = GetComponent<CinemachinePanTilt>();
    }

    void Update()
    {
        if (panTilt != null && input != null)
        {
            panTilt.PanAxis.Value += input.lookInput.x * sensitivity;
            panTilt.TiltAxis.Value += input.lookInput.y * sensitivity;
        }
    }
}