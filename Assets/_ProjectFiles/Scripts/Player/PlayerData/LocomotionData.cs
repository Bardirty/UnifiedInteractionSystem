using UnityEngine;

[CreateAssetMenu(fileName = "LocomotionData", menuName = "Player/LocomotionData", order = 0)]
public class LocomotionData : ScriptableObject {
    public float moveSpeed = 2f;
    public float dodgeSpeed = 5f;

    public float sprintSpeedMultiplier = 1.5f;
    public float movementDeadzone = 0.01f;
    public float dodgeDuration = 0.3f;

    public float sprintStaminaCostPerSecond = 15f;
    public float minimalStaminaForRun = 20f;
    public float sprintHoldThreshold = 0.25f;
    public float dodgeStaminaCost = 25f;
}
