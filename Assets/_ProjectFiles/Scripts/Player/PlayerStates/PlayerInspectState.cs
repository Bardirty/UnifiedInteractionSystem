public class PlayerInspectState : PlayerBaseState {
    public PlayerInspectState(PlayerContext context) : base(context) {}

    public override void Enter() {
        context.Locomotion.Stop();
        context.ItemHolder.InspectingItem.ChangePhysics(false);
        context.ItemHolder.InspectingItem.SetSlot(context.InspectPoint);
    }
    public override void Update() {
        if(context.Input.Interact.IsDown) {
            if (!context.ItemHolder.HasItem) {
                context.ItemHolder.ConfirmInspect();
            }
            fsm.ChangeState(new PlayerIdleState(context));
        }
    }
}
