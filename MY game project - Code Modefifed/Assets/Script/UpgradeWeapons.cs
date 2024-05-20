using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWeapons : MonoBehaviour
{
    private List<GameObject> weapons = new List<GameObject>();
    private int currentWeaponIndex;
    private int currentWeaponCost;
    public GameObject UpgradeText;
    [SerializeField] private Text UpgradeTexts;
    
    [SerializeField] private Transform player;
    private bool _inArea = false;
    private int costIncrease;

    void Start()
    {
        
        currentWeaponIndex = 0;
        currentWeaponCost = 5;
        costIncrease = 1;
        DetectAndAddWeapons(player);
        ActivateCurrentWeapon();
        UpgradeText.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        UpgradeText.SetActive(false);
        _inArea = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("PlayerTag"))
        {
            _inArea = true;
            UpgradeText.SetActive(true);
        }
    }

    void Update()
    {
        if (_inArea == true && (ItemManager.Instance.GetItemCount("Coin") >= currentWeaponCost) && Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(ItemManager.Instance.GetItemCount("Coin"));
            Debug.Log("Upgrade");
            UpgradeToNextWeapon();
        }
        UpdateCostText(currentWeaponCost);
    }

    //find the weapon, add to the list
    private void DetectAndAddWeapons(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag("Weapon"))
            {
                child.gameObject.SetActive(false);
                weapons.Add(child.gameObject);

                Debug.Log("Detected weapon: " + child.gameObject.name);
            }

            DetectAndAddWeapons(child);
        }
    }

    //change from one index of the weapon to another
    private void UpgradeToNextWeapon()
    {
        for(int i = 0; i < currentWeaponCost; i++)
        {
            ItemManager.Instance.RemoveItem("Coin");
        }
        currentWeaponIndex++;
        if(currentWeaponCost < 40)
        {
         currentWeaponCost = currentWeaponCost + (costIncrease * 5);
         costIncrease++;
        }
        PlayerCharacter playerCharacterScript = player.GetComponent<PlayerCharacter>();
        playerCharacterScript.attackDamage += 5;
        if (currentWeaponIndex < weapons.Count)
        {
            ActivateCurrentWeapon();
        }

    }

    //change the weapon
    private void ActivateCurrentWeapon()
    {
        weapons[currentWeaponIndex].SetActive(true);

        Debug.Log("Activated weapon: " + weapons[currentWeaponIndex].name);
    }
    private void UpdateCostText(int currentWeaponCost)
    {
        if (UpgradeTexts != null)
        {
            UpgradeTexts.text = "Push 'G' for upgrade: " + currentWeaponCost.ToString() + " coins";
        }
    }
}
