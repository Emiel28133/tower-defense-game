using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    [SerializeField] private int money = 50; // Initial money
    [SerializeField] private Text moneyText; // Assign your UI Text in the Inspector
    [SerializeField] private Button spawnButton; // Assign your button in the Inspector

    //public int Money => money; // Public property to get the current money amount

    public int Money
    {
        get { return money; }
    }

    void Start()
    {
        UpdateMoneyUI();
        //spawnButton.onClick.AddListener(SpendMoney);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    public void SpendMoney()
    {
        if (money >= 10)
        {
            Debug.Log("Spending money");

            money -= 10;
            UpdateMoneyUI();
        }
    }

    void UpdateMoneyUI()
    {
        moneyText.text = "Money: " + money;
        //spawnButton.interactable = money >= 10;
    }
}
