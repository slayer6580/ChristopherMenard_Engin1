using UnityEngine;

public class FreeState : CharacterState
{
    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {

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
            float x = m_stateMachine.Rigibody.velocity.x * m_stateMachine.AccelerationValue * Time.deltaTime;
            float y = m_stateMachine.Rigibody.velocity.y;
            float z = m_stateMachine.Rigibody.velocity.z * m_stateMachine.AccelerationValue * Time.deltaTime;

            Vector3 newVelocity = new Vector3(x, y, z);

            m_stateMachine.Rigibody.velocity = newVelocity;
        }
        //Debug.Log(m_stateMachine.RB.velocity.magnitude);

        float fowardComponent = Vector3.Dot(m_stateMachine.Rigibody.velocity, vectorOnFloor);
        //m_stateMachine.UpdateAnimatorValues(new Vector2(0, fowardComponent));
        m_stateMachine.UpdateAnimatorValues(new Vector2(vectorOnFloor.x, vectorOnFloor.z));
    }

    public override void OnExit()
    {

    }

    public override bool CanEnter()
    {
        //Je ne peux entrer dans le FreeState que si je touche le sol
        return m_stateMachine.IsInContactWithFloor();
    }

    public override bool CanExit()
    {
        return true;
    }
}
