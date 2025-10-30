using UnityEngine;

public class EntityState
{
    protected StateMachine stateMachine;
    protected string stateName;

    public EntityState(StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }


    // evertime state will be changed, this method will be called
    public virtual void Enter()
    {

    }

    // this method will be called every frame when we are going to run logic of the state here
    public virtual void Update()
    {
        
    }

    // this method will be called when we exiting state and going to another state
    public virtual void Exit()
    {
        
    }
}
