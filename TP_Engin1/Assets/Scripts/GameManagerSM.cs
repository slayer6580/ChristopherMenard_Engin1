using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManagerSM : BaseStateMachine<IState>
{
    [SerializeField]
    protected Camera m_gameplayCamera;
    [SerializeField]
    protected Camera m_cinematicCamera;
    protected override void CreatePossibleStates()
    {
        m_possibleStates = new List<IState>();
        m_possibleStates.Add(new GameplayState(m_gameplayCamera));
        m_possibleStates.Add(new CinematicState(m_cinematicCamera));
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
