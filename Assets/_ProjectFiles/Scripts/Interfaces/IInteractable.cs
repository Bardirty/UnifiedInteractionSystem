public interface IInteractable {
    string GetInteractionText();
    void InteractStart(PlayerContext playerContext);
    void InteractHold(PlayerContext playerContext);
    void InteractEnd(PlayerContext playerContext);
}