using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    public float health = 100f;
    private float damage;

    Animator animator;
    public NavMeshAgent navMeshAgent;

    void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void TakeDamage(float amount)// hàm nhận dame của mục tiêu được gọi khi raycast của weapon bắn trúng mục tiêu  => sẽ call đến hàm này
    {
        damage = amount;
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
        if (damage <= 35f)ScoreSystem.scoreValue += 10;// cộng điểm cho người chơi
        else ScoreSystem.scoreValue += 30;// cộng điểm cho người chơi
    }
}
