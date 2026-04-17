using UnityEngine;

public class GameBootstrap : MonoBehaviour {
    [SerializeField] private PlayerContext _playerContext;
    [SerializeField] private GUIManager _guiManager;

    private void Start() {
        _playerContext.Interaction.OnInteractRayTouched += _guiManager.SetInteractionText;
    }

    private void OnDestroy() {
        _playerContext.Interaction.OnInteractRayTouched -= _guiManager.SetInteractionText;
    }
}