using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public GameObject player;

    public NavMeshAgent navMeshAgent;

    Animator animator;

    public float attackRange = 2f;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        navMeshAgent.SetDestination(player.transform.position);
        //transform.LookAt(player.gameObject.transform);
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            animator.SetTrigger("Attack");
        }
    }
}
