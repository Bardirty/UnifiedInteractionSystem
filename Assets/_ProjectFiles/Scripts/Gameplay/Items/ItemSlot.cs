using UnityEngine;

public class ItemSlot : MonoBehaviour, IInteractable {
    [SerializeField] private string _interactionTextWithItem = "Pick Item";
    [SerializeField] private string _interactionTextNoItem = "Put Item";
    [SerializeField] private Item _item;

    public string GetInteractionText() {
        return _item != null ? _interactionTextWithItem : _interactionTextNoItem;
    }

    public void Interact(PlayerContext playerContext) {
        if (_item == null && playerContext.ItemHolder.HasItem) {
            Item currentItem = playerContext.ItemHolder.CurrentItem;
            playerContext.ItemHolder.DropCurrentItem();

            SetItem(currentItem);
            return;
        }
        if (_item != null && !playerContext.ItemHolder.HasItem) {
            if (_item.TryGet(playerContext)) {
                _item = null;
            }
        }
    }
    private void SetItem(Item item) {
        if (item != null) {
            _item = item;
            _item.SetSlot(transform);
            _item.CurrentItemSlot = this;
        }
    }

    public void InteractStart(PlayerContext playerContext) {
        Interact(playerContext);
    }

    public void InteractEnd(PlayerContext playerContext) {
    }

    public void InteractHold(PlayerContext playerContext) {
    }

}
