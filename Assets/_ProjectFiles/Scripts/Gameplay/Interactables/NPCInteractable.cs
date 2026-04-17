using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable {
    [SerializeField] private DialogueNodeSO _startNode;

    public string GetInteractionText() => "Talk";

    public void InteractStart(PlayerContext context) {
        context.DialogueController.StartDialogue(_startNode);
        context.StateMachine.ChangeState(new PlayerDialogueState(context));
    }

    public void InteractHold(PlayerContext context) { }
    public void InteractEnd(PlayerContext context) { }
}