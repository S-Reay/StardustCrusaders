using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Wander : StateMachineBehaviour
{
    public NavMeshAgent agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        agent = animator.GetComponentInParent<NavMeshAgent>();

        agent.destination = animator.transform.position + (Random.insideUnitSphere * 7);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(agent.velocity, new Vector3(0f, 0f, 0f)) < 0.1f|| !agent.hasPath)
        {
            animator.SetTrigger("FinishedWandering");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("FinishedWandering");
    }
}
