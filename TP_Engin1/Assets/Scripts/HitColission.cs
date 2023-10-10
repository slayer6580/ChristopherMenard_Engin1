using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitColission : MonoBehaviour
{
    public enum EEntity
    {
        MainCharacter,
        Enemy
    }

    [SerializeField]
    private EEntity m_canBeHitBy;

    private void OnTriggerEnter(Collider other)
    {
        string hitObject = other.transform.gameObject.tag;

        switch (hitObject)
        {
            case "MainCharacter":
                GetHit(EEntity.MainCharacter);
                break;
            case "Enemy":
                GetHit(EEntity.Enemy);
                break;
            default:
                break;
        }
    }

    private bool GetHit(EEntity hittable) 
    {
        if (hittable == m_canBeHitBy) 
        {
            return true;
        }
        return false;
    }
}
