using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    public float health = 100f;

    Animator animator;
    public NavMeshAgent navMeshAgent;

    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            ScoreSystem.scoreValue += 1;
            navMeshAgent.isStopped = true;
            animator.SetTrigger("Dead");
            StartCoroutine(Death());
        }
    }


    IEnumerator Death()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
