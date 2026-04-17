using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable {
    private const string OPEN_TRIGGER = "Open";
    private const string WRONG_TRIGGER = "Wrong";

    [Header("Settings")]
    [SerializeField] private ItemType _requiredItemType = ItemType.Key;
    [SerializeField] private string _interactionText = "Open Chest";

    [Header("References")]
    [SerializeField] private Animator _animator;


    private bool _isOpened;

    public string GetInteractionText() {
        if (_isOpened)
            return string.Empty;

        return _interactionText;
    }

    public void InteractStart(PlayerContext context) {
        if (_isOpened)
            return;

        if (!context.ItemHolder.HasItem) {
            Wrong();
            return;
        }

        Item currentItem = context.ItemHolder.CurrentItem;

        if (currentItem.ItemType == _requiredItemType) {
            Open(context);
        }
        else Wrong();
    }
    private void Wrong() {
        if (_animator != null)
            _animator.SetTrigger(WRONG_TRIGGER);
    }

    private void Open(PlayerContext context) {
        _isOpened = true;
        if (_animator != null)
            _animator.SetTrigger(OPEN_TRIGGER);
        Item item = context.ItemHolder.CurrentItem;

        context.ItemHolder.DropCurrentItem();

        if (item != null)
            Destroy(item.gameObject);
    }

    public void InteractHold(PlayerContext context) { }

    public void InteractEnd(PlayerContext context) { }
}