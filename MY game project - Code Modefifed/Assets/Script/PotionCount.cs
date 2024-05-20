using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionCount : MonoBehaviour
{
    public Text PotionText;
    public Text directionText;
    public int Potioncount = 0;
    void Update()
    {

        if (ItemManager.Instance != null)
        {
            Potioncount = ItemManager.Instance.GetItemCount("Potion");
        }
        if(Potioncount == 0)
        {
            directionText.gameObject.SetActive(false);
        }else
        {
            directionText.gameObject.SetActive(true);
        }

        if (Potioncount != 0)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                GameObject player = GameObject.FindGameObjectWithTag("PlayerTag");

                if (player != null)
                {
                    // Access the PlayerHealth script and add 25 health
                    PlayerCharacter playerHealth = player.GetComponent<PlayerCharacter>();

                    if (playerHealth != null)
                    {
                        playerHealth.currentHealth+=25;
                        ItemManager.Instance.RemoveItem("Potion");
                        if (playerHealth.currentHealth > playerHealth.maxHealth)
                        {
                            playerHealth.currentHealth = playerHealth.maxHealth;
                        }
                    }
                }
            }
        }
        UpdatePotionText(Potioncount);
    }

    private void UpdatePotionText(int Potioncount)
    {
        if (PotionText != null)
        {
            PotionText.text = Potioncount.ToString();
        }
    }
}
