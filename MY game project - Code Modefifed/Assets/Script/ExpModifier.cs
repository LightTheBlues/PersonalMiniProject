using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpModifier : MonoBehaviour
{
    private Slider slider;
    private int expMultiplier = 1;
    public GameObject Controller;
    private GameManager gameManager;
    public Text multiplierText;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = Controller.GetComponent<GameManager>();
        slider = GetComponent<Slider>();
        multiplierText.text = expMultiplier.ToString() + "x";
    }

    // Update is called once per frame
    void Update()
    {
        slider.onValueChanged.AddListener(OnExpMultiplierChanged);
    }

    void OnExpMultiplierChanged(float value)
    {
        expMultiplier = Mathf.RoundToInt(value);
        gameManager.multiplier = expMultiplier;
        multiplierText.text = expMultiplier.ToString() + "x";
    }
}
