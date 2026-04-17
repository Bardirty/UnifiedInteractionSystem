public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerContext context)
        : base(context) { }

    public override void Enter() {
        context.Locomotion.Stop();
    }

    public override void Update() {
        if (context.Input.MoveInput.sqrMagnitude > context.LocomotionData.movementDeadzone || context.Input.LookInput.sqrMagnitude > 0.01f) {
            fsm.ChangeState(new PlayerWalkState(context));
            return;
        }
    }
}
