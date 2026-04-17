public abstract class PlayerBaseState
{
    protected PlayerContext context;
    protected PlayerStateMachine fsm;

    protected PlayerBaseState(PlayerContext context) {
        this.context = context;
        this.fsm = context.StateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void LateUpdate() { }
}
