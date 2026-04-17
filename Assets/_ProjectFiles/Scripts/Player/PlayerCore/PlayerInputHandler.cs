using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonInfo {
    public bool IsDown { get; set; }
    public bool IsHeld { get; set; }
    public bool IsUp { get; set; }
    public float HoldTime { get; set; }
}
public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem_Actions _input;
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public ButtonInfo Interact { get; private set; }

    private void Awake() {
        _input = new InputSystem_Actions();
        
        _input.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        _input.Player.Move.canceled += _ => MoveInput = Vector2.zero;
        _input.Player.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        _input.Player.Look.canceled += _ => LookInput = Vector2.zero;

        BindButtonEvents(Interact = new ButtonInfo(), _input.Player.Interact);
    }
    private void OnEnable() {
        _input.Enable();
    }
    private void OnDisable() {
        _input.Disable();
    }
    private void BindButtonEvents(ButtonInfo button, InputAction action) {
        action.started += _ => {
            button.IsDown = true;
            button.IsHeld = true;
            button.IsUp = false;
            button.HoldTime = 0f;
        };
        action.canceled += _ => {
            button.IsHeld = false;
            button.IsUp = true;
            button.HoldTime = 0f;
        };
    }
    //Button Update
    private void Update() {
        UpdateButton(Interact);
    }

    private void UpdateButton(ButtonInfo button) {
        if (button.IsHeld)
            button.HoldTime += Time.deltaTime;
    }
    //Button LateUpdate (after successive frames)
    private void LateUpdate() {
        LateUpdateButton(Interact);
    }
    private void LateUpdateButton(ButtonInfo button) {
        button.IsDown = false;
        button.IsUp = false;
    }
}
