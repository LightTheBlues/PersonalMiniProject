using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAttack : MonoBehaviour
{
    public float AttackDistance = 10;
    public float FireRate = 2.0f;
    public LayerMask playerLayer;  // Set this in the Unity Editor


    private Transform player;
    private Animator animator;
    private float nextFireTime;

    public int  Worth = 10;

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
                    if (hitObject.GetComponent<PlayerCharacter>())
                    {
                        Debug.Log("Bear Attack");
                        PerformAttack();
                    }
                }

                nextFireTime = Time.time + FireRate;
            }
        }
    }
    private void OnDestroy()
    {
        GameManager.Instance.EnemyKilled(Worth);
    }

    private void PerformAttack()
    {

        EnemyData EnemyData = this.GetComponent<EnemyData>();
        PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();

        if (playerCharacter != null && EnemyData._alive == true)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Bear_Attack1"))
            {
                animator.SetTrigger("Attack1");
            }
            playerCharacter.TakeDamage(EnemyData.Damage);
        }
    }
}
