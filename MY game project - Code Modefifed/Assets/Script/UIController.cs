using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private Text scoreLabel;
    [SerializeField] private SettingsPopup settingsPopup;
    [SerializeField] private GameObject instruction;
    private bool instructionPage = true;
    public Button startButton;

    private int _score;
	
	void Awake()
	{
		Messenger.AddListener(GameEvent.GameStart, GameStart);
        instruction.SetActive(true);

    }
	void OnDestroy()
	{
		Messenger.RemoveListener(GameEvent.GameStart, GameStart);
	}
    // Start is called before the first frame update
    void Start()
    {
        settingsPopup.Close();
        startButton.onClick.AddListener(HideInstructions);
    }

    void Update()
    {
        if(instructionPage == true)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            OnOpenSettings();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
        {
            settingsPopup.Close();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    public void GameStart()
    {
		_score += 1;
        scoreLabel.text = _score.ToString();
    }

    public void OnOpenSettings()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        settingsPopup.Open();
    }

    void HideInstructions()
    {
        if (instruction != null)
        {
            instruction.SetActive(false);
            instructionPage = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }
}
