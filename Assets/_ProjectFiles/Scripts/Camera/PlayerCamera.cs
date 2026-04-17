using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    private float _pitch = 0f;
    private CameraData _cameraData;

    public void Initialize(CameraData cameraData) {
        _cameraData = cameraData;
    }
    public void RotateCamera(float lookY) {
        if (_cameraData == null) {
            Debug.LogError("CameraData is not set!");
            return;
        }
        _pitch -= lookY * _cameraData.cameraPitchSensitivity * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, _cameraData.minPitch, _cameraData.maxPitch);

        transform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}
