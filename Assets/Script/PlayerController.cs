using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float fllowDistance = 1f;
    [SerializeField] private float DetectionRadius = 20f;
    [SerializeField] private LayerMask EnemyLayerMask;
    private Transform enemyTransform;
    private NavMeshAgent m_NavMeshAgent;
    private Animator animator;
    private Enity player;
    private bool isAttacking = false;

    private int attackLeft = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        player = GetComponent<Enity>();
    }

    void Update()
    {
        if (player.currenthealth <= 0)
        {
            m_NavMeshAgent.isStopped = true;
            Destroy(gameObject, 2f);
            return;
        }
        if (player.currenthealth < player.starthealth)
        {
            player.HealthRegen();
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, DetectionRadius, EnemyLayerMask);
        float minDistance = Mathf.Infinity;
        foreach (Collider hit in hits)
        {
            float dist = Vector3.Distance(transform.position, hit.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                enemyTransform = hit.transform;
            }
        }

        Debug.Log("Enemy: " + enemyTransform.name);
        if (enemyTransform != null)
        {
            Debug.Log(hits);
            Vector3 direction = (enemyTransform.position - transform.position).normalized;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
            if (Vector3.Distance(transform.position, enemyTransform.position) > fllowDistance)
            {
                m_NavMeshAgent.isStopped = false;
                animator.SetFloat("Speed", m_NavMeshAgent.velocity.magnitude);
                m_NavMeshAgent.SetDestination(enemyTransform.position);
            }
            else
            {
                animator.SetFloat("Speed", 0f);
                m_NavMeshAgent.isStopped = true;
            }
        }
    }

    public void EnableAttack()
    {
        animator.SetTrigger("Attack");
        animator.SetInteger("Attack Index", UnityEngine.Random.Range(0, 2));
    }
    public void EnableDoge()
    {
        transform.Translate(Vector3.back * 1.5f);
    }
    public void CheckHit()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, 1f, EnemyLayerMask);

        foreach (Collider enemy in enemies)
        {
            enemy.GetComponent<Enity>().TakeDame(player.damage);
        }
    }
}
