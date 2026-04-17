using System;
using UnityEngine;

public class GameBootstrap : MonoBehaviour {
    [SerializeField] private PlayerContext _playerContext;
    [SerializeField] private GUIManager _guiManager;
    [SerializeField] private DialogueController _dialogueController;
    [SerializeField] private DialogueUI _dialogueUI;


    private void Start() {
        BindInteraction();
        BindDialogue();
        _dialogueUI.gameObject.SetActive(false);
    }

    private void BindInteraction() {
        if (_playerContext.Interaction != null)
            _playerContext.Interaction.OnInteractRayTouched += _guiManager.SetInteractionText;
    }
    private void UnbindInteraction() {
        if (_playerContext.Interaction != null)
            _playerContext.Interaction.OnInteractRayTouched -= _guiManager.SetInteractionText;
    }

    private void BindDialogue() {
        if (_dialogueController == null || _dialogueUI == null)
            return;

        _dialogueUI.Bind(_dialogueController);

        if (_guiManager != null) {
            _dialogueController.OnDialogueStarted += DialogueController_OnDialogueStart;
            _dialogueController.OnDialogueEnded += DialogueController_OnDialogueEnd;
        }
    }
    private void UnbindDialogue() {
        if (_dialogueController == null || _dialogueUI == null)
            return;
        _dialogueUI.Unbind();

        if (_guiManager != null) {
            _dialogueController.OnDialogueStarted -= DialogueController_OnDialogueStart;
            _dialogueController.OnDialogueEnded -= DialogueController_OnDialogueEnd;
        }
    }
    private void DialogueController_OnDialogueStart() {
        _dialogueUI.gameObject.SetActive(true);
        _guiManager.SetCursor(true);
    }
    private void DialogueController_OnDialogueEnd() {
        _dialogueUI.gameObject.SetActive(false);
        _guiManager.SetCursor(false);
    }

    private void OnDestroy() {
        UnbindInteraction();
        UnbindDialogue();
    }
}