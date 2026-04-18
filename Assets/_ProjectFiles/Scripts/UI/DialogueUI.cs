using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogueUI : MonoBehaviour {
    private DialogueManager _controller;
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Transform _choicesRoot;
    [SerializeField] private GameObject _choicePrefab;

    private void Awake() {
        _choicesRoot.gameObject.SetActive(false);
    }
    public void Bind(DialogueManager controller) {
        _controller = controller;

        _controller.OnTextChanged += SetText;
        _controller.OnChoicesChanged += SetChoices;
    }
    public void Unbind() {
        if (_controller == null) return;
        _controller.OnTextChanged -= SetText;
        _controller.OnChoicesChanged -= SetChoices;
        _controller = null;
    }
    private void OnDestroy() {
        Unbind();
    }

    public void SetDialoguePanelActive(bool isActive) {
        _dialoguePanel.SetActive(isActive);
    }

    private void SetText(string text) {
        _text.text = text;
    }

    private void SetChoices(DialogueChoiceSO[] choices) {
        for (int i = _choicesRoot.childCount - 1; i >= 0; i--) {
            Destroy(_choicesRoot.GetChild(i).gameObject);
        }

        bool hasChoices = choices != null && choices.Length > 0;

        _choicesRoot.gameObject.SetActive(hasChoices);

        if (!hasChoices)
            return;

        for (int i = 0; i < choices.Length; i++) {
            int index = i;

            var go = Instantiate(_choicePrefab, _choicesRoot);
            var button = go.GetComponent<Button>();
            var label = go.GetComponentInChildren<TextMeshProUGUI>();

            label.text = choices[i].Text;

            button.onClick.AddListener(() =>
                _controller?.SelectChoice(index)
            );
        }
    }
}