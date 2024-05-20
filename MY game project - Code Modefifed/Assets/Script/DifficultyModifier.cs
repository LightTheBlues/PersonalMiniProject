using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DifficultyModifier : MonoBehaviour
{
    public Text DifficultLevel;
    private EnemySpawner enemySpawner;
    public GameObject Tigger;
    private InputField difficultyInputField;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = Tigger.GetComponent<EnemySpawner>();
        difficultyInputField = GetComponent<InputField>();

        difficultyInputField.contentType = InputField.ContentType.IntegerNumber;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDifficultLevelText();
    }
    private void UpdateDifficultLevelText()
    {
        if (DifficultLevel != null)
        {
            DifficultLevel.text = enemySpawner.currentDifficulty.ToString();
        }
        if (difficultyInputField != null)
        {
            if (int.TryParse(difficultyInputField.text, out int newDifficulty))
            {
                enemySpawner.currentDifficulty = newDifficulty;
            }
        }
    }
}
