using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    private PlayerContext _playerContext;

    private Transform _cameraTransform;
    private PlayerInputHandler _inputHandler;
    private InteractionData _interactionData;


    private IInteractable _currentInteractable;
    private IInteractable _previousInteractable;

    public Action<string> OnInteractRayTouched;

    public void Initialize(PlayerContext playerContext) {
        _playerContext = playerContext;

        _cameraTransform = playerContext.Camera.transform;
        _inputHandler = playerContext.Input;
        _interactionData = playerContext.InteractionData;
    }

    private void Update() {
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
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null && interactable != _currentInteractable) {
                _previousInteractable = _currentInteractable;
                _currentInteractable = interactable;
                OnInteractRayTouched?.Invoke(interactable.GetInteractionText());
            }
        }
        else {
            if (_currentInteractable != null) {
                _previousInteractable = _currentInteractable;
                _currentInteractable = null;

                OnInteractRayTouched?.Invoke(string.Empty);
            }
        }
    }

    public void Interact() => _currentInteractable.InteractStart(_playerContext);
    public void InteractHold() => _currentInteractable.InteractHold(_playerContext);
    public void InteractEnd() => _currentInteractable.InteractEnd(_playerContext);
}
