using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Transform _cameraTransform;
    private CameraData _cameraData;
    public void Initialize(
        Transform cameraTransform,
        CameraData cameraData
        ) {
        _cameraTransform = cameraTransform;
        _cameraData = cameraData;
    }

    public void RotateYaw(float mouseX) {
        if (_cameraData == null) {
            Debug.LogError("CameraData is not set!");
            return;
        }
        transform.Rotate(Vector3.up * mouseX * _cameraData.cameraYawSensitivity * Time.deltaTime);
    }
    public Vector3 GetMovementDirection(Vector2 input) {
        if (input.sqrMagnitude < 0.01f)
            return Vector3.zero;

        Vector3 camForward = _cameraTransform.forward;
        Vector3 camRight = _cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        return (camForward * input.y + camRight * input.x).normalized;
    }
}
