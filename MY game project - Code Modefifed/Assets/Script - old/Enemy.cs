using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MaxHealth = 1;
    public float Speed = 1;
    public float AttackDistance = 10;
    public float AttackSpeed = 1;
    public int Damage = 1;

    public float OrginalMaxHealth = 1;
    public int OrginalDamage = 1;

    private Transform player; // Reference to the player's transform

    private float CurrentHealth; //new
    public float fireRate = 2.0f; // Fire rate in seconds
    private float nextFireTime;

    Animator animator;
    public bool _alive;


    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        OrginalMaxHealth = MaxHealth;
        OrginalDamage = Damage;

        player = GameObject.FindWithTag("PlayerTag").transform; 

        animator = GetComponent<Animator>();
        _alive = true;

    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction from the enemy to the player
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Check if the player is within the attack range
        if (distanceToPlayer <= AttackDistance && _alive == true)
        {
            // Check if it's time to fire a fireball
            if (Time.time > nextFireTime)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.SphereCast(ray, 1.75f, out hit) && hit.distance <= AttackDistance)
                {
                    GameObject hitObject = hit.transform.gameObject;
                    if (hitObject.GetComponent<PlayerCharacter>())
                    {
                        Debug.Log("attack");
                        PerformAttack();
                    }
                }

                // Update the next fire time
                nextFireTime = Time.time + 2 / fireRate;
            }
        }
    }

    public void EnemyTakeDamge(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log("CurrentHealth" + CurrentHealth);
        if (CurrentHealth <= 0)
        {
            EnemeyDie();
        }
    }

    private void EnemeyDie()
    {
        _alive = false;
        animator.SetTrigger("Die");
        Destroy(this.gameObject, 3f);
    }

    void PerformAttack()
    {
        //only work for bear
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Bear_Attack1"))
        {
            animator.SetTrigger("Attack1");
        }
        
        // Deal damage to the player
        PlayerCharacter playerCharacter = player.GetComponent<PlayerCharacter>();
        if (playerCharacter != null)
        {
            playerCharacter.TakeDamage(Damage);
        }


    }

}

