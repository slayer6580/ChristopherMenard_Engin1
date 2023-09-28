using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.5f;
    private float m_currentStateTimer = 0.0f;
    public override void OnEnter()
    {
        m_currentStateTimer = STATE_EXIT_TIMER;
        m_stateMachine.Animator.SetBool("TouchGround", true);
    }

    public override void OnExit()
    {
        m_stateMachine.GetUnstunned();
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
    }

    public override bool CanEnter(CharacterState currentState)
    {
        var stunState = currentState as StunState;
        if (stunState != null)
        {
            //si je suis ici, c'est que je suis présentement dans le stunState et teste
            //si je peux entrer dans RecoverState

            //Je ne peux entrer dans le RecoverState que si je touche le sol
            return m_stateMachine.IsInContactWithFloor();
        }
        return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0.0f;
    }
}
