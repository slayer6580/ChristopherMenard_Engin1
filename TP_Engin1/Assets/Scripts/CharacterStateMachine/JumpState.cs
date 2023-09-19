using UnityEngine;

public class JumpState : CharacterState
{
    private const float STATE_EXIT_TIMER = 0.2f;
    private float m_currentStateTimer = 0.0f;
    public override void OnEnter()
    {
        Debug.Log("Entering State: JumpState");

        //Effectuer le saut
        m_stateMachine.Rigibody.AddForce(Vector3.up * m_stateMachine.JumpIntensity, ForceMode.Acceleration);
        m_currentStateTimer = STATE_EXIT_TIMER;
        m_stateMachine.Animator.SetBool("TouchGround", false);
        m_stateMachine.Animator.SetTrigger("Jump");
    }

    public override void OnExit()
    {
        Debug.Log("Exiting State: JumpState");
        m_stateMachine.Animator.SetBool("TouchGround", true);
    }

    public override void OnFixedUpdate()
    {
        Debug.Log("FixedUpdating State: JumpState");
        Vector3 vectorOnFloor = new Vector3();
        bool isKeyPressed = false;

        if (Input.GetKey(KeyCode.W))
        {
            vectorOnFloor += Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.forward, Vector3.up);
            isKeyPressed = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vectorOnFloor += Vector3.ProjectOnPlane(-m_stateMachine.Camera.transform.forward, Vector3.up);
            isKeyPressed = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vectorOnFloor += Vector3.ProjectOnPlane(m_stateMachine.Camera.transform.right, Vector3.up);
            isKeyPressed = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vectorOnFloor += Vector3.ProjectOnPlane(-m_stateMachine.Camera.transform.right, Vector3.up);
            isKeyPressed = true;
        }
        vectorOnFloor.Normalize();

        if (isKeyPressed) 
        {
            m_stateMachine.Rigibody.AddForce(vectorOnFloor * m_stateMachine.AccelerationValue, ForceMode.Acceleration);
        }
        
        if (m_stateMachine.Rigibody.velocity.magnitude > m_stateMachine.MaxVelocityInAir)
        {
            float x = m_stateMachine.Rigibody.velocity.normalized.x * m_stateMachine.MaxVelocityInAir;
            float y = m_stateMachine.Rigibody.velocity.y;
            float z = m_stateMachine.Rigibody.velocity.normalized.z * m_stateMachine.MaxVelocityInAir;
            
            Vector3 newVelocity = new Vector3(x, y, z);

            //m_stateMachine.Rigibody.velocity = m_stateMachine.Rigibody.velocity.normalized;
            //m_stateMachine.Rigibody.velocity *= m_stateMachine.MaxVelocity;
            m_stateMachine.Rigibody.velocity = newVelocity;
        }
        
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
