using UnityEngine;

public class PlayerWalkState : PlayerBaseState {
    public PlayerWalkState(PlayerContext context)
        : base(context) { }
    public override void Enter() {
    }
    public override void FixedUpdate() {
        Vector3 direction =
            context.Rotation.GetMovementDirection(context.Input.MoveInput);
        context.Locomotion.Move(direction);
    }

    public override void Update() {
        if (context.Input.MoveInput.sqrMagnitude < context.LocomotionData.movementDeadzone) {
            fsm.ChangeState(new PlayerIdleState(context));
        }
    }
    public override void LateUpdate() {
        context.Rotation.RotateYaw(context.Input.LookInput.x);
        context.Camera.RotateCamera(context.Input.LookInput.y);
    }

}
