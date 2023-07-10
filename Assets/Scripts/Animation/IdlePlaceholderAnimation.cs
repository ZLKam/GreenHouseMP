using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlaceholderAnimation : StateMachineBehaviour
{
    private bool started = false;

    private float random;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (started)
            return;
        random = Random.Range(0f, 1f);
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animator.Play(animatorStateInfo.fullPathHash, -1, random);
        started = true;
    }
}
