using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    public float AttackDistance = 3;
    public float FireRate = 2.0f;  // Attacks every 2 seconds
    public LayerMask playerLayer;  // Set this in the Unity Editor

    private Transform player;
    private Animator animator;
    private float nextFireTime;

    public int Worth = 1;

    private void Start()
    {
        player = GameObject.FindWithTag("PlayerTag").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= AttackDistance)
        {
            if (Time.time > nextFireTime)
            {
                Ray ray = new Ray(transform.position, directionToPlayer.normalized);
                RaycastHit hit;

                
                if (Physics.SphereCast(ray, .75f, out hit, AttackDistance, playerLayer))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    Debug.Log("hit");
                    if (hitObject.GetComponent<PlayerCharacter>())
                    {
                        Debug.Log("Skeleton Attack");
                        PerformAttack();
                    }
                }

                nextFireTime = Time.time + FireRate;
                //Debug.Log(nextFireTime);
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.EnemyKilled(Worth);
    }

    private void PerformAttack()
    {
        EnemyData EnemyData = GetComponent<EnemyData>();
        animator.SetTrigger("Attack1");

        PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();

        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        if (playerCharacter != null && EnemyData._alive && distanceToPlayer <= AttackDistance)
        {
            animator.SetTrigger("Attack1");
            playerCharacter.TakeDamage(EnemyData.Damage);
        }
    }
}
