using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : MonoBehaviour
{
    public void OnStart()
    {

    }

    public virtual void OnEnter()
    {
    }

    public virtual void OnExit()
    {
    }

    public virtual void OnFixedUpdate()
    {
    }

    public virtual void OnUpdate()
    {
    }

    public virtual bool CanEnter()
    {
        return true;
    }

    public virtual bool CanExit()
    {
        return true;
    }
}
