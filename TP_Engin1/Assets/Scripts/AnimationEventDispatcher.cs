using UnityEngine;

public class AnimationEventDispatcher : MonoBehaviour
{
    private CharacterControllerStateMachine m_stateMachineRef;

    public void ActivateAttackHitBox() 
    {
        Debug.Log("true anim Event");
    }

    public void DeactivateAttackHitBox()
    {
        Debug.Log("false anim event");
    }
}
