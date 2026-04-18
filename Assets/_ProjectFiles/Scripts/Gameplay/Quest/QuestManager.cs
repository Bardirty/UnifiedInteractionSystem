using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour {
    [SerializeField] private ItemType[] _possibleItems;
    public ItemType RequiredItem { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsCompleted { get; private set; }

    public event Action<ItemType> OnQuestStarted;
    public event Action OnQuestCompleted;

    private void Start() {
        BindQuestListeners();
    }
    public void StartQuest() {
        RequiredItem = GetRandomItem();
        IsActive = true;
        IsCompleted = false;
        OnQuestStarted?.Invoke(RequiredItem);
    }
    private ItemType GetRandomItem() {
        if (_possibleItems == null || _possibleItems.Length == 0) {
            Debug.LogWarning("No quest items configured!");
            return ItemType.Cube;
        }

        return _possibleItems[UnityEngine.Random.Range(0, _possibleItems.Length)];
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