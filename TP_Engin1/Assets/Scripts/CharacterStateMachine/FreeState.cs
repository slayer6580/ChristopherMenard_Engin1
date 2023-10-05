using UnityEngine;

public class FreeState : CharacterState
{
    public override void OnEnter()
    {
        //Debug.Log("Entering State: FreeState");
        m_stateMachine.Animator.SetBool("TouchGround", true);
    }

    public override void OnUpdate()
    {
        //Debug.Log("In State: FreeState");
        if (Input.GetMouseButtonDown(0))
        {
            m_stateMachine.Attack();
        }

        if (Input.GetKey(KeyCode.H))
        {
            Debug.Log("GOT STUNNED");
            m_stateMachine.StunCharacter();
        }
    }

    /*
     * Par exemple, si vous allez à un angle nord-nord-ouest 
     * (3/4 du déplacement 	vers l'avant, 1/4 vers la gauche), et que votre vitesse maximale de 
     * déplacement avant est 20 et vers les côtés 5, votre vitesse maximale calculée 
     * à ce moment devrait être de ((3/4) * 20 + (1/4) * 5) == 15 + 1.25 == 16.25
     */

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
        m_stateMachine.Rigibody.AddForce(vectorOnFloor * m_stateMachine.AccelerationValue, ForceMode.Acceleration);

        if (m_stateMachine.Rigibody.velocity.magnitude > m_stateMachine.MaxVelocity)
        {
            m_stateMachine.Rigibody.velocity = m_stateMachine.Rigibody.velocity.normalized;
            m_stateMachine.Rigibody.velocity *= m_stateMachine.MaxVelocity;
        }

        if (isKeyPressed == false)
        {
            float x = m_stateMachine.Rigibody.velocity.x * m_stateMachine.AccelerationValue * Time.deltaTime;//mettre un scalaire(float)
            float y = m_stateMachine.Rigibody.velocity.y;
            float z = m_stateMachine.Rigibody.velocity.z * m_stateMachine.AccelerationValue * Time.deltaTime;

            Vector3 newVelocity = new Vector3(x, y, z);

            m_stateMachine.Rigibody.velocity = newVelocity;
        }

        float fowardComponent = Vector3.Dot(m_stateMachine.Rigibody.velocity, vectorOnFloor);
        //m_stateMachine.UpdateAnimatorValues(new Vector2(0, fowardComponent));
        m_stateMachine.UpdateAnimatorValues(new Vector2(vectorOnFloor.x, vectorOnFloor.z));
    }

    public override void OnExit()
    {
        
    }

    public override bool CanEnter(CharacterState currentState)
    {
       //This must be run in Update absolutely

       var jumpState = currentState as JumpState;
       if (jumpState != null) 
       { 
            return m_stateMachine.IsInContactWithFloor();
       }

        var fallingState = currentState as FallingState;
        if (fallingState != null)
        {
            return m_stateMachine.IsInContactWithFloor();
        }

        var recoverState = currentState as RecoverState;
        if (recoverState != null)
        {
            return m_stateMachine.IsInContactWithFloor();
        }

        var attackState = currentState as AttackState;
        if (attackState != null)
        {
            return !m_stateMachine.GetIsAttacking();
        }

        return false;
    }

    public override bool CanExit()
    {
        return true;
    }
}
