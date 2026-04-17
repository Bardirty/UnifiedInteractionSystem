using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerLocomotion : MonoBehaviour {
    private LocomotionData _locomotionData;
    private Rigidbody _rigidbody;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    public void Initialize(LocomotionData locomotionData) {
        _locomotionData = locomotionData;
    }

    public void Move(Vector3 worldDirection, float speedMultiplier = 1f) {
        if(_locomotionData == null) {
            Debug.LogError("LocomotionData is not set!");
            return;
        }

        if (worldDirection.sqrMagnitude < 0.01f) {
            Stop();
            return;
        }
        _rigidbody.linearVelocity =
            worldDirection.normalized *
            _locomotionData.moveSpeed *
            speedMultiplier;
    }


    public void Stop() {
        _rigidbody.linearVelocity = Vector3.zero;
    }

    public void SetVelocity(Vector3 velocity) {
        _rigidbody.linearVelocity = velocity;
    }
}
