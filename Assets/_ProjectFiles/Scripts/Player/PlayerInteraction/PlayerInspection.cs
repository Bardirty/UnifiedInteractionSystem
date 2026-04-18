using System;
using UnityEngine;

public class PlayerInspection : MonoBehaviour {
    public Item InspectingItem { get; private set; }
    public bool IsInspecting { get; private set; }

    public event Action<Item> OnInspectStarted;
    public event Action OnInspectEnded;

    public bool TryStartInspect(Item item) {
        if (item == null || IsInspecting)
            return false;

        InspectingItem = item;
        IsInspecting = true;
        OnInspectStarted?.Invoke(item);
        return true;
    }
    public void ConfirmInspect() {
        if (InspectingItem == null)
            return;
        InspectingItem = null;
        IsInspecting = false;
        OnInspectEnded?.Invoke();
    }

}
