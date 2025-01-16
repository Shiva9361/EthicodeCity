using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int Population;
    public float Money;

    public bool CanBuy(float cost)
    {
        return Money >= cost;
    }

    public void Buy(float cost)
    {
        Money -= cost;
    }
}
