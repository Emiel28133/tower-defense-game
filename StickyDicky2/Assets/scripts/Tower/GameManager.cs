using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float fireRateUpgrade = 2f; // Example fire rate upgrade value
    public float bulletSpeedUpgrade = 5f; // Example bullet speed upgrade value
    private MoneySystem moneySystem;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        moneySystem = FindObjectOfType<MoneySystem>();
    }

    public void BuyFireRateUpgrade()
    {
        if (moneySystem != null && moneySystem.Money >= 100)
        {
            moneySystem.SpendMoney(100);
            Tower.UpgradeFireRate(fireRateUpgrade);

            // Find all bullets and increase their speed
            Bullets[] bullets = FindObjectsOfType<Bullets>();
            foreach (Bullets bullet in bullets)
            {
                bullet.IncreaseSpeed(bulletSpeedUpgrade);
            }
        }
        else
        {
            Debug.Log("Not enough money to buy upgrade!");
        }
    }
}
