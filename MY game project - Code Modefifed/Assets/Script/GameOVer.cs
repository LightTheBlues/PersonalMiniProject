using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOVer : MonoBehaviour
{
    public GameObject player;
    private PlayerCharacter playercharacter;
    public GameObject Credit;

    private bool gameOverNow = false;

    // Start is called before the first frame update
    void Start()
    {
        playercharacter = player.GetComponent<PlayerCharacter>();
        Credit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playercharacter.die == true && !gameOverNow)
        {
            StartCoroutine(WaitAndTurnOn());
            gameOverNow = true;
        }
    }

    IEnumerator WaitAndTurnOn()
    {
        yield return new WaitForSeconds(3f);

        if (Credit != null)
        {
            Debug.Log("creditPage");
            Credit.SetActive(true);
            Time.timeScale = 0.8f;
        }
    }
}
