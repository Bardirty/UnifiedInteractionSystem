using System;
using UnityEngine;

public class DialogueController : MonoBehaviour {
    private DialogueNodeSO _currentNode;

    public bool IsActive => _currentNode != null;

    public event Action OnDialogueStarted;
    public event Action<string> OnTextChanged;
    public event Action<DialogueChoiceSO[]> OnChoicesChanged;
    public event Action OnDialogueEnded;

    public void StartDialogue(DialogueNodeSO startNode) {
        OnDialogueStarted?.Invoke();
        SetNode(startNode);
    }

    private void SetNode(DialogueNodeSO node) {
        _currentNode = node;

        OnTextChanged?.Invoke(node.Text);
        
        if (node.HasChoices)
            OnChoicesChanged?.Invoke(node.Choices);
        else
            OnChoicesChanged?.Invoke(null);
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