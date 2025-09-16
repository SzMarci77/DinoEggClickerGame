using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUpgrade : MonoBehaviour
{
    [Header("Components")]
    public TMP_Text priceText;
    public TMP_Text incomeInfoText;
    public Button button;
    public Image dinoImage;
    public TMP_Text upgradeNameText;

    [Header("Generator values")]
    public string upgradeName;
    public int startPrice = 15;
    public float upgradePriceMultiplier = 1.15f;
    public float eggsPerUpgrade = 0.1f;

    [Header("Managers")]
    public GameManager gameManager;

    int level = 0;

    private void Start() 
    {
        UpdateUI();
    }

    public void ClickAction() { 
        int price = CalculatePrice();
        bool purchaseSuccess = gameManager.PurchaseAction(price);
        if(purchaseSuccess)
        {
            level++;
            // gameManager.IdleCalculate();
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        priceText.text = CalculatePrice().ToString();
        incomeInfoText.text = level.ToString() + " X " + eggsPerUpgrade + "/s";
        bool canAfford = gameManager.count >= CalculatePrice();
        button.interactable = canAfford;

        bool isPurchased = level > 0;
        dinoImage.color = isPurchased ? Color.white : Color.black;
        upgradeNameText.text = isPurchased ? upgradeName : "???";
    }



    int CalculatePrice(){
        int price = Mathf.RoundToInt(startPrice * Mathf.Pow(upgradePriceMultiplier, level));
        return price;
    }
    public float CalculateIncomePerSecond(){
        return eggsPerUpgrade * level;
    }
}
