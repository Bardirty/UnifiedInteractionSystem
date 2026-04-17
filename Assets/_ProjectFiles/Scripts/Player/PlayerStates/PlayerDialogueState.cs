public class PlayerDialogueState : PlayerBaseState {
    private DialogueController _dialogue;

    public PlayerDialogueState(PlayerContext context) : base(context) { }

    public override void Enter() {
        context.Locomotion.Stop();

        _dialogue = context.DialogueController;

        context.LockInteraction();

        _dialogue.OnDialogueEnded += OnDialogueEnded;
    }

    public override void Update() {
        if (context.Input.Interact.IsDown) {
            _dialogue.Continue();
        }
    }

    private void OnDialogueEnded() {
        _dialogue.OnDialogueEnded -= OnDialogueEnded;
        fsm.ChangeState(new PlayerIdleState(context));
    }
    public override void Exit() {
        context.Interaction.IgnoreInputThisFrame();
        context.UnlockInteraction();
    }
}