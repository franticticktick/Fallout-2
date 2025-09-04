using UnityEngine;

public class FireState : StateMachineBehaviour
{

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var characterController = animator.GetComponent<CharacterController>();
        var character = characterController.Character;
        if (characterController != null)
        {
            // characterController.RaiseMuzzle = true;
            //  characterController.StopMuzzle();
        }
        //  animator.SetBool(character.GetAttackAnimationName(), false);
    }

    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var characterController = animator.GetComponent<CharacterController>();
        var character = characterController.Character;

        AnimatorTransitionInfo transitionInfo = animator.GetAnimatorTransitionInfo(layerIndex);
        if (transitionInfo.IsUserName("BFI"))
        {
            if (characterController != null)
            {
                characterController.RaiseMuzzle = true;
                characterController.StopMuzzle();

                animator.SetBool(character.GetAttackAnimationName(), false);
            }
        }

    }
}
