using UnityEngine;

[CreateAssetMenu(fileName = "DefaultLocomotionData", menuName = "Player/LocomotionData", order = 0)]
public class LocomotionData : ScriptableObject {
    public float moveSpeed = 2f;
    public float movementDeadzone = 0.01f;
}
