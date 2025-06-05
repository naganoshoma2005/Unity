using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [Header("Player HP Settings")]
    public int maxHealth = 100; // 最大HP
    private int currentHealth;  // 現在のHP

    [Header("UI Settings")]
    public Slider healthBar;    // HPバーのスライダー

    [Header("Damage Feedback")]
    public GameObject damageEffect; // ダメージ時のエフェクト（例：赤いフラッシュ）
    public float damageEffectDuration = 0.2f; // ダメージエフェクトの表示時間

    private bool isDead = false; // 死亡フラグ

    [SerializeField] private float invisibleTimer;
    private bool isInvisible;

    void Start()
    {
        // 初期HPを設定
        currentHealth = maxHealth;

        // HPバーを初期化
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    private void Update()
    {
        if (isInvisible)
        {
            invisibleTimer -= Time.deltaTime;
            if (invisibleTimer <= 0)
            {
                isInvisible = false;
                invisibleTimer = 1.5f;
            }
        }
    }

    /// <summary>
    /// ダメージを受けたときの処理
    /// </summary>
    /// <param name="damage">受けるダメージ量</param>
    public void TakeDamage(int damage)
    {
        if (!isInvisible)
        {
            if (isDead) return; // 死亡後はダメージを受けない

            currentHealth -= damage; // HPを減少
            isInvisible = true;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // HPを0～最大値の範囲内に制限

            Debug.Log($"プレイヤーは {damage} ダメージを受けた！ 残りHP: {currentHealth}");

            // HPバーを更新
            if (healthBar != null)
            {
                healthBar.value = currentHealth;
            }

            // ダメージエフェクトを表示
            if (damageEffect != null)
            {
                StartCoroutine(ShowDamageEffect());
            }

            // HPが0以下なら死亡
            if (currentHealth <= 0)
            {
                Die();
            }
        }

    }

    /// <summary>
    /// 回復処理
    /// </summary>
    /// <param name="healAmount">回復量</param>
    public void Heal(int healAmount)
    {
        if (isDead) return; // 死亡後は回復できない

        currentHealth += healAmount; // HPを増加
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // HPを0～最大値の範囲内に制限

        Debug.Log($"プレイヤーは {healAmount} 回復した！ 現在のHP: {currentHealth}");

        // HPバーを更新
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    /// <summary>
    /// ダメージエフェクトを一時的に表示
    /// </summary>
    private System.Collections.IEnumerator ShowDamageEffect()
    {
        damageEffect.SetActive(true);
        yield return new WaitForSeconds(damageEffectDuration);
        damageEffect.SetActive(false);
    }

    /// <summary>
    /// 死亡処理
    /// </summary>
    private void Die()
    {
        isDead = true;
        Debug.Log("プレイヤーは死亡した！");
        // 死亡時の処理をここに追加（例：ゲームオーバー画面を表示）
    }
}
