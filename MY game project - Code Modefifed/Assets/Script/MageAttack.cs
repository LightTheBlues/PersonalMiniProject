using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttack : MonoBehaviour
{
    public float AttackDistance = 10;
    public float FireRate = 0.2f;
    public LayerMask playerLayer;
    public GameObject buffEffectPrefab;
    private Transform player;
    private Animator animator;
    private float nextFireTime;
    private List<GameObject> activeBuffEffects = new List<GameObject>();
    public int Worth = 3;
    private iceBall iceBallScript;
    private float nextActionTime = 0.0f;
    public float period = 5.0f;
    [SerializeField] private AudioSource buffSoundEffect;

    private void Start()
    {
        player = GameObject.FindWithTag("PlayerTag").transform;
        animator = GetComponent<Animator>();
        iceBallScript = GetComponent<iceBall>();
        nextActionTime = 0.0f;
        period = 5.0f;
    }

    private void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            BuffNearbyEnemies();
            buffSoundEffect.Play();
            // execute block of code here
        }

        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer < 3)
        {
            iceBallScript.range = false;
        }
        else
        {
            iceBallScript.range = true;
        }
        if (distanceToPlayer <= AttackDistance)
        {
            if (Time.time > nextFireTime)
            {
                Ray ray = new Ray(transform.position, directionToPlayer.normalized);
                RaycastHit hit;

                if (Physics.SphereCast(ray, .75f, out hit, AttackDistance, playerLayer) && hit.distance <= AttackDistance && hit.distance <= 3)
                {
                    GameObject hitObject = hit.transform.gameObject;
                    if (hitObject.GetComponent<PlayerCharacter>())
                    {
                        Debug.Log("Mage Attack");
                        PerformAttack();
                    }
                }

                nextFireTime = Time.time + FireRate;
            }
        }
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

    private void BuffNearbyEnemies()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, 5f); // Adjust the radius as needed

        foreach (Collider enemyCollider in hitEnemies)
        {
            // Check if the hit object has the EnemyData script
            EnemyData enemyScript = enemyCollider.GetComponent<EnemyData>();
            if (enemyScript != null && enemyScript._alive && enemyScript.Damage == enemyScript.DamagebeforeBuff)
            {
                // Buff the enemy
                enemyScript.Damage += 1;
            }
            if (enemyCollider.CompareTag("Enemies"))
            {
                GameObject buffEffect = Instantiate(buffEffectPrefab, enemyCollider.transform.position, Quaternion.identity);
                activeBuffEffects.Add(buffEffect);
                Destroy(buffEffect, 3f);
            }
        }
    }

    private void OnDestroy()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, 5f); // Adjust the radius as needed

        foreach (Collider enemyCollider in hitEnemies)
        {
            // Check if the hit object has the EnemyData script
            EnemyData enemyScript = enemyCollider.GetComponent<EnemyData>();
            if (enemyScript != null && enemyScript._alive && enemyScript.Damage != enemyScript.DamagebeforeBuff)
            {
                // Buff the enemy
                enemyScript.Damage = enemyScript.DamagebeforeBuff;
            }
        }
        foreach (GameObject buffEffect in activeBuffEffects)
        {
            Destroy(buffEffect);
        }
        GameManager.Instance.EnemyKilled(Worth);
    }
}
