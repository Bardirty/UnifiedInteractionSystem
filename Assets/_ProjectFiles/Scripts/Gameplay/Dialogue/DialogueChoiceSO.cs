using UnityEngine;

[CreateAssetMenu(fileName = "DialogueChoice", menuName = "Dialogue/DialogueChoice")]
public class DialogueChoiceSO : ScriptableObject {
    public string Text;
    public DialogueNodeSO NextNode;
}