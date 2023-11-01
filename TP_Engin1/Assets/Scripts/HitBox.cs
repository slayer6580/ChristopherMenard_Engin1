using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAgentType
{
    Ally,
    Enemy,
    Neutral,
    Count
}

public class HitBox : MonoBehaviour
{
    [SerializeField]
    protected bool m_canHit;
    [SerializeField]
    protected bool m_canBeHit;
    [SerializeField]
    protected EAgentType m_agentType = EAgentType.Count;
    [SerializeField]
    protected List<EAgentType> m_affectedAgentTypes = new List<EAgentType>();

    protected void OnTriggerEnter(Collider other)
    {
        var otherHitBox = other.GetComponent<HitBox>();
        if (otherHitBox == null) { return; }

        if (CanHitOther(otherHitBox)) 
        {
            VFXManager._Instance.InstantiateVFX(EVFX_Type.Hit, other.ClosestPoint(transform.position));
            AudioManager._Instance.PlayAudioClip(EAudio_Type.Hit, other.ClosestPoint(transform.position));
            GetHit(otherHitBox);
        };
    }

    protected bool CanHitOther(HitBox other)
    {
        if (m_canHit && other.m_canBeHit)
        {
            if (m_affectedAgentTypes.Contains(other.m_agentType))
            {
                return true;
            }
        }
        return false;
    }

    protected void GetHit(HitBox otherHitBox) 
    {
        Debug.Log(gameObject.name + " got hit by " + otherHitBox);
    }

}


