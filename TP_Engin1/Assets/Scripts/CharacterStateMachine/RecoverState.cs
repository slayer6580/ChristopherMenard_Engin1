using UnityEngine;

public class RecoverState : CharacterState
{
    private const float STATE_EXIT_TIMER = 1.0f;
    private float m_currentStateTimer = 0.0f;
    public override void OnEnter()
    {
        m_currentStateTimer = STATE_EXIT_TIMER;
        m_stateMachine.Animator.SetBool("TouchGround", true);
    }

    public override void OnExit()
    {
        m_stateMachine.UnstunCharacter();
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
    }

    public override bool CanEnter(IState currentState)
    {
        var stunState = currentState as StunState;
        if (stunState != null)
        {
            return m_stateMachine.IsInContactWithFloor();
        }
        return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0.0f;
    }
}
