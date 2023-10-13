using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicState : IState
{
    protected Camera m_camera;
    public CinematicState(Camera camera)
    {
        m_camera = camera;
    }

    public bool CanEnter(IState currentState)
    {
        throw new System.NotImplementedException();
    }

    public bool CanExit()
    {
        return Input.GetKeyDown(KeyCode.G);
    }

    public void OnEnter()
    {
        Debug.Log("On Enter CinematicState");
    }

    public void OnExit()
    {
        Debug.Log("On Exit CinematicState");
    }

    public void OnFixedUpdate()
    {
        
    }

    public void OnStart()
    {
        
    }

    public void OnUpdate()
    {
        
    }
}
