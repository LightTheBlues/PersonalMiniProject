using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private CharacterController _charController;

    //// Health BAR and PLAYER HEALTH
    public int maxHealth = 50;
    public int currentHealth;
    public HealthBar healthBar;

    // Attack settings
    public Transform attackPoint;
    public float attackRange = 2.0f;
    public int attackDamage = 10;
    // Attack cooldown settings
    public float attackCooldown = 1.0f;
    private float nextAttackTime = 0.0f;
    FPSInput FPSInputComponent;
    [SerializeField] private AudioSource attackSoundEffect;
    Animator animator;

    private bool isDefending = false;
    private bool isAttacking = false;
    public GameObject blockEffect;
    public GameObject thirdPersonCameraPrefab;
    public bool die = false;



    // * Require for the fireball to work
    public static PlayerCharacter Instance { get; private set; }

    void Awake()
    {
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                // Handle the case where there's already an instance
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        _charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        FPSInputComponent = this.GetComponent<FPSInput>();
    }

    public void TakeDamage(int damage)
    {
        if(isDefending == true)
        {
           damage = Mathf.RoundToInt(damage / 2);

           
        }
        if(isDefending == false)
        {
            animator.SetTrigger("Hurt");
        }
        Vector3 effectPosition = transform.position + new Vector3(0, 3, 0); // Move up by 2 units on the Y-axis
        GameObject effectInstance = Instantiate(blockEffect, effectPosition, Quaternion.identity);
        effectInstance.transform.localScale = new Vector3(3, 3, 3);
        Destroy(effectInstance, 1.0f);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        Debug.Log("Health: " + currentHealth);

        if (currentHealth <= 0 && die == false)
        {
            Die();
            die = true;
        }
    }

    void Update()
    {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        animator.SetBool("PlayerDefensing", isDefending);
        // Attack on left-click (mouse button 0)
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime && isDefending == false)
        {
            isAttacking = true;
            Attack();
        }
        if (Input.GetMouseButtonDown(1) && isAttacking == false)
        {
            FPSInputComponent.speed = 3;
            // Start defending
            isDefending = true;
        }
        if(Input.GetMouseButtonUp(1))
        {
            isDefending = false;
            FPSInputComponent.speed = 6;
        }


    }

    void Attack()
    {
        attackSoundEffect.Play();
        float randomValue = Random.value;
        if (randomValue < 0.5f)
        {
            animator.SetTrigger("PlayerAttack");
        }
        else
        {
            animator.SetTrigger("PlayerAttack2");
        }

        nextAttackTime = Time.time + attackCooldown;

        // Use Physics.Raycast to check if there's an enemy in front of the player
        RaycastHit hit;
        if (Physics.Raycast(attackPoint.position, transform.forward, out hit, attackRange))
        {
            // Check if the hit object has the EnemyData script
            EnemyData enemyScript = hit.collider.GetComponent<EnemyData>();
            if (enemyScript != null)
            {
                // Deal damage to the enemy
                enemyScript.TakeDamage(attackDamage);
            }
            else if (hit.collider.CompareTag("box"))
            {
                // If the hit object has the tag "box," destroy it
                hit.collider.gameObject.SetActive(false);
            }
        }

        isAttacking = false;
    }

    public void LevelUp()
    {
        maxHealth += 10;
        currentHealth += 10;
        Debug.Log("Player leveled up! New health: " + currentHealth);
    }

    void Die()
    {
        InstantiateThirdPersonCamera();
        animator.SetTrigger("die");
        //0.5 work
        Time.timeScale = 0.5f;
    }

    void InstantiateThirdPersonCamera()
    {
        if (thirdPersonCameraPrefab != null)
        {
            Vector3 cameraPosition = transform.position + new Vector3(0.5f, 2f, 0);
            Vector3 directionToPlayer = transform.position - cameraPosition;
            Quaternion cameraRotation = Quaternion.LookRotation(directionToPlayer);


            GameObject thirdPersonCamera = Instantiate(thirdPersonCameraPrefab, cameraPosition, cameraRotation);

        }
    }
}
