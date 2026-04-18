using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour {
    [SerializeField] private GameObject _questUIPanel;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _checkmark;

    private void Awake() {
        Hide();
    }

    public void Show(ItemType item) {
        _questUIPanel.SetActive(true);
        _text.text = $"Bring: {item}";
        _checkmark.SetActive(false);
    }

    public void Complete() {
        _checkmark.SetActive(true);
        Invoke(nameof(Hide), 2f);
    }

    public void Hide() {
        _questUIPanel.SetActive(false);
    }
}