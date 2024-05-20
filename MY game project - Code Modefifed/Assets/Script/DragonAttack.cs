using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    public float AttackDistance = 10;
    public float FireRate = 2.0f;


    private Transform player;
    private Animator animator;
    private float nextFireTime;

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
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.SphereCast(ray, 1.75f, out hit) && hit.distance <= AttackDistance)
                {
                    GameObject hitObject = hit.transform.gameObject;
                    if (hitObject.GetComponent<PlayerCharacter>())
                    {
                        Debug.Log("Attack");
                        PerformAttack();
                    }
                }

                nextFireTime = Time.time + 1 / FireRate;
            }
        }
    }

    private void PerformAttack()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Bear_Attack1"))
        {
            animator.SetTrigger("Attack1");
        }

        PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();
        EnemyData EnemyData = this.GetComponent<EnemyData>();
        if (playerCharacter != null)
        {
            playerCharacter.TakeDamage(EnemyData.Damage);
        }
    }
}
