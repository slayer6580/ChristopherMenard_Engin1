using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : CharacterState
{
    public override void OnEnter()
    {
        Debug.Log("Entering State: FallingState");
        m_stateMachine.Animator.SetBool("TouchGround", false);
    }

    public override void OnUpdate()
    {

    }
    public override void OnFixedUpdate()
    {

    }
    public override void OnExit()
    {
        Debug.Log("Exiting State: FallingState");
        m_stateMachine.Animator.SetBool("TouchGround", true);
    }

    public override bool CanEnter(CharacterState currentState)
    {
        var freeState = currentState as FreeState;
        if (freeState != null)
        {
            //si je suis ici, c'est que je suis présentement dans le FreeState et teste
            //si je peux entrer dans FallingState
            return !m_stateMachine.IsInContactWithFloor();
        }
        return false;
    }

    public override bool CanExit()
    {
        return true;
    }
}
