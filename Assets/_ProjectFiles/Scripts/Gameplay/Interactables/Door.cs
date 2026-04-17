using UnityEngine;

public class Door : MonoBehaviour {
    [SerializeField] private Transform _doorTransform;
    [SerializeField] private float _openHeight = 3f;

    private Vector3 _closedPosition;

    private void Awake() {
        _closedPosition = _doorTransform.localPosition;
    }

    public void SetProgress(float progress) {
        Vector3 target = _closedPosition + Vector3.up * (_openHeight * progress);
        _doorTransform.localPosition = target;
    }
}