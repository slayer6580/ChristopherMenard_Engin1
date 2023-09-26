using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : CharacterState
{
    public override void OnEnter() 
    {

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
            Debug.Log("Test");
            //si je suis ici, c'est que je suis présentement dans le InAirState et teste
            //si je peux entrer dans JumpState

            //Je ne peux entrer dans le FreeState que si je touche le sol
            //return !m_stateMachine.IsInContactWithFloor() && Input.GetKeyDown(KeyCode.Space);
        }
        return false;
    }

    public override bool CanExit() 
    {
        return false;
    }
}
