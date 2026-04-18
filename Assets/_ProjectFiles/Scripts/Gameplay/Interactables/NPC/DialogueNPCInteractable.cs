using UnityEngine;

public class DialogueNPC : BaseNPC {
    [SerializeField] private DialogueNodeSO _startNode;

    public override string GetInteractionText() => "Talk";

    public override void InteractStart(PlayerContext context) {
        context.DialogueManager.StartDialogue(_startNode);
        context.StateMachine.ChangeState(new PlayerDialogueState(context));
    }
}