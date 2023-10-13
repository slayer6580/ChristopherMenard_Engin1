using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerStateMachine : BaseStateMachine<CharacterState>
{
    public Camera Camera { get; private set; }

    [field: SerializeField]
    public Rigidbody Rigibody { get; private set; }

    [field: SerializeField]
    public Collider Collider { get; private set; }

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
    public GameObject HitBox { get; private set; }

    [field: SerializeField]
    public CharacterFloorTrigger m_floorTrigger;

    private bool m_isStun = false;
    private bool m_isAttacking = false;

    [SerializeField]
    private float m_animationSpeed;
    private float m_xValue;
    private float m_yValue;

    private void Awake()
    {
        CreatePossibleStates();
    }

    protected override void CreatePossibleStates() 
    {
        m_possibleStates = new List<CharacterState>();
        m_possibleStates.Add(new FreeState());
        m_possibleStates.Add(new JumpState());
        m_possibleStates.Add(new FallingState());
        m_possibleStates.Add(new StunState());
        m_possibleStates.Add(new RecoverState());
        m_possibleStates.Add(new AttackState());
    }

    protected override void Start()
    {
        foreach (CharacterState state in m_possibleStates)
        {
            state.OnStart(this);
        }
        m_currentState = m_possibleStates[0];
        m_currentState.OnEnter();

        Camera = Camera.main;
        Camera = Camera.main;
    }

    protected override void Update()
    {
        base.Update();
        TryStateTransition();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
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
        m_xValue = Mathf.Lerp(m_xValue, movementVecValue.x, Time.deltaTime * m_animationSpeed);
        m_yValue = Mathf.Lerp(m_yValue, movementVecValue.y, Time.deltaTime * m_animationSpeed);

        Animator.SetFloat("MoveX", m_xValue);
        Animator.SetFloat("MoveY", m_yValue);
    }
    public void StunCharacter()
    {
        m_isStun = true;
    }
    public void UnstunCharacter()
    {
        m_isStun = false;
    }

    public void Attack() 
    {
        m_isAttacking = true;
    }

    public void StopAttack() 
    {
        m_isAttacking = false;
    }

    public bool GetIsAttacking()
    {
        return m_isAttacking;
    }

    public void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other.transform.gameObject.name);
        if (other.transform.tag == "Stun") 
        {
            StunCharacter();
        }
    }

    public bool GetIsStun() 
    {
        return m_isStun;
    }
}
