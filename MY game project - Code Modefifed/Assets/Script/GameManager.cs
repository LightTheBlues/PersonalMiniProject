using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ExperienceBar experienceBar;
    // Player experience
    public int ExpEarn;
    public int currentPlayerLevel = 1;
    public int pointUntilNextLevel = 5;
    public int multiplier;



    // The score player has earned
    public int totalPoints = 0;

    void Start()
    {
        Instance = this;
        experienceBar.NextLevel(pointUntilNextLevel);
        ExpEarn = 0;
        multiplier = 1;
        // Initialize the game state
        UpdateUI();
        // Hide the mouse cursor
    }

    void OnGameEnd()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Update()
    {
        experienceBar.SetExperience(ExpEarn);
        experienceBar.NextLevel(pointUntilNextLevel);
    }

    // Call this method when an enemy is killed
    public void EnemyKilled(int enemyDifficulty)
    {
        totalPoints += enemyDifficulty * multiplier;
        ExpEarn += enemyDifficulty * multiplier;
        while (ExpEarn >= pointUntilNextLevel)
        {
            LevelUp();
        }

        UpdateUI();
    }


    void LevelUp()
    {
        currentPlayerLevel++;
        ExpEarn = ExpEarn - pointUntilNextLevel; //no negative
        pointUntilNextLevel *= 2;
        PlayerCharacter player;
        //player
        if (GameObject.FindGameObjectWithTag("PlayerTag") != null)
        {
            player = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<PlayerCharacter>();
            if (player != null)
            {
                player.LevelUp();
            }
        }

        Debug.Log("Level Up! Current Level: " + currentPlayerLevel + ", Exps until Next Level: " + pointUntilNextLevel);
    }

    // Update the UI with the current score and level information
    void UpdateUI()
    {
        Debug.Log("Total Points: " + totalPoints) ;
    }

}
