using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pointTracker : MonoBehaviour
{
    public Text ScoreText;
    public GameObject gameManagerObject; 
    private GameManager gameManagers;
    public int totalScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManagers = gameManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        totalScore = gameManagers.totalPoints;
        UpdateScoreText(totalScore);
    }

    private void UpdateScoreText(int totalScore)
    {
        if (ScoreText != null)
        {
            ScoreText.text = "Score: " + totalScore.ToString();
        }
    }
}
