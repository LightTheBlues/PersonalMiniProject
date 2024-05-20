using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OldNPCMOVE : MonoBehaviour
{
    [SerializeField] Transform _destination;
    [SerializeField] float stoppingDistance;

    NavMeshAgent _navMeshAgent;
    EnemyData enemyComponent;
    Animator animator;

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        enemyComponent = this.GetComponent<EnemyData>();
        animator = GetComponent<Animator>();

        if (_navMeshAgent == null)
        {
            Debug.Log("The nav mesh agent component is not attached");
        }
        else
        {
            SetDestination();
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, _destination.transform.position);
        // Make the enemy face the player
        FacePlayer();
        if (enemyComponent._alive == false)
        {
            StopEnemy();
        }
        else if (distance < stoppingDistance)
        {
            animator.SetBool("Idle", true);
            StopEnemy();
        }
        else
        {
            animator.SetBool("Idle", false);
            GoToTarget();
        }
    }

    private void SetDestination()
    {
        if (_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }

    private void GoToTarget()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(_destination.transform.position);
    }

    private void StopEnemy()
    {
        _navMeshAgent.isStopped = true;
    }

    private void FacePlayer()
    {
        // Get the direction to the player
        Vector3 directionToPlayer = _destination.position - transform.position;
        directionToPlayer.y = 0f; // Keep the rotation only in the horizontal plane

        // Rotate towards the player using LookAt
        if (directionToPlayer != Vector3.zero)
        {
            // Use LookAt to make the object face the player
            transform.LookAt(_destination);
        }
    }
}