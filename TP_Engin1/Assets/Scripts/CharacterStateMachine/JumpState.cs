using UnityEngine;

public class JumpState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.1f;
    private float m_currentStateTimer = 0.0f;
    //private bool m_CanEnter => m_stateMachine.IsInContactWithFloor() && Input.GetKeyDown(KeyCode.Space); //equivalent avec =>
    public override void OnEnter()
    {
        //Effectuer le saut
        m_stateMachine.Rigibody.AddForce(Vector3.up * m_stateMachine.JumpIntensity, ForceMode.Acceleration);
        m_currentStateTimer = STATE_EXIT_TIMER;
        m_stateMachine.Animator.SetBool("TouchGround", false);
        m_stateMachine.Animator.SetTrigger("Jump");
    }

    public override void OnExit()
    {
        m_stateMachine.Animator.SetBool("TouchGround", true);
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
        var freeState = currentState as FreeState;
        if (freeState != null)
        {
            return m_stateMachine.IsInContactWithFloor() && Input.GetKeyDown(KeyCode.Space);
        }
        return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0.0f;
    }
}
