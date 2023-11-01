using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager _Instance { get; protected set; }

    [SerializeField]
    private GameObject m_HitPS;

    [SerializeField]
    private AnimationCurve m_TimeCurve;

    [SerializeField]
    private CameraShake m_cameraShake;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else if (_Instance != this) 
        {
            Destroy(gameObject);
        }
    }

    public void InstantiateVFX(EVFX_Type vfxType, Vector3 pos) 
    {
        switch (vfxType)
        {
            case EVFX_Type.Hit:
                Instantiate(m_HitPS, pos, Quaternion.identity, transform);
                m_cameraShake.ShakeCamera();
                break;
            default:
                break;
        }
    }
}

public enum EVFX_Type
{
    Hit,
    Count
}
