using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : CharacterState
{
    public override void OnEnter()
    {
        m_stateMachine.Animator.SetTrigger("GetHit");
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
        var freeState = currentState as FreeState;
        if (freeState != null)
        {
            //si je suis ici, c'est que je suis présentement dans le Free state et teste
            //si je peux entrer dans JumpState

            //Je ne peux entrer dans le FreeState que si je touche le sol
            return true;
        }
        return false;
    }

    public override bool CanExit()
    {
        return true;
    }
}
