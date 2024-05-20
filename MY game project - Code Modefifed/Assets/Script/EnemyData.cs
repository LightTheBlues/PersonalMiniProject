using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public int MaxHealth = 1;
    public float Speed = 1;
    public int Damage = 1;
    public int DamagebeforeBuff = 1;

    public int OrginalMaxHealth = 1;
    public int OrginalDamage = 1;

    public int currentHealth;

    public bool _alive;
    Animator animator;
    public HealthBar healthBar;
    [SerializeField] public GameObject coinPrefab;
    [SerializeField] private GameObject potionPrefab;

    private void Start()
     {
        DamagebeforeBuff = Damage;
         OrginalMaxHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        OrginalDamage = Damage;
         currentHealth = MaxHealth;
         animator = GetComponent<Animator>();
         _alive = true;
     }

    private void Update()
    {
        healthBar.SetMaxHealth(MaxHealth);
        healthBar.SetHealth(currentHealth);
    }
    
     public void TakeDamage(int damage)
     {
         currentHealth -= damage;
         Debug.Log("CurrentHealth: " + currentHealth);
        healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0 && _alive == true)
         {
             Die();
         }
     }
    
    private void Die()
    {
        _alive = false;
        animator.SetTrigger("Die");
        float randomValue = Random.value;
        if (randomValue < 0.4f)
        {
            
        }
        else if (randomValue < 0.95f)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(potionPrefab, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject, 3f);
    }
}
