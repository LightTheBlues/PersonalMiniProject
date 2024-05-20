using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocAttack : MonoBehaviour
{
    public float AttackDistance = 10;
    public float FireRate = 0.2f;
    public float HealingRange = 6;
    public float HealingRate = 0.2f;
    public GameObject healingEffectPrefab;
    private Transform player;
    private Animator animator;
    private float nextFireTime;
    private float nextHealingTime;
    private EnemyData enemyComponent;
    private bool firstAttack = false;
    [SerializeField] private AudioSource healSoundEffect;
    public int Worth = 10;

    private void Start()
    {
        player = GameObject.FindWithTag("PlayerTag").transform;
        animator = GetComponent<Animator>();
        enemyComponent = this.GetComponent<EnemyData>();
    }

    private void Update()
    {
        if(enemyComponent.currentHealth != enemyComponent.MaxHealth && firstAttack == false)
        {
            animator.SetTrigger("hurt");
            firstAttack = true;
            
        }
        if(firstAttack == false && Time.time > nextHealingTime)
        {
            Debug.Log("heal Start");
            StartCoroutine(WaitForDelayAndHeal());
            nextHealingTime = Time.time + 1 / HealingRate;
        }
        
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer <= AttackDistance && firstAttack == true)
        {
            if (Time.time > nextFireTime)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.SphereCast(ray, 0.75f, out hit) && hit.distance <= AttackDistance && hit.distance <= 3)
                {
                    GameObject hitObject = hit.transform.gameObject;
                    if (hitObject.GetComponent<PlayerCharacter>())
                    {
                        Debug.Log("Doc Attack");
                        PerformAttack();
                    }
                }

                nextFireTime = Time.time + FireRate;
            }
        }
    }

    IEnumerator WaitForDelayAndHeal()
    {
        Debug.Log("heal Start delay");
        animator.SetTrigger("heal");
        // Wait for 10 seconds
        yield return new WaitForSeconds(1);

        HealNearbyEnemies();
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
                animator.SetTrigger("Attack1");
            playerCharacter.TakeDamage(EnemyData.Damage);
        }
    }

    private void HealNearbyEnemies()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, HealingRange);

        foreach (Collider enemyCollider in hitEnemies)
        {
            // Check if the hit object has the EnemyData script
            EnemyData enemyScript = enemyCollider.GetComponent<EnemyData>();
            if (enemyScript != null && enemyScript._alive)
            {
                // Heal the enemy
                int healingAmount = 20; // You can adjust this value based on your requirements

                if (enemyScript.currentHealth + healingAmount <= enemyScript.MaxHealth)
                {
                    enemyScript.currentHealth += healingAmount;
                }
                else if (enemyScript.currentHealth + 10 <= enemyScript.MaxHealth)
                {
                    enemyScript.currentHealth += 10;
                }

                // Instantiate a healing effect
                healSoundEffect.Play();
                GameObject healingEffect = Instantiate(healingEffectPrefab, enemyCollider.transform.position, Quaternion.identity);
                Destroy(healingEffect, 2.0f); // Adjust the duration as needed
            }
        }
    }

}
