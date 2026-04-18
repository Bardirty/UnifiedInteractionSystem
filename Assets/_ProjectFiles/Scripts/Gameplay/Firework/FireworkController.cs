using UnityEngine;

public class FireworkController : MonoBehaviour {
    [SerializeField] private ButtonInteractable _button;
    [SerializeField] private ParticleSystem _firework;

    private void OnEnable() {
        if (_button != null)
            _button.OnButtonPressed += PlayFirework;
    }

    private void OnDisable() {
        if (_button != null)
            _button.OnButtonPressed -= PlayFirework;
    }

    private void PlayFirework() {
        if (_firework != null)
            _firework.Play();
    }
}