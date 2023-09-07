using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Camera m_camera;

    [SerializeField]
    private float m_accelerationValue;

    [SerializeField]
    private float m_maxVelocity;

    [SerializeField]
    private float m_decelerationSpeed;

    private Rigidbody m_rb;

    // Start is called before the first frame update
    void Start()
    {
        //Chercher la caméra actuellement utilisé/activé
        m_camera = Camera.main;
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        //TODO 31 aout
        //Appliquer les déplacements relatifs à la caméra dans les 3 aurtes directions
        //Avoir des vitesses de déplacements maximales
        //Lorsqu'aucun input est mis, décélérer le personnage rapidement
    }

    void Move()
    {
        Vector3 vectorOnFloor = new Vector3();
        bool isKeyPressed = false;

        if (Input.GetKey(KeyCode.W))
        {
            vectorOnFloor += Vector3.ProjectOnPlane(m_camera.transform.forward, Vector3.up);
            isKeyPressed = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vectorOnFloor += Vector3.ProjectOnPlane(-m_camera.transform.forward, Vector3.up);
            isKeyPressed = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vectorOnFloor += Vector3.ProjectOnPlane(m_camera.transform.right, Vector3.up);
            isKeyPressed = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vectorOnFloor += Vector3.ProjectOnPlane(-m_camera.transform.right, Vector3.up);
            isKeyPressed = true;
        }
        vectorOnFloor.Normalize();
        m_rb.AddForce(vectorOnFloor * m_accelerationValue, ForceMode.Acceleration);

        if (m_rb.velocity.magnitude > m_maxVelocity)
        {
            m_rb.velocity = m_rb.velocity.normalized;
            m_rb.velocity *= m_maxVelocity;
        }

        if (isKeyPressed == false)
        {
            //m_rb.velocity = m_rb.velocity * m_accelerationValue * Time.deltaTime;
        }
        Debug.Log(m_rb.velocity.magnitude);
    }
}
