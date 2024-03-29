﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public enum ItemType{
        Gold50,
        Gold100,
        NoAds
    }
    public ItemType itemType;
    public Text priceText;
    private string defaultText;

    void Start(){
        defaultText = priceText.text;
        StartCoroutine(LoadPriceRoutine());
    }

    public void ClickBuy(){
        switch(itemType){
            case ItemType.Gold50:
                IAPManager.Instance.Buy50Gold();
                break;
            case ItemType.Gold100:
                IAPManager.Instance.Buy100Gold();
                break;
            case ItemType.NoAds:
                IAPManager.Instance.BuyNoAds();
                break;
            default:
                break;
        }
    }

    private IEnumerator LoadPriceRoutine()
    {
        while(!IAPManager.Instance.IsInitialized())
            yield return null;
        
        string loadedPrice = "";

        switch(itemType){
            case ItemType.Gold50:
                loadedPrice = IAPManager.Instance.GetProductPriceFromStore(IAPManager.Instance.GOLD_50);
                break;
            case ItemType.Gold100:
                loadedPrice = IAPManager.Instance.GetProductPriceFromStore(IAPManager.Instance.GOLD_100);
                break;
            case ItemType.NoAds:
                loadedPrice = IAPManager.Instance.GetProductPriceFromStore(IAPManager.Instance.NO_ADS);
                break;
            default:
                break;
        }

        priceText.text = defaultText + " " + loadedPrice;
    }

}