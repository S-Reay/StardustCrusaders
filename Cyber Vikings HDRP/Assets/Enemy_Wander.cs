using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Wander : StateMachineBehaviour
{
    Transform player;
    EnemyStats stats;
    public NavMeshAgent agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (stats == null)
        {
            stats = animator.GetComponentInParent<EnemyStats>();
        }

        Random.InitState(System.DateTime.Now.Millisecond);
        agent = animator.GetComponentInParent<NavMeshAgent>();

        agent.destination = animator.transform.position + (Random.insideUnitSphere * 10);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(agent.velocity, new Vector3(0f, 0f, 0f)) < 0.1f|| !agent.hasPath)
        {
            Debug.LogWarning("finished");
            animator.SetTrigger("FinishedWandering");
        }

        if (Vector3.Distance(animator.transform.position, player.position) < stats.detectRadius)
        {
            RaycastHit hit;
            if (Physics.Raycast(animator.transform.position, player.position - animator.transform.position, out hit))
            {
                if (hit.transform == player)
                {
                    animator.SetTrigger("Aggresive");
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("FinishedWandering");
    }
}
