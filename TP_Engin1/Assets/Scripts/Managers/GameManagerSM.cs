using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManagerSM : BaseStateMachine<IState>
{
    [SerializeField]
    protected CinemachineVirtualCamera m_cinemachineGameplayCamera;
    [SerializeField]
    protected CinemachineVirtualCamera m_cinemachineCinematicCamera;
    protected override void CreatePossibleStates()
    {
        m_possibleStates = new List<IState>();
        m_possibleStates.Add(new GameplayState(m_cinemachineGameplayCamera));
        m_possibleStates.Add(new CinematicState(m_cinemachineCinematicCamera));
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.N)) 
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
        }
    }

}
