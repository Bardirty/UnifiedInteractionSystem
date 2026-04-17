using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _interactionText;

    private void Start() {
        SetCursorInGame(true);
    }

    private void SetCursorInGame(bool inGame) {
        Cursor.lockState = inGame ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !inGame;
    }

    public void SetInteractionText(string text) {
        _interactionText.text = text;
        _interactionText.gameObject.SetActive(!string.IsNullOrEmpty(text));
    }
}