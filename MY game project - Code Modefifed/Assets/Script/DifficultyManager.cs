using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private bool difficultyChangedToBlood = false;
    private bool difficultyChangedToNormal = false;
    private bool difficultyChangedToCausal = false;
    public int enemyHealthIncrease = 2;
    public int enemyAttackIncrease = 2;

    public void BloodDifficulty()
    {
        if (difficultyChangedToBlood == false)
        {
            // Find all game objects with the "Enemy" script
            EnemyData[] enemies = FindObjectsOfType<EnemyData>();

            // Iterate through all enemies and adjust their health
            foreach (EnemyData enemy in enemies)
            {
                enemy.MaxHealth = enemy.OrginalMaxHealth * enemyHealthIncrease;
                enemy.Damage = enemy.OrginalDamage * enemyAttackIncrease;
            }
            difficultyChangedToBlood = true;
            difficultyChangedToNormal = false;
            difficultyChangedToCausal = false;
        }

    }

    public void NormalDifficulty()
    {
        if (difficultyChangedToNormal == false)
        {
            // Find all game objects with the "Enemy" script
            EnemyData[] enemies = FindObjectsOfType<EnemyData>();

            // Iterate through all enemies and adjust their health
            foreach (EnemyData enemy in enemies)
            {
                enemy.MaxHealth = enemy.OrginalMaxHealth;
                enemy.Damage = enemy.OrginalDamage;
            }
            difficultyChangedToBlood = false;
            difficultyChangedToNormal = true;
            difficultyChangedToCausal = false;
        }
    }

    public void CasualDifficulty()
    {
        if (difficultyChangedToCausal == false)
        {
            // Find all game objects with the "Enemy" script
            EnemyData[] enemies = FindObjectsOfType<EnemyData>();

            // Iterate through all enemies and adjust their health
            foreach (EnemyData enemy in enemies)
            {
                enemy.MaxHealth = enemy.OrginalMaxHealth / enemyHealthIncrease;
                enemy.Damage = enemy.Damage  - 1;
            }
            difficultyChangedToBlood = false;
            difficultyChangedToNormal = false;
            difficultyChangedToCausal = true;
        }
    }
}
