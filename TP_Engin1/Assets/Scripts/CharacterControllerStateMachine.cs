using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStateMachine : MonoBehaviour
{
    public Camera Camera { get; private set; }

    [field:SerializeField]
    public Rigidbody Rigibody { get; private set; }

    [field: SerializeField]
    public Animator Animator { get; set; }

    [field: SerializeField]
    public float AccelerationValue { get; private set; }

    [field: SerializeField]
    public float InAirAccelerationValue { get; private set; }

    [field: SerializeField]
    public float MaxVelocity { get; private set; }

    [field: SerializeField]
    public float MaxVelocityInAir { get; private set; }

    [field: SerializeField]
    public float JumpIntensity { get; private set; }

    [field: SerializeField]
    public CharacterFloorTrigger m_floorTrigger;
    private CharacterState m_currentState;
    private List<CharacterState> m_possibleStates;

    private void Awake()
    {
        m_possibleStates = new List<CharacterState>();
        m_possibleStates.Add(new FreeState());
        m_possibleStates.Add(new JumpState());
    }

    void Start()
    {
        Camera = Camera.main;
        //RB = GetComponent<Rigidbody>();

        foreach (CharacterState state in m_possibleStates)
        {
            state.OnStart(this);
        }
        m_currentState = m_possibleStates[0];
        m_currentState.OnEnter();
    }

    private void Update()
    {
        m_currentState.OnUpdate();
        TryStateTransition();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_currentState.OnFixedUpdate();
    }

    private void TryStateTransition()
    {
        if (!m_currentState.CanExit())
        {
            return;
        }

        //Je peux quitter le state actuel
        foreach (var state in m_possibleStates)
        {
            if (m_currentState == state)
            {
                continue;
            }

            if (state.CanEnter())
            {
                //Quiter le state actuel
                m_currentState.OnExit();
                m_currentState = state;
                //Rentrer dans le state state
                m_currentState.OnEnter();
            }
        }
    }

    public bool IsInContactWithFloor() 
    {
        return m_floorTrigger.IsOnFloor;
    }

    public void UpdateAnimatorValues(Vector2 movementVecValue) 
    {
        //Aller chercher ma vitesse actuelle
        //Communiquer directement avec mon animator
        movementVecValue = new Vector2(movementVecValue.x, movementVecValue.y / MaxVelocity);

        Animator.SetFloat("MoveX", movementVecValue.x);
        Animator.SetFloat("MoveY", movementVecValue.y);
    }
}
