using UnityEngine;

public enum DialogueNodeType {
    Default,
    QuestStart,
    QuestComplete,
}
[CreateAssetMenu(menuName = "Dialogue/Node")]
public class DialogueNodeSO : ScriptableObject {
    [TextArea] public string Text;
    [SerializeField] private DialogueNodeType _nodeType;
    [SerializeField] private ItemType _questItem;

    public DialogueNodeSO NextNode;
    public DialogueChoiceSO[] Choices;

    // References
    public bool HasChoices => Choices != null && Choices.Length > 0;
    public DialogueNodeType NodeType => _nodeType;
    public ItemType QuestItem => _questItem;
}