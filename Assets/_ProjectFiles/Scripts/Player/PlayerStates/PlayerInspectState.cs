using UnityEngine;

public class PlayerInspectState : PlayerBaseState {
    private float rotationSpeed => context.InspectionData.RotationSpeed;
    public PlayerInspectState(PlayerContext context) : base(context) {}

    private Item _inspectingItem;
    public override void Enter() {
        context.Locomotion.Stop();
        context.LockInteraction();
        _inspectingItem = context.Inspection.InspectingItem;
        if (_inspectingItem != null) {
            _inspectingItem.ChangePhysics(false);
            _inspectingItem.SmoothAttachToTransform(context.InspectPoint, context.InspectionData.TransitionDuration);
            _inspectingItem.OnInspectStart();
        }
    }
    public override void Update() {
        HandleRotation();
        HandleInteract();
    }
    private void HandleRotation() {

        if (context.Input.MouseLeft.IsHeld && _inspectingItem != null) {
            Vector2 delta = context.Input.LookInput;
            _inspectingItem.transform.Rotate(
                Vector3.up, -delta.x * rotationSpeed, Space.World);
            _inspectingItem.transform.Rotate(
                Vector3.right, delta.y * rotationSpeed, Space.World);
        }
    }

    private void HandleInteract() {
        if (context.Input.Interact.IsDown) {
            if (_inspectingItem != null && context.ItemHolder.TryHoldItem(_inspectingItem)) {
                context.Inspection.ConfirmInspect();
            }
            context.Interaction.IgnoreInputThisFrame();
            fsm.ChangeState(new PlayerIdleState(context));
        }
    }
    public override void Exit() {
        if (_inspectingItem != null) {
            _inspectingItem.OnInspectEnd();
        }
        context.UnlockInteraction();
    }
}
