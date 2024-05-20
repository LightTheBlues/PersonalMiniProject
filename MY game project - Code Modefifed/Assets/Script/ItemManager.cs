using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    // Private dictionary to store collected items and their counts
    private Dictionary<string, int> collectedItems = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Public method to collect an item
    public void CollectItem(string itemName)
    {
        if (itemName == "EXP")
        {
            GameManager.Instance.EnemyKilled(10);
        }
        else
        {
            if (collectedItems.ContainsKey(itemName))
            {
                collectedItems[itemName]++;
            }
            else
            {
                collectedItems[itemName] = 1;
            }
        }
    }
    public void RemoveItem(string itemName)
    {
        if (collectedItems.ContainsKey(itemName))
        { 
            collectedItems[itemName]--; 
        }
    }

    public int GetItemCount(string itemName)
    {
        if (collectedItems.ContainsKey(itemName))
        {
            return collectedItems[itemName];
        }
        else
        {
            return 0;
        }
    }
}
