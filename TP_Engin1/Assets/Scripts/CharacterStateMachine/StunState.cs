using UnityEngine;

public class StunState : CharacterState
{
    public override void OnEnter() 
    {
        m_stateMachine.Animator.SetTrigger("GetStun");
    }

    public override void OnUpdate() 
    {

    }
    public override void OnFixedUpdate() 
    {

    }
    public override void OnExit() 
    {

    }

    public override bool CanEnter(CharacterState currentState) 
    {
        var fallingState = currentState as FallingState;
        if (fallingState != null)
        {
            return m_stateMachine.GetIsStun() && !m_stateMachine.IsInContactWithFloor();
        }

        var freeState = currentState as FreeState;
        if (freeState != null)
        {
            return m_stateMachine.GetIsStun();
        }
        return false;
    }

    public override bool CanExit() 
    {
        return true;
    }
}
