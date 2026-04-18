using UnityEngine;

public abstract class BaseNPC : MonoBehaviour, IInteractable {
    public abstract string GetInteractionText();

    public abstract void InteractStart(PlayerContext context);

    public virtual void InteractHold(PlayerContext context) { }
    public virtual void InteractEnd(PlayerContext context) { }
}