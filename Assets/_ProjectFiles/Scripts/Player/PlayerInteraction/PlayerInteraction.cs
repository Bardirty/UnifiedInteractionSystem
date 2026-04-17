using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    private PlayerContext _playerContext;

    private Transform _cameraTransform;
    private PlayerInputHandler _inputHandler;
    private InteractionData _interactionData;

    private IInteractable _currentInteractable;
    private IInteractable _previousInteractable;

    public IInteractable CurrentInteractable => _currentInteractable;

    public Action<string> OnInteractRayTouched;

    private bool _interactionBlocked;
    private string _currentInteractionText;

    public void Initialize(PlayerContext playerContext) {
        _playerContext = playerContext;

        _cameraTransform = playerContext.Camera.transform;
        _inputHandler = playerContext.Input;
        _interactionData = playerContext.InteractionData;
    }

    private void Update() {
        if (_interactionBlocked) {
            _interactionBlocked = false;
            return;
        }

        if (!_playerContext.CanInteract) {
            ClearInteractables();
            return;
        }
        CheckForInteractables();

        if (_currentInteractable == null && _previousInteractable != null) {
            _previousInteractable.InteractEnd(_playerContext);
            _previousInteractable = null;
        }
        if (_currentInteractable != null) {
            if (_inputHandler.Interact.IsDown)
                Interact();
            else if (_inputHandler.Interact.IsHeld)
                InteractHold();
            else if (_inputHandler.Interact.IsUp)
                InteractEnd();
        }
    }

    private void CheckForInteractables() {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _interactionData.interactionDistance)) {
            if (hit.collider.TryGetComponent(out IInteractable interactable)) {
                string newInteractionText = interactable.GetInteractionText();
                if (interactable != _currentInteractable) {
                    _previousInteractable = _currentInteractable;
                    _currentInteractable = interactable;

                    _currentInteractionText = newInteractionText;
                    OnInteractRayTouched?.Invoke(_currentInteractionText);
                }
                else if (newInteractionText != _currentInteractionText) {
                    _currentInteractionText = newInteractionText;
                    OnInteractRayTouched?.Invoke(_currentInteractionText);
                }
            }
        }
        else {
            if (_currentInteractable != null) {
                _previousInteractable = _currentInteractable;
                _currentInteractable = null;

                _currentInteractionText = string.Empty;
                OnInteractRayTouched?.Invoke(_currentInteractionText);
            }
        }
    }
    public void ClearInteractables() {
        _previousInteractable = null;
        _currentInteractable = null;
        OnInteractRayTouched?.Invoke(string.Empty);
    }
    public void IgnoreInputThisFrame() => _interactionBlocked = true;
    public void Interact() => _currentInteractable.InteractStart(_playerContext);
    public void InteractHold() => _currentInteractable.InteractHold(_playerContext);
    public void InteractEnd() => _currentInteractable.InteractEnd(_playerContext);
}
