
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    public static int Money;
    private int previousMoney; // Variable to store the previous money amount
    public int usingMoney;
    public int startMoney = 2000; // 初期のお金を2000に設定
    public int incomePerSecond = 10; // 1秒ごとに増えるお金の量
    public Text moneyText;
    public Manager manager;
    public bool buy;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Money = startMoney;
        previousMoney = Money; // Initialize the previousMoney
        UpdateMoneyUI();
        InvokeRepeating("AddIncome", 1f, 1f); // 1秒後から毎秒AddIncomeを呼び出す
    }

    private void Update()
    {
        usingMoney = Money;
        if (manager.ObjectNumber == 0 && Money >= Game.Price.TrapMoney)
        {
            buy = true;
        }
        else if (manager.ObjectNumber == 1 && Money >= Game.Price.Trap1Money)
        {
            buy = true;
        }
        else if (manager.ObjectNumber == 2 && Money >= Game.Price.Trap2Money)
        {
            buy = true;
        }
        else if (manager.ObjectNumber == 3 && Money >= Game.Price.Trap3Money)
        {
            buy = true;
        }
        else
        {
            buy = false;
        }
    }

    void AddIncome()
    {
        Money += incomePerSecond;
        UpdateMoneyUI();
    }

    public void AddMoney(int amount)
    {
        Money += amount;
        UpdateMoneyUI();
    }

    public void SpendMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            UpdateMoneyUI();
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    public void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            int moneyChange = Money - previousMoney;
            moneyText.text = "$" + Money.ToString() + " (" + (moneyChange >= 0 ? "+" : "") + moneyChange.ToString() + ")";
            previousMoney = Money; // Update the previousMoney to the current Money
        }
    }

    public void Shopping()
    {
        if (manager.ObjectNumber == 0)
        {
            Money -= Game.Price.TrapMoney;
        }
        else if (manager.ObjectNumber == 1)
        {
            Money -= Game.Price.Trap1Money;
        }
        else if (manager.ObjectNumber == 2)
        {
            Money -= Game.Price.Trap2Money;
        }
        else if (manager.ObjectNumber == 3)
        {
            Money -= Game.Price.Trap3Money;
        }

        UpdateMoneyUI(); // Ensure UI is updated after shopping
    }

    public void decreasecoin()
    {
        if (Money >= 200)
        {
            Money -= 200;
        }
        else if (Money < 200)
        {
            Money = 0;
        }

        UpdateMoneyUI(); // Ensure UI is updated after decreasing coins
    }
}
