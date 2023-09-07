using UnityEngine;

public class JumpState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.2f;
    private float m_currentStateTimer = 0.0f;
    public override void OnEnter()
    {
        Debug.Log("Entering State: JumpState");

        //Effectuer le saut
        m_stateMachine.RB.AddForce(Vector3.up * m_stateMachine.JumpIntensity, ForceMode.Acceleration);
        m_currentStateTimer = STATE_EXIT_TIMER;
    }

    public override void OnExit()
    {
        Debug.Log("Exiting State: JumpState");
    }

    public override void OnFixedUpdate()
    {
        Debug.Log("FixedUpdating State: JumpState");
    }

    public override void OnUpdate()
    {
        Debug.Log("Updating State: JumpState");
        m_currentStateTimer -= Time.deltaTime;
    }

    public override bool CanEnter()
    {
        //This must be an update absolutely
        return Input.GetKeyDown(KeyCode.Space);
    }

    public override bool CanExit()
    {
        return m_currentStateTimer <= 0.0f;
    }
}
