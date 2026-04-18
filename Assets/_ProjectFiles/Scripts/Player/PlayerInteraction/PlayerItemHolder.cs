using System;
using UnityEngine;

public class PlayerItemHolder : MonoBehaviour {
    public Item CurrentItem { get; private set; }
    public bool HasItem => CurrentItem != null;

    private Transform _itemHoldPoint;

    public void Initialize(Transform itemHoldPoint) {
        _itemHoldPoint = itemHoldPoint;
    }

    public bool TryHoldItem(Item item) {
        if (HasItem || item == null)
            return false;

        CurrentItem = item;
        CurrentItem.DetachFromSlot();
        CurrentItem.ChangePhysics(false);
        CurrentItem.SmoothAttachToTransform(_itemHoldPoint);
        return true; 
    }
    public void DropCurrentItem() {
        if(CurrentItem != null) {
            CurrentItem.ChangePhysics(true);
            CurrentItem.Detach();
            CurrentItem = null;
        }
    }
}
