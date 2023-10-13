using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class AttackState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.5f;
    private float m_currentStateTimer = 0.0f;
    public override void OnEnter()
    {
        m_stateMachine.Animator.SetTrigger("Attack");
        m_currentStateTimer = STATE_EXIT_TIMER;
    }

    public override void OnUpdate()
    {
        m_currentStateTimer -= Time.deltaTime;
        if (m_currentStateTimer <= 0.0f) 
        {
            m_stateMachine.StopAttack();
        }
    }
    public override void OnFixedUpdate()
    {

    }
    public override void OnExit()
    {
        m_stateMachine.HitBox.SetActive(false);
    }

    public override bool CanEnter(IState currentState)
    {
        var freeState = currentState as FreeState;
        if (freeState != null)
        {
            //si je suis ici, c'est que je suis présentement dans le FreeState et teste
            //si je peux entrer dans Attack
            
            return m_stateMachine.GetIsAttacking();
        }
        return false;
    }

    public override bool CanExit()
    {
        //return m_currentStateTimer <= 0.0f;
        return !m_stateMachine.GetIsAttacking();
    }
}
