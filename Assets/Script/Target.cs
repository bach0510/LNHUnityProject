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
    public void TakeDamage(float amount)// hàm nhận dame của mục tiêu được gọi khi raycast của weapon bắn trúng mục tiêu  => sẽ call đến hàm này
    {
        health -= amount; // trừ máu bằng số lượng amount truyền vào
        if (health <= 0f)// nếu máu của mục tiêu hết hoặc < 0
        {
            
            navMeshAgent.isStopped = true;// dừng navMeshAgent
            animator.SetTrigger("Dead");// chạy animation dead cuare zombie
            StartCoroutine(Death());
        }
    }


    IEnumerator Death()
    {
        yield return new WaitForSeconds(3);// đợi 3s => đợi chạy hết animation dead
        Destroy(gameObject);// Destroy object 
        ScoreSystem.scoreValue += 1;// cộng điểm cho người chơi
    }
}
