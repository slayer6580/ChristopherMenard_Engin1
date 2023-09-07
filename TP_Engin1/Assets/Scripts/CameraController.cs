using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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
    private Vector3 newPosition = new Vector3();
    private float distance = 0;


    // Update is called once per frame
    void Update()
    {
        UpdateHorizontalMovements();
        UpdateVerticalMovements();
        UpdateCameraScroll();
    }

    private void FixedUpdate()
    {
        MoveCameraInFrontOfObstructionFU();
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
            Debug.DrawRay(m_objectToLookAt.position, vecteurDiff.normalized * hit.distance, Color.yellow);
            transform.SetPositionAndRotation(hit.point, transform.rotation);
        }
        else
        {
            //J'en ai pas
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
            //TODO: Faire une vérification selon la distance la plus proche ou la plus éloignée
            //Que je souhaite entre ma caméra et mon objet

            //Mathf.Clamp(distance, m_minMaxDistanceFromTarget.x, m_minMaxDistanceFromTarget.y);

            Vector3 direction = (m_objectToLookAt.transform.position - transform.position).normalized;
            newPosition = direction * Input.mouseScrollDelta.y + transform.position;
            distance = Vector3.Distance(m_objectToLookAt.transform.position, newPosition);
        }

        if (distance > m_minMaxDistanceFromTarget.x && distance < m_minMaxDistanceFromTarget.y)
        {
            //transform.Translate(Vector3.forward * Input.mouseScrollDelta.y, Space.Self);
            //transform.position = newPosition;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, newPosition, m_cameraSpeed * Time.deltaTime);
            transform.position = smoothPosition;
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
}