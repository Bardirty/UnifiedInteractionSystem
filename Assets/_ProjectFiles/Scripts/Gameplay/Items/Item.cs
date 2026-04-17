using UnityEngine;

public class Item : MonoBehaviour, IInteractable {
    [SerializeField] private string _itemName;
    [SerializeField] private string _interactionText = "Pick Item";
    public string ItemName => _itemName;

    public ItemSlot CurrentItemSlot { get; set; }

    private Transform _slot;
    public bool IsInteractable => CurrentItemSlot == null;

    private Rigidbody _rigidbody;
    private Collider _collider;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void SetSlot(Transform slot) {
        _slot = slot;
        if (slot == null) {
            ReleaseSlot();
            return;
        }
        transform.position = slot.position;
        transform.rotation = slot.rotation;
        transform.parent = slot;
    }
    public void ReleaseSlot() {
        transform.parent = null;
        _slot = null;
        CurrentItemSlot = null;
    }
    public string GetInteractionText() {
        return _interactionText;
    }
    public bool TryGet(PlayerContext playerContext) {
        if (playerContext.ItemHolder.TryStartInspect(this)) {
            playerContext.StateMachine.ChangeState(new PlayerInspectState(playerContext));
            return true;
        }
        return false;

    }
    public void InteractStart(PlayerContext playerContext) {
        if (IsInteractable)
            TryGet(playerContext);
    }

    public void InteractHold(PlayerContext playerContext) {}

    public void InteractEnd(PlayerContext playerContext) {}

    public void ChangePhysics(bool isEnabled) {
        _rigidbody.isKinematic = !isEnabled;
        _collider.enabled = isEnabled;
    }
}