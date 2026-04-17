using UnityEngine;

public class PlayerStateMachine : MonoBehaviour {
    public PlayerBaseState CurrentState { get; private set; }
    private PlayerContext context;

    private void Start() {
        context = GetComponent<PlayerContext>();
        ChangeState(new PlayerIdleState(context));
    }

    public void ChangeState(PlayerBaseState newState) {
        if (CurrentState != null) {
            CurrentState.Exit();
        }

        CurrentState = newState;
        CurrentState.Enter();
    }

    private void Update() {
        CurrentState?.Update();
    }

    private void FixedUpdate() {
        CurrentState?.FixedUpdate();
    }
    private void LateUpdate() {
        CurrentState?.LateUpdate();
    }
}
