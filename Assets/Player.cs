using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }

    private EntityState _idleState;

    private void Awake()
    {
        stateMachine = new StateMachine();

        _idleState = new EntityState(stateMachine, "Idle");
    }

    private void Start()
    {
        stateMachine.Initialize(_idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }
}
