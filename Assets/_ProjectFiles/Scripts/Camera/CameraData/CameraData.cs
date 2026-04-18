using UnityEngine;

[CreateAssetMenu(fileName = "DefaultCameraData", menuName = "Camera/Camera Data")]
public class CameraData : ScriptableObject {
    public float cameraYawSensitivity = 1f;
    public float cameraPitchSensitivity = 1f;
    public float minPitch = -90f;
    public float maxPitch = 90f;

}
