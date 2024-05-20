using UnityEngine;
using UnityEngine.UI;
public class TiggerGame : MonoBehaviour
{
    private EnemySpawner Spawner;
    public GameObject Rock09;
    // [SerializeField] GameObject Rock09;
    private int countEnter = 0;
    public GameObject NextWaveText;
    public GameObject upgradeText;
    public noWaveStart checkWave;

    private void Start()
    {
        // Get the EnemySpawner script attached to the same GameObject
        Spawner = GetComponent<EnemySpawner>();
        countEnter = 0;
        NextWaveText.SetActive(false);
    }

    private void Update()
    {
        if (CheckAnyEnemyAlive() == false && countEnter != 0 && checkWave.inSpwan == false)
        {
            Rock09.SetActive(false);
            NextWaveText.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.N) && CheckAnyEnemyAlive() == false && countEnter != 0 && checkWave.inSpwan == false)
        {
            Rock09.SetActive(true);
            countEnter++;
            countEnter++;
            Spawner.StartSpawning();
        }
        if (CheckAnyEnemyAlive() == true)
        {
            NextWaveText.SetActive(false);
        }
        if (upgradeText.activeSelf)
        {
            NextWaveText.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the player
        countEnter++;
        //Debug.LogError(countEnter);
        if (other.CompareTag("PlayerTag") && countEnter % 2 == 1)
        {
            Spawner.StartSpawning();
            Debug.Log("Game Start");
            NextWaveText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerTag") && countEnter % 2 == 1)
        {
            Rock09.SetActive(true);
        }
    }

    private bool CheckAnyEnemyAlive()
    {
        EnemyData[] enemies = FindObjectsOfType<EnemyData>();

        foreach (EnemyData enemy in enemies)
        {
            if (enemy._alive)
            {
                return true;
            }
        }

        return false;
    }
}