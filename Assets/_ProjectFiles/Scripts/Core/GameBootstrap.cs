using System;
using UnityEngine;

public class GameBootstrap : MonoBehaviour {
    [SerializeField] private PlayerContext _playerContext;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private DialogueManager _dialogueController;
    [SerializeField] private DialogueUI _dialogueUI;
    [SerializeField] private QuestManager _questManager;
    [SerializeField] private QuestUI _questUI;
    [SerializeField] private InspectionUI _inspectionUI;

    private void Start() {
        BindInteraction();
        BindDialogue();
        BindQuest();
        BindInspection();
        _dialogueUI.SetDialoguePanelActive(false);
    }

    // Interaction region
    #region Interaction Binding
    private void BindInteraction() {
        if (_playerContext.Interaction != null)
            _playerContext.Interaction.OnInteractRayTouched += _gameUI.SetInteractionText;
    }
    private void UnbindInteraction() {
        if (_playerContext.Interaction != null)
            _playerContext.Interaction.OnInteractRayTouched -= _gameUI.SetInteractionText;
    }
    #endregion

    // Dialogue region
    #region Dialogue Binding
    private void BindDialogue() {
        if (_dialogueController == null || _dialogueUI == null)
            return;

        _dialogueUI.Bind(_dialogueController);

        if (_gameUI != null) {
            _dialogueController.OnDialogueStarted += DialogueController_OnDialogueStart;
            _dialogueController.OnDialogueEnded += DialogueController_OnDialogueEnd;
        }
    }
    private void UnbindDialogue() {
        if (_dialogueController == null || _dialogueUI == null)
            return;
        _dialogueUI.Unbind();

        if (_gameUI != null) {
            _dialogueController.OnDialogueStarted -= DialogueController_OnDialogueStart;
            _dialogueController.OnDialogueEnded -= DialogueController_OnDialogueEnd;
        }
    }
    private void DialogueController_OnDialogueStart() {
        _dialogueUI.SetDialoguePanelActive(true);
        _gameUI.SetCursor(true);
    }
    private void DialogueController_OnDialogueEnd() {
        _dialogueUI.SetDialoguePanelActive(false);
        _gameUI.SetCursor(false);
    }
    #endregion

    // Quest region
    #region Quest Binding
    private void BindQuest() {
        if (_dialogueController == null || _questManager == null || _questUI == null)
            return;

        _dialogueController.OnQuestRequested += _questManager.StartQuest;
        _dialogueController.OnQuestCompleteRequested += DialogueController_OnQuestCompleteRequested;

        _questManager.OnQuestStarted += _questUI.Show;
        _questManager.OnQuestCompleted += _questUI.Complete;
    }
    private void UnbindQuest() {
        if (_dialogueController == null || _questManager == null || _questUI == null)
            return;

        _dialogueController.OnQuestRequested -= _questManager.StartQuest;
        _dialogueController.OnQuestCompleteRequested -= DialogueController_OnQuestCompleteRequested;

        _questManager.OnQuestStarted -= _questUI.Show;
        _questManager.OnQuestCompleted -= _questUI.Complete;
    }
    private void DialogueController_OnQuestCompleteRequested() {
        _questManager.TryCompleteWithPlayer(_playerContext);
    }
    #endregion

    // Inspection region
    #region Inspection Binding
    private void BindInspection() {
        if (_playerContext == null || _inspectionUI == null)
            return;

        _playerContext.Inspection.OnInspectStarted += OnInspectStarted;
        _playerContext.Inspection.OnInspectEnded += OnInspectEnded;
    }

    private void OnInspectStarted(Item item) {
        _inspectionUI.Show(item.Description);
    }
    private void OnInspectEnded() {
        _inspectionUI.Hide();
    }
    private void UnbindInspection() {
        if (_playerContext == null || _inspectionUI == null)
            return;
        _playerContext.Inspection.OnInspectStarted -= OnInspectStarted;
        _playerContext.Inspection.OnInspectEnded -= OnInspectEnded;
    }

    
    #endregion

    private void OnDestroy() {
        UnbindInspection();
        UnbindQuest();
        UnbindDialogue();
        UnbindInteraction();
    }
}