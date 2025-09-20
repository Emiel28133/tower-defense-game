using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    [SerializeField] private int money = 50; // Initial money
    [SerializeField] private Text moneyText; // Assign your UI Text in the Inspector
    [SerializeField] private Button spawnButton; // Assign your button in the Inspector

    public int Money
    {
        get { return money; }
    }

    void Start()
    {
        UpdateMoneyUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    public void SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateMoneyUI();
        }
    }

    void UpdateMoneyUI()
    {
        moneyText.text = "FoolsGold: " + money;
    }
}
