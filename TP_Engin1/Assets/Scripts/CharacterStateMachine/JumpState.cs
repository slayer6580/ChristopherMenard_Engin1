using UnityEngine;

public class JumpState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.1f;
    private float m_currentStateTimer = 0.0f;
    //private bool m_CanEnter => m_stateMachine.IsInContactWithFloor() && Input.GetKeyDown(KeyCode.Space); //equivalent avec =>
    public override void OnEnter()
    {
        //Debug.Log("Entering State: JumpState");

        //Effectuer le saut
        m_stateMachine.Rigibody.AddForce(Vector3.up * m_stateMachine.JumpIntensity, ForceMode.Acceleration);
        m_currentStateTimer = STATE_EXIT_TIMER;
        m_stateMachine.Animator.SetBool("TouchGround", false);
        m_stateMachine.Animator.SetTrigger("Jump");
    }

    public override void OnExit()
    {
        //Debug.Log("Exiting State: JumpState");
        m_stateMachine.Animator.SetBool("TouchGround", true);
    }

    public override void OnFixedUpdate()
    {
        //Debug.Log("FixedUpdating State: JumpState");

    }

    public override void OnUpdate()
    {
        //Debug.Log("Updating State: JumpState");
        m_currentStateTimer -= Time.deltaTime;
    }

    public override bool CanEnter(CharacterState currentState)
    {
        //This must be an update absolutely
        //return Input.GetKeyDown(KeyCode.Space);

        var freeState = currentState as FreeState;
        if (freeState != null)
        {
            //si je suis ici, c'est que je suis présentement dans le Free state et teste
            //si je peux entrer dans JumpState
            return m_stateMachine.IsInContactWithFloor() && Input.GetKeyDown(KeyCode.Space);
        }
        return false;
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0.0f;
    }
}
