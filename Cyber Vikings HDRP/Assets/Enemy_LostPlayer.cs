using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_LostPlayer : StateMachineBehaviour
{
    Transform player;
    EnemyStats stats;
    int layerMask;


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
        layerMask = ~LayerMask.GetMask("EnemyLayer");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(animator.transform.position, player.position) < stats.detectRadius)
        {
            RaycastHit hit;
            if (Physics.Raycast(animator.transform.position, player.position - animator.transform.position, out hit, layerMask))
            {
                if (hit.transform.tag != "Player")
                {
                    //Lost Player
                    animator.SetTrigger("LostPlayer");
                }
            }
        }
        else
        {
            //Lost player
            animator.SetTrigger("LostPlayer");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("LostPlayer");
    }
}
