using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public GameObject player;

    public NavMeshAgent navMeshAgent;
    public Healthbar healthSystem;
    private AudioSource audio;

    Animator animator;

    public float attackRange = 1f;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthSystem = player.transform.GetComponent<Healthbar>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);// set hướng của navmessh đi theo người chơi

        if (Vector3.Distance(transform.position, player.transform.position) < attackRange && animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))// nếu khoảng cách giữa navmesh và người chơi nhỏ hơn tầm tấn công của zombie và zombie đang ở trạng thái Walk
        {
            animator.SetTrigger("Attack");// trigger animation tấn công của zombie 
            StartCoroutine(WaitForAttack(1.2f));// chạy quá trình tấn công của zombie

        }
    }

    public IEnumerator WaitForAttack(float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);
        // nếu trong thời gian zombie đang tấn công mà khoảng cách của người chơi và zombie vẫn nhỏ hhown tầm đánh của zombie thì người chơi sẽ nhận dame
        // ngược lại nếu trong khoảng thời gian này người chơi có thể ra khỏi tầm đánh của zombie thf sẽ không nhận dame ( lúc này sẽ tính người chơi né đc đòn tấn công của zombie)
        if (Vector3.Distance(transform.position, player.transform.position) < attackRange)// nếu trong thời gian zombie đang tấn công mà khoảng cách của người chơi và zombie vẫn nhỏ hhown tầm đánh của zombie thì người chơi sẽ nhận dame
        {
            healthSystem.TakeDamage(0.6f);// hàm này sẽ gọi liên tục khi attack kết thúc nên để dame bé đi tránh tình trạng bị player nhận quá nhiều dame
        }
    }
}
