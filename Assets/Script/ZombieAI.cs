using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public GameObject player;

    public NavMeshAgent navMeshAgent;
    public Healthbar healthSystem;

    Animator animator;

    public float attackRange = 2f;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthSystem = player.transform.GetComponent<Healthbar>();
    }

    // Update is called once per frame
    void Update()
    {

        navMeshAgent.SetDestination(player.transform.position);

        if (Vector3.Distance(transform.position, player.transform.position) < attackRange && animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            animator.SetTrigger("Attack");
            StartCoroutine(WaitForAttack(1.2f));

        }
    }

    public IEnumerator WaitForAttack(float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
        {
            healthSystem.TakeDamage(0.6f);// hàm này sẽ gọi liên tục khi attack kết thúc nên để dame bé đi tránh tình trạng bị player nhận quá nhiều dame
        }
    }
}
