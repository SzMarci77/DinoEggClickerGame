using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text countText;
    [SerializeField] TMP_Text incomeText;
    [SerializeField] StoreUpgrade[] storeUpgrades;
    [SerializeField] int updatesPerSecond = 5;

    [HideInInspector] public float count = 0;
    float nextTimeCheck = 1;
    float lastIncomeValue = 0;

    private void Start()
    {
        UpdateUI();
    }


    private void Update()
    {
        if (nextTimeCheck < Time.timeSinceLevelLoad)
        {
            IdleCalculate();
            nextTimeCheck = Time.timeSinceLevelLoad + (1f / updatesPerSecond);
        }
    }
    public void IdleCalculate()
    {
        float sum = 0;
        foreach (var storeUpgrade in storeUpgrades)
        {
            sum += storeUpgrade.CalculateIncomePerSecond();
            storeUpgrade.UpdateUI();
        }
        lastIncomeValue = sum;
        count += sum / updatesPerSecond;
        UpdateUI();
    }

    public void ClickAction()
    {
        count++;
        count += lastIncomeValue * 0.02f;
        UpdateUI();
    }

    public bool PurchaseAction(int cost)
    {
        if(count >= cost)
        {
            count -= cost;
            UpdateUI();
            return true;
        }
        return false;
    }
    void UpdateUI(){
        countText.text = Mathf.RoundToInt(count).ToString();
        incomeText.text = lastIncomeValue.ToString("F1") + "/s";
    }

    /*
    string FormatNumber(float number)
    {
        if (number < 1000)
            return Mathf.RoundToInt(number).ToString();

        string[] suffixes = { "", "K", "M", "B", "T", "Q" };
        int suffixIndex = 0;
        double tempNumber = number;

        while (tempNumber >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            tempNumber /= 1000.0;
            suffixIndex++;
        }

        return tempNumber.ToString("F1") + suffixes[suffixIndex];
    }
    */
}
