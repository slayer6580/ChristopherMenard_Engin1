using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager _Instance 
    {
        get { return _Instance;}
        protected set { _Instance = value; }
    }

    [SerializeField]
    private GameObject m_HitPS;

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
