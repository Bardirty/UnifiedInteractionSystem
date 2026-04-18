using UnityEngine;
using System.Collections;
public enum ItemType {
    Key,
    Note,
    Ball,
    Cube
}
public class Item : MonoBehaviour, IInteractable {
    [SerializeField] private ItemType _itemType = ItemType.Cube;
    [SerializeField] private string _description;
    [SerializeField] private string _interactionText = "Pick Item";
    public ItemType ItemType => _itemType;
    public string Description => _description;

    public ItemSlot CurrentItemSlot { get; set; }
    public bool IsInteractable => CurrentItemSlot == null;

    private Rigidbody _rigidbody;
    private Collider _collider;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }
    // Logic for attaching and detaching the item 
    public void AttachToSlot(ItemSlot slot) {
        if (slot == null)
            return;
        CurrentItemSlot = slot;
        AttachToTransform(slot.transform);
    }

    public void DetachFromSlot() {
        CurrentItemSlot = null;
        Detach();
    }
    // World transform attachment
    public void AttachToTransform(Transform target) {
        if (target == null)
            return;

        transform.SetParent(target);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    // Smooth Transition
    public void SmoothAttachToTransform(Transform target, float duration = 0.25f) {
        StartCoroutine(SmoothMoveRoutine(target, duration));
    }
    private IEnumerator SmoothMoveRoutine(Transform target, float duration) {
        if (target == null)
            yield break;

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        Vector3 endPos = target.position;
        Quaternion endRot = target.rotation;

        float time = 0f;

        while (time < duration) {
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);

            time += Time.deltaTime;
            yield return null;
        }
        AttachToTransform(target);
    }

    public void Detach() {
        transform.SetParent(null);
    }


    public string GetInteractionText() {
        return _interactionText;
    }
    public virtual bool TryGet(PlayerContext playerContext) {
        if (playerContext.Inspection.TryStartInspect(this)) {
            playerContext.StateMachine.ChangeState(new PlayerInspectState(playerContext));
            return true;
        }
        return false;

    }
    public void ChangePhysics(bool isEnabled) {
        if(IsPhysicsNull())
            return;
        _rigidbody.isKinematic = !isEnabled;
        _collider.enabled = isEnabled;
    }
    private bool IsPhysicsNull() {
        return _rigidbody == null || _collider == null;
    }


    public void InteractStart(PlayerContext playerContext) {
        if (IsInteractable)
            TryGet(playerContext);
    }
    public void InteractHold(PlayerContext playerContext) {}

    public void InteractEnd(PlayerContext playerContext) {}

    public virtual void OnInspectStart() { }
    public virtual void OnInspectEnd() { }
}