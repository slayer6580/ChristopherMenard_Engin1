using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : CharacterState
{
    public override void OnEnter() 
    {
        m_stateMachine.Animator.SetTrigger("GetStun");
    }

    public override void OnUpdate() 
    {
        if (m_stateMachine.IsInContactWithFloor()) 
        {
            m_stateMachine.Animator.SetBool("TouchGround", true);
        }
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
            //si je suis ici, c'est que je suis présentement dans le FallingState et teste
            //si je peux entrer dans Stun

            return m_stateMachine.GetIsStun() && !m_stateMachine.IsInContactWithFloor();
        }
        return false;
    }

    public override bool CanExit() 
    {
        return true;
    }
}
