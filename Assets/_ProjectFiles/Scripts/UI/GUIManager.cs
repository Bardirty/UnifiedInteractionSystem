using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _interactionText;

    private void Start() {
        SetCursor(false);
    }

    public void SetCursor(bool isEnable) {
        Cursor.lockState = !isEnable ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = isEnable;
    }

    public void SetInteractionText(string text) {
        _interactionText.text = text;
        _interactionText.gameObject.SetActive(!string.IsNullOrEmpty(text));
    }
}