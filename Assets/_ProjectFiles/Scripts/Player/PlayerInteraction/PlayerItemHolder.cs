using System;
using UnityEngine;

public class PlayerItemHolder : MonoBehaviour {
    public Item CurrentItem { get; private set; }
    public bool HasItem => CurrentItem != null;
    public bool IsInspecting { get; private set; }
    public Item InspectingItem { get; private set; }

    public event Action<Item> OnStartInspect;
    public event Action OnEndInspect;

    private Transform _itemHoldPoint;

    public void Initialize(Transform itemHoldPoint) {
        _itemHoldPoint = itemHoldPoint;
    }

    public bool TryHoldItem(Item item) {
        if (CurrentItem != null || item == null)
            return false;

        CurrentItem = item;
        CurrentItem.ChangePhysics(false);
        CurrentItem.SetSlot(_itemHoldPoint);

        return true;
    }
    public void DropCurrentItem() {
        if(CurrentItem != null) {
            CurrentItem.ChangePhysics(true);
            CurrentItem.ReleaseSlot();
            CurrentItem = null;
        }
    }

    public bool TryStartInspect(Item item) {
        if (item == null || HasItem || IsInspecting)
            return false;

        InspectingItem = item;
        IsInspecting = true;
        OnStartInspect?.Invoke(item);
        return true;
    }
    public void ConfirmInspect() {
        if (InspectingItem == null)
            return;
        
        if (TryHoldItem(InspectingItem)) {
            InspectingItem = null;
            IsInspecting = false;
            OnEndInspect?.Invoke();
        }
    }
}
