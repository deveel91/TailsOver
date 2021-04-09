using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : Singleton<DataManager>
{
    public Text goldAmountText;
    public GameObject noAdsButton;

    private int goldAmount = 0;
    private bool noAds = false;

    public void AddGold(int amount){
        goldAmount += amount;
        goldAmountText.text = goldAmount.ToString();
    }

    public void RemoveAds(){
        noAds = true;
        noAdsButton.SetActive(false);
    }
}