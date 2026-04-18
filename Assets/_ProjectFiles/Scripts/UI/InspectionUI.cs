using TMPro;
using UnityEngine;

public class InspectionUI : MonoBehaviour {
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    private void Awake() {
        Hide();
    }

    public void Show(string description) {
        if (_panel != null)
            _panel.SetActive(true);

        if (_descriptionText != null)
            _descriptionText.text = description;
    }

    public void Hide() {
        if (_panel != null)
            _panel.SetActive(false);
    }
}