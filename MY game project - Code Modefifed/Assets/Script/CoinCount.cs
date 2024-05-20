using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCount : MonoBehaviour
{
    public Text CoinText;
    public int coinCount = 0;
    void Update()
    {

        if (ItemManager.Instance != null)
        {
            coinCount = ItemManager.Instance.GetItemCount("Coin");
        }

        UpdateCoinText(coinCount);
    }

    private void UpdateCoinText(int coinCount)
    {
        if (CoinText != null)
        {
            CoinText.text =  coinCount.ToString();
        }
    }
}
