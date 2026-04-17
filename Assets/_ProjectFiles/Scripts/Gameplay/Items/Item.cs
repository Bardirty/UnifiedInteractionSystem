using UnityEngine;

public class Item : MonoBehaviour, IInteractable {
    [SerializeField] private string _itemName;
    [SerializeField] private string _interactionText = "Pick Item";
    public string ItemName => _itemName;

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

    public void Detach() {
        transform.SetParent(null);
    }


    public string GetInteractionText() {
        return _interactionText;
    }
    public bool TryGet(PlayerContext playerContext) {
        if (playerContext.Inspection.TryStartInspect(this)) {
            playerContext.StateMachine.ChangeState(new PlayerInspectState(playerContext));
            return true;
        }
        return false;

    }
    public void ChangePhysics(bool isEnabled) {
        _rigidbody.isKinematic = !isEnabled;
        _collider.enabled = isEnabled;
    }


    public void InteractStart(PlayerContext playerContext) {
        if (IsInteractable)
            TryGet(playerContext);
    }
    public void InteractHold(PlayerContext playerContext) {}

    public void InteractEnd(PlayerContext playerContext) {}
}