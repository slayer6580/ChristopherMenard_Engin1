using Cinemachine;
using UnityEngine;

public class GameplayState : IState
{
    protected CinemachineVirtualCamera m_camera;
    public GameplayState(CinemachineVirtualCamera camera) 
    {
        m_camera = camera;
    }
    public bool CanEnter(IState currentState)
    {
        return Input.GetKeyDown(KeyCode.G);
    }

    public bool CanExit()
    {
        return Input.GetKeyDown(KeyCode.G);
    }

    public void OnEnter()
    {
        Debug.Log("On Enter GameplayState");
        m_camera.enabled = true;
    }

    public void OnExit()
    {
        Debug.Log("On Exit GameplayState");
        m_camera.enabled = false;
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
