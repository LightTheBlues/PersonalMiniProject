using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    [SerializeField] string playerTag = "PlayerTag";
    [SerializeField] float stoppingDistance;

    NavMeshAgent _navMeshAgent;
    EnemyData enemyComponent;
    Animator animator;
    Transform playerTransform; // Store a reference to the player's Transform

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
        // Find the player GameObject by tag and get its Transform
        GameObject player = GameObject.FindWithTag(playerTag);
        if (player != null)
        {
            playerTransform = player.transform;

            float distance = Vector3.Distance(transform.position, playerTransform.position);
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
        else
        {
            Debug.LogWarning("Player not found.");
        }
    }

    private void SetDestination()
    {
        if (playerTransform != null)
        {
            Vector3 targetVector = playerTransform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }

    private void GoToTarget()
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(playerTransform.position);
    }

    private void StopEnemy()
    {
        _navMeshAgent.isStopped = true;
    }

    private void FacePlayer()
    {
        if (playerTransform != null)
        {
            // Get the direction to the player
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.y = 0f; // Keep the rotation only in the horizontal plane

            // Rotate towards the player using LookAt
            if (directionToPlayer != Vector3.zero)
            {
                // Use LookAt to make the object face the player
                transform.LookAt(playerTransform);
            }
        }
    }
}
