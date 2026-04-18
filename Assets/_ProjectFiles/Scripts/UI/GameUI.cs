using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour {
    [SerializeField] private GameObject guiPanel;
    [SerializeField] private TextMeshProUGUI _interactionText;

    private void Start() {
        SetCursor(false);
    }

    public void SetCursor(bool isEnable) {
        Cursor.lockState = !isEnable ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = isEnable;
    }
    public void ToggleGUI(bool isEnable) {
        guiPanel.SetActive(isEnable);
        SetCursor(isEnable);
    }

    public void SetInteractionText(string text) {
        _interactionText.text = text;
        _interactionText.gameObject.SetActive(!string.IsNullOrEmpty(text));
    }
}