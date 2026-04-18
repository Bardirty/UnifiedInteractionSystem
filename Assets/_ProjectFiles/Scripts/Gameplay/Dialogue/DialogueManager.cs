using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
    private DialogueNodeSO _currentNode;

    public bool IsActive => _currentNode != null;

    // Standard dialogue events
    public event Action OnDialogueStarted;
    public event Action<string> OnTextChanged;
    public event Action<DialogueChoiceSO[]> OnChoicesChanged;
    public event Action OnDialogueEnded;

    // Quest events
    public event Action OnQuestRequested;
    public event Action OnQuestCompleteRequested;

    public void StartDialogue(DialogueNodeSO startNode) {
        OnDialogueStarted?.Invoke();
        SetNode(startNode);
    }

    private void SetNode(DialogueNodeSO node) {
        if (node == null)
            return;
        _currentNode = node;

        OnTextChanged?.Invoke(node.Text);

        HandleNodeLogic(node);

        if (node.HasChoices)
            OnChoicesChanged?.Invoke(node.Choices);
        else
            OnChoicesChanged?.Invoke(null);
    }
    private void HandleNodeLogic(DialogueNodeSO node) {
        switch (node.NodeType) {
            case DialogueNodeType.Default:
                break;
            case DialogueNodeType.QuestStart:
                OnQuestRequested?.Invoke();
                break;
            case DialogueNodeType.QuestComplete:
                OnQuestCompleteRequested?.Invoke();
                break;
        }
    }

    public void SelectChoice(int index) {
        if (_currentNode == null || !_currentNode.HasChoices)
            return;

        var next = _currentNode.Choices[index].NextNode;

        if (next == null) {
            EndDialogue();
            return;
        }

        SetNode(next);
    }

    public void Continue() {
        if (_currentNode == null)
            return;

        if (_currentNode.HasChoices)
            return;

        if (_currentNode.NextNode != null) {
            SetNode(_currentNode.NextNode);
        }
        else {
            EndDialogue();
        }
    }

    private void EndDialogue() {
        _currentNode = null;
        OnChoicesChanged?.Invoke(null);
        OnDialogueEnded?.Invoke();
    }
}