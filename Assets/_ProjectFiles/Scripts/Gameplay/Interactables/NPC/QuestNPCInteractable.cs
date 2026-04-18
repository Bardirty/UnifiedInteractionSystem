using System;
using UnityEngine;

enum QuestNPCState {
    Default,
    QuestGiven,
    QuestCompleted
}
public class QuestNPC : BaseNPC, IQuestInjectable {
    [Header("Dialogue")]
    [SerializeField] private DialogueNodeSO _defaultNode;
    [SerializeField] private DialogueNodeSO _questGivenNode;
    [SerializeField] private DialogueNodeSO _questCompletedNode;

    private QuestNPCState _state = QuestNPCState.Default;

    private QuestManager _questManager;
    private Action<ItemType> _onQuestStarted;
    private Action _onQuestCompleted;

    public override string GetInteractionText() => "Talk";

    public override void InteractStart(PlayerContext context) {
        var node = GetDialogueNode(context);

        context.DialogueManager.StartDialogue(node);
        context.StateMachine.ChangeState(new PlayerDialogueState(context));
    }

    private DialogueNodeSO GetDialogueNode(PlayerContext context) {
        if (_state == QuestNPCState.QuestGiven) {
            var item = context.ItemHolder.CurrentItem;

            if (item != null && item.ItemType == _questManager.RequiredItem)
                return _questCompletedNode;

            return _questGivenNode;
        }

        return _state switch {
            QuestNPCState.Default => _defaultNode,
            QuestNPCState.QuestCompleted => _questCompletedNode,
            _ => _defaultNode
        };
    }

    public void Inject(QuestManager questManager) {
        _questManager = questManager;

        _onQuestStarted = (_) => _state = QuestNPCState.QuestGiven;
        _onQuestCompleted = () => _state = QuestNPCState.QuestCompleted;

        _questManager.OnQuestStarted += _onQuestStarted;
        _questManager.OnQuestCompleted += _onQuestCompleted;
    }

    private void OnDestroy() {
        if (_questManager == null) return;

        _questManager.OnQuestStarted -= _onQuestStarted;
        _questManager.OnQuestCompleted -= _onQuestCompleted;
    }
}