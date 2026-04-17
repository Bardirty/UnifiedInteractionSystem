public class PlayerInspectState : PlayerBaseState {
    public PlayerInspectState(PlayerContext context) : base(context) {}

    private Item _inspectingItem;
    public override void Enter() {
        context.Locomotion.Stop();
        context.LockInteraction();
        _inspectingItem = context.Inspection.InspectingItem;
        if (_inspectingItem != null) {
            _inspectingItem.ChangePhysics(false);
            _inspectingItem.AttachToTransform(context.InspectPoint);
        }
    }
    public override void Update() {
        if(context.Input.Interact.IsDown) {
            if(_inspectingItem != null && context.ItemHolder.TryHoldItem(_inspectingItem)) {
                context.Inspection.ConfirmInspect();
            }
            context.Interaction.IgnoreInputThisFrame();
            fsm.ChangeState(new PlayerIdleState(context));
        }
    }
    public override void Exit() {
        context.UnlockInteraction();
    }
}
