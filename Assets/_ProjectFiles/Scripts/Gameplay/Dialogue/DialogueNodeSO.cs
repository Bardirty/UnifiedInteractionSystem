using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Node")]
public class DialogueNodeSO : ScriptableObject {
    [TextArea] public string Text;

    public DialogueNodeSO NextNode;
    public DialogueChoiceSO[] Choices;
    public bool HasChoices => Choices != null && Choices.Length > 0;
}