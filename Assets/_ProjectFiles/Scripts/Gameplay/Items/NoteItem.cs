using UnityEngine;

public class NoteItem : Item {
    
    [SerializeField] private Animator _animator;
    private static readonly int OpenHash = Animator.StringToHash("Open");
    private static readonly int CloseHash = Animator.StringToHash("Close");
    public override void OnInspectStart() {
        if (_animator != null)
            _animator.SetTrigger(OpenHash);
    }

    public override void OnInspectEnd() {
        if (_animator != null)
            _animator.SetTrigger(CloseHash);
    }
}