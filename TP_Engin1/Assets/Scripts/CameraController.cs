using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform m_objectToLookAt;
    [SerializeField]
    private float m_rotationSpeed = 1.0f;
    [SerializeField]
    private Vector2 m_clampingXRotationValues = Vector2.zero;
    [SerializeField]
    private Vector2 m_minMaxDistanceFromTarget;
    [SerializeField]
    [Range(0.1f, 20.0f)]

    private float m_cameraSpeed;

    private Vector3 m_targetPosition;
    public float m_targetDistance;
    private float m_actualDistance;
    private bool m_isObstrcutingCamera = false;

    private void Start()
    {
        m_targetDistance = GetDistanceFromTarget();
        m_actualDistance = GetDistanceFromTarget();
    }

    void Update()
    {
        UpdateHorizontalMovements();
        UpdateVerticalMovements();
        UpdateCameraScroll();
        
    }
    /*
    private void Test()
    {
        Vector3 vecteurDiff = transform.position - m_objectToLookAt.position;
        float distance = vecteurDiff.magnitude;
        Vector3 direction = vecteurDiff.normalized;
        //Debug.Log("Calcul:" + direction * distance);
    }
    */

    private void FixedUpdate()
    {
        MoveCameraInFrontOfObstructionFU();
        LerpCameraPosition();
    }

    private void MoveCameraInFrontOfObstructionFU()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        var vecteurDiff = transform.position - m_objectToLookAt.position;
        var distance = vecteurDiff.magnitude;

        if (Physics.Raycast(m_objectToLookAt.position, vecteurDiff, out hit, distance, layerMask))
        {
            //J'ai un objet entre mon focus et ma caméra
            m_isObstrcutingCamera = true;
            Debug.DrawRay(m_objectToLookAt.position, vecteurDiff.normalized * hit.distance, Color.yellow);
            transform.SetPositionAndRotation(hit.point, transform.rotation);
            m_actualDistance = GetDistanceFromTarget();
        }
        else
        {
            //J'en ai pas
            m_isObstrcutingCamera = false;
            Debug.DrawRay(m_objectToLookAt.position, vecteurDiff, Color.white);
        }
    }

    private void UpdateHorizontalMovements()
    {
        float currentAngleX = Input.GetAxis("Mouse X") * m_rotationSpeed;
        transform.RotateAround(m_objectToLookAt.position, m_objectToLookAt.up, currentAngleX);
    }

    private void UpdateVerticalMovements()
    {
        float currentAngleY = Input.GetAxis("Mouse Y") * m_rotationSpeed;
        float eulersAngleX = transform.rotation.eulerAngles.x;

        float comparisonAngle = eulersAngleX + currentAngleY;

        comparisonAngle = ClampAngle(comparisonAngle);

        if ((currentAngleY < 0 && comparisonAngle < m_clampingXRotationValues.x)
            || (currentAngleY > 0 && comparisonAngle > m_clampingXRotationValues.y))
        {
            return;
        }
        transform.RotateAround(m_objectToLookAt.position, transform.right, currentAngleY);
    }
    
    private void UpdateCameraScroll()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            float newDistance = m_targetDistance - Input.mouseScrollDelta.y;
            m_targetDistance = newDistance;
            m_targetDistance = Mathf.Clamp(m_targetDistance, m_minMaxDistanceFromTarget.x, m_minMaxDistanceFromTarget.y);
        }
    }

    private void LerpCameraPosition()
    {
        if (m_isObstrcutingCamera == false) 
        {
            m_actualDistance = Mathf.Lerp(m_actualDistance, m_targetDistance, m_cameraSpeed * Time.deltaTime);
            float direction = m_actualDistance / Mathf.Abs(m_actualDistance);
            transform.Translate(Vector3.forward * (m_actualDistance - m_targetDistance) * Time.deltaTime, Space.Self);
        }
        
    }

    private float ClampAngle(float angle)
    {
        if (angle > 180)
        {
            angle -= 360;
        }
        return angle;
    }


    private float GetDistanceFromTarget()
    {
        Vector3 vecteurDiff = transform.position - m_objectToLookAt.position;
        float distance = vecteurDiff.magnitude;
        return distance;
    }
}