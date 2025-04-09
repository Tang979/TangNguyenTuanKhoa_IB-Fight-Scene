using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float fllowDistance = 1f;
    [SerializeField] private float DetectionRadius = 20f;
    [SerializeField] private LayerMask PlayerMask;
    [SerializeField] private float cooldownAttack = 2f;
    private float lastAttackTime = 0f;
    private Transform PlayerTransform;
    private NavMeshAgent m_NavMeshAgent;
    private Animator animator;
    private Enity enemy;
    void Start()
    {
        //emerald = GetComponent<EmeraldSystem>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enity>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (enemy.currenthealth <= 0)
        {
            m_NavMeshAgent.isStopped = true;
            Destroy(gameObject, 2f);
            return;
        }
        Collider[] CurrentlyDetectedTargets = Physics.OverlapSphere(transform.position, DetectionRadius, PlayerMask);

        foreach (Collider C in CurrentlyDetectedTargets)
        {
            if (C.gameObject != this.gameObject)
            {
                PlayerTransform = C.transform;
            }
        }
        if (PlayerTransform != null)
        {
            Vector3 direction = (PlayerTransform.position - transform.position).normalized;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }

            if (Vector3.Distance(transform.position, PlayerTransform.position) > fllowDistance)
            {
                m_NavMeshAgent.isStopped = false;
                animator.SetFloat("Speed", m_NavMeshAgent.velocity.magnitude);
                m_NavMeshAgent.SetDestination(PlayerTransform.position);
            }
            else
            {
                animator.SetFloat("Speed", 0f);
                m_NavMeshAgent.isStopped = true;
            }
            lastAttackTime += Time.deltaTime;
            if (lastAttackTime >= cooldownAttack)
            {
                animator.SetTrigger("Attack");
                animator.SetInteger("Attack Index", UnityEngine.Random.Range(0, 2));
                lastAttackTime = 0f;
            }
        }




    }

    public void CheckHit()
    {
        Collider[] players = Physics.OverlapSphere(transform.position, 1f, PlayerMask);

        foreach (Collider player in players)
        {
            Debug.Log("Gây sát thương tới: " + player.name);
            // Gọi hàm nhận sát thương
            player.GetComponent<Enity>().TakeDame(enemy.damage);
        }
    }

    public void EnableAttack()
    {
        animator.SetTrigger("Attack");
        animator.SetInteger("Attack Index", UnityEngine.Random.Range(0, 2));
    }
}
