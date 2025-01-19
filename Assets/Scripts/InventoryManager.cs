using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int AiCredits;
    public float Money;

    public Text moneyText;

    private void Start()
    {
        // Initialize the UI display
        UpdateMoneyDisplay();
    }


    public bool CanBuy(float cost)
    {
        return Money >= cost;
    }

    public void Buy(float cost)
    {
        Money -= cost;
        UpdateMoneyDisplay();
    }

    public void AddMoney(float money)
    {
        Money += money;
        UpdateMoneyDisplay();
    }

    private void UpdateMoneyDisplay()
    {
        if (moneyText != null)
        {
            moneyText.text = "$" + Money.ToString(); // Show 2 decimal points
        }
    }
}
