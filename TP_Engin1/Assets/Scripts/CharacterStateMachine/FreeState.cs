using UnityEngine;

public class FreeState : CharacterState
{
    public override void OnEnter()
    {
        Debug.Log("Entering State: FreeState");
    }

    public override void OnUpdate()
    {
        Debug.Log("In State: FreeState");
    }

    public override void OnFixedUpdate()
    {

        //TODO 31 AOÛT:
        //Appliquer les déplacements relatifs à la caméra dans les 3 autres directions
        //Avoir des vitesses de déplacements maximales différentes vers les côtés et vers l'arrière
        //Lorsqu'aucun input est mis, décélérer le personnage rapidement

        //Debug.Log(m_stateMachine.RB.velocity.magnitude);

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
        Debug.Log("Existing State: FreeState");
    }

    public override bool CanEnter(CharacterState currentState)
    {
       //This must be run in Update absolutely

       var jumpState = currentState as JumpState;
       if (jumpState != null) 
       { 
            //si je suis ici, c'est que je suis présentement dans le jump state et teste
            //si je peux entrer dans FreeState

            //Je ne peux entrer dans le FreeState que si je touche le sol
            return m_stateMachine.IsInContactWithFloor();
       }

        var fallingState = currentState as FallingState;
        if (fallingState != null)
        {
            //si je suis ici, c'est que je suis présentement dans le falling state et teste
            //si je peux entrer dans FreeState

            //Je ne peux entrer dans le FreeState que si je touche le sol
            return m_stateMachine.IsInContactWithFloor();
        }

        var hitState = currentState as HitState;
        if (hitState != null)
        {

        }
        return false;
    }

    public override bool CanExit()
    {
        return true;
    }
}
