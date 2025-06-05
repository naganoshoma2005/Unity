using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] public int maxHelth = 100; // 最大HP
    private GameManager gameManager;
    public int currentHealth; // 現在のHP

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        currentHealth = maxHelth; // 初期のHPを最大値に設定
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHelth); // HPが0未満にならないように

        if (currentHealth <= 0)
        {
            OnDeath(); // HPが0になったら死亡処理
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHelth); // HPが最大値を超えないように
    }

    private void OnDeath()
    {
        gameManager.OnPlayerDestroy();
        // 死亡時の処理（例えばゲームオーバー画面の表示など）
        Debug.Log("Player has died.");
        gameObject.SetActive(false); // プレイヤーを非表示にする
        // ここでゲームオーバー画面を表示するなどの処理を追加できます
    }

    public int GetHealth()
    {
        return currentHealth; // 現在のHPを返す
    }

    internal void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }
 
}
