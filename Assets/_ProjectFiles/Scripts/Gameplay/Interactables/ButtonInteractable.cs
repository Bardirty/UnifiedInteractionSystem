using System;
using UnityEngine;

public class ButtonInteractable : MonoBehaviour, IInteractable {
    private const string PRESS_TRIGGER = "Press";

    [SerializeField] private Animator _animator;
    public event Action OnButtonPressed;
    public string GetInteractionText() => "Press E to press button";

    private void Awake() {
        if (_animator == null) {
            _animator = GetComponent<Animator>();
        }
    }

    public void InteractEnd(PlayerContext playerContext) {}

    public void InteractHold(PlayerContext playerContext) {}

    public void InteractStart(PlayerContext playerContext) {
        _animator?.SetTrigger(PRESS_TRIGGER);
        OnButtonPressed?.Invoke();
    }
}
