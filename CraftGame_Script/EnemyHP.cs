
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 100; // 敵の最大HP
    private int currentHealth;  // 現在のHP
    public int experienceReward = 20; // 敵を倒した時の経験値

    public GameObject[] dropItems; // ドロップする素材のプレハブ
    public int minDropCount = 1; // 最小ドロップ数
    public int maxDropCount = 3; // 最大ドロップ数
    public float dropHeight = 2.0f; // ドロップアイテムの生成高さ

    [Header("Knockback Settings")]
    public float knockbackForce = 5f;
    public float verticalDisplacement = 2f;
    public float displacementDuration = 0.2f;
    public float destroyTime = 5f;

    private Rigidbody rb;
    private bool isKnockedBack = false;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.mass = 1f;
            rb.linearDamping = 0.5f;
            rb.angularDamping = 0.05f;
            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }

    public void TakeDamage(int damage, Transform attackerTransform = null)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} は {damage} ダメージを受けた！ 残りHP: {currentHealth}");

        if (attackerTransform != null)
        {
            ApplyKnockback(attackerTransform);
        }

        GoremMove goremMove = GetComponent<GoremMove>();
        if (goremMove != null)
        {
            goremMove.OnDamaged();
        }

        EnemyChase chaseScript = GetComponent<EnemyChase>();
        if (chaseScript != null)
        {
            chaseScript.OnDamaged();
        }

        if (currentHealth <= 0)
        {
            Die(attackerTransform);
        }
    }

    private void ApplyKnockback(Transform attackerTransform)
    {
        Vector3 knockbackDirection = (transform.position - attackerTransform.position).normalized;
        Vector3 knockbackForceVector = knockbackDirection * knockbackForce;
        knockbackForceVector.y = verticalDisplacement;
        rb.AddForce(knockbackForceVector, ForceMode.Impulse);
        isKnockedBack = true;
        Invoke("ResetKnockback", displacementDuration);
    }

    [System.Obsolete]
    private void ResetKnockback()
    {
        isKnockedBack = false;
        rb.velocity = Vector3.zero;
    }

    private void Die(Transform attackerTransform)
    {
        Debug.Log($"{gameObject.name} は倒された！");

        // **アイテムをドロップ**
        DropItems();

        if (animator != null)
        {
            animator.SetTrigger("Die");
            Destroy(gameObject, destroyTime);
        }
        else
        {
            Destroy(gameObject);
        }

        if (GetComponent<Collider>() != null)
            GetComponent<Collider>().enabled = false;

        if (rb != null)
            rb.isKinematic = true;

        if (attackerTransform != null)
        {
            PlayerStats playerStats = attackerTransform.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.GainExperience(experienceReward);
                Debug.Log($"プレイヤーが {experienceReward} 経験値を獲得！");
            }
        }
    }

    /// <summary>
    /// アイテムドロップ処理
    /// </summary>
    private void DropItems()
    {
        if (dropItems == null || dropItems.Length == 0)
        {
            Debug.LogWarning($"{gameObject.name} の dropItems が空です。アイテムをドロップできません。");
            return;
        }

        int dropCount = Random.Range(minDropCount, maxDropCount + 1);
        for (int i = 0; i < dropCount; i++)
        {
            GameObject droppedItem = dropItems[Random.Range(0, dropItems.Length)];

            // **Inspector から調整可能な dropHeight を適用**
            Vector3 dropPosition = transform.position + new Vector3(Random.Range(-1f, 1f), dropHeight, Random.Range(-1f, 1f));

            Instantiate(droppedItem, dropPosition, Quaternion.identity);
            Debug.Log($"アイテム {droppedItem.name} を {dropPosition} にドロップ！");
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
