using UnityEngine;

public class AnimationEventDispatcher : MonoBehaviour
{
    [SerializeField]
    private CharacterControllerStateMachine m_stateMachineRef;

    public void ActivateAttackHitBox() 
    {
        //Debug.Log("true anim Event");
        m_stateMachineRef.SetHitBoxState(true);
    }

    public void DeactivateAttackHitBox()
    {
        //Debug.Log("false anim event");
        m_stateMachineRef.SetHitBoxState(true);
    }
}
