using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSelector : StateMachineBehaviour
{
    const int Not_Select = -1;
    public int testSelect = Not_Select;

    readonly int IdleSelect_Hash = Animator.StringToHash("IdleSelect");

    int prevSelect = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(IdleSelect_Hash, RandomSelect());
    }

    int RandomSelect()
    {
        int select = 0;

        // 이전 선택이 0번일 경우에만 다른 모션을 재생한다. (전부 다른확률)
        // test_Select가 NotSelect가 아닌 경우 무조건 설정된 값으로 변경 (0~4)만 가능
        if(prevSelect == 0)
        {
            float randValue = Random.value;

            if (randValue < 0.01f)
            {
                select = 4;
            }
            else if (randValue < 0.02f)
            {
                select = 3;
            }
            else if (randValue < 0.03f)
            {
                select = 2;
            }
            else if (randValue < 0.04f)
            {
                select = 1;
            }
        }

        if(testSelect != Not_Select)
        {
            select = testSelect;
        }

        prevSelect = select;

        return select;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
