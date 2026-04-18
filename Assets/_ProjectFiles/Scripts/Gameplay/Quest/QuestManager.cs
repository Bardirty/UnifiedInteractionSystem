using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour {
    public ItemType RequiredItem { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsCompleted { get; private set; }

    public event Action<ItemType> OnQuestStarted;
    public event Action OnQuestCompleted;
    private List<IQuestInjectable> _listeners = new ();

    private void Start() {
        BindQuestListeners();
    }
    public void StartQuest(ItemType item) {
        RequiredItem = item;
        IsActive = true;
        IsCompleted = false;
        OnQuestStarted?.Invoke(item);
    }

    public void TryComplete(Item item) {
        if (!IsActive || item == null)
            return;

        if (item.ItemType == RequiredItem) {
            IsCompleted = true;
            IsActive = false;

            OnQuestCompleted?.Invoke();
        }
    }
    public void TryCompleteWithPlayer(PlayerContext player) {
        Debug.Log("Trying to complete quest with player item...");
        var item = player.ItemHolder.CurrentItem;

        bool wasCompletedBefore = IsCompleted;

        TryComplete(item);

        if (!wasCompletedBefore && IsCompleted && item != null) {
            player.ItemHolder.DropCurrentItem();
            Destroy(item.gameObject);
        }
    }

    private void BindQuestListeners() {
        IQuestInjectable[] listeners = FindObjectsByType<MonoBehaviour>((FindObjectsSortMode.None))
            .OfType<IQuestInjectable>().ToArray();

        foreach (IQuestInjectable injectable in listeners) {
            injectable.Inject(this);
        }
    }
}