using UnityEngine;

public class ValveInteractable : MonoBehaviour, IInteractable {
    [Header("Settings")]
    [SerializeField] private float _holdSpeed = 1f;  
    [SerializeField] private float _returnSpeed = 1f;
    [SerializeField] private float _maxAngle = 180f;

    [Header("References")]
    [SerializeField] private Transform _valveVisual;
    [SerializeField] private Door _door;

    private float _progress;
    private bool _isHolding;

    public string GetInteractionText() => "Hold to turn valve";

    public void InteractStart(PlayerContext playerContext) {
        _isHolding = true;
    }

    public void InteractHold(PlayerContext playerContext) {
        _isHolding = true;
    }

    public void InteractEnd(PlayerContext playerContext) {
        _isHolding = false;
    }

    private void Update() {
        if (_isHolding) {
            _progress += Time.deltaTime * _holdSpeed;
        }
        else {
            _progress -= Time.deltaTime * _returnSpeed;
        }

        _progress = Mathf.Clamp01(_progress);

        UpdateVisuals();
    }

    private void UpdateVisuals() {
        float angle = _progress * _maxAngle;
        _valveVisual.localRotation = Quaternion.Euler(0f, 0f, -angle);
        if (_door != null) {
            _door.SetProgress(_progress);
        }
    }
}