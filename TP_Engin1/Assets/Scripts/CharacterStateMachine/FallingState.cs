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
        if (Input.GetKey(KeyCode.H))
        {
            Debug.Log("GOT STUNNED");
            m_stateMachine.GetStunned();
        }
    }
    public override void OnFixedUpdate()
    {
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
    public override void OnExit()
    {
        Debug.Log("Exiting State: FallingState");
        //m_stateMachine.Animator.SetBool("TouchGround", true);
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

        var jumpState = currentState as JumpState;
        if (jumpState != null)
        {
            //si je suis ici, c'est que je suis présentement dans le JumpState et teste
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
