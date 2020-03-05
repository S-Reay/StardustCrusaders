using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Aggresive : StateMachineBehaviour
{
    Transform target;
    NavMeshAgent agent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null)
        {
            agent = animator.GetComponentInParent<NavMeshAgent>();
        }
        target = PlayerManager.instance.player.transform;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(target.position, animator.transform.position);
        agent.SetDestination(target.position);
        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();
            animator.SetTrigger("Attack");
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - agent.GetComponent<Transform>().position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        agent.GetComponent<Transform>().rotation = Quaternion.Slerp(agent.GetComponent<Transform>().rotation, lookRotation, Time.deltaTime * 5f);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
