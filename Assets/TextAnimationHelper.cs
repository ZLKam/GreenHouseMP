using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextAnimationHelper : StateMachineBehaviour
{
    private char _text = '.';
    private TextMeshProUGUI text;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        text = animator.GetComponent<TestGearAnimation>().text;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int frame = Mathf.RoundToInt(stateInfo.normalizedTime * 3f % 4);
        text.text = (new string(_text, frame));
    }
}
