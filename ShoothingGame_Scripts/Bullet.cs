using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5.0f;    // 弾が消えるまでの時間
    public int damage = 20;          // 弾が与えるダメージ量

    void Start()
    {
        Destroy(gameObject, lifetime);  // 一定時間後に自動的に削除
    }

    void OnTriggerEnter(Collider other)
    {
        // プレイヤーに当たった場合
        if (other.CompareTag("Player"))
        {
            // プレイヤーが持っているPlayerHealthスクリプトを取得してダメージを与える
            PlayerHP playerHealth = other.GetComponent<PlayerHP>();
            if (playerHealth != null)
            {
                Debug.Log("hit!");
                playerHealth.TakeDamage(damage); // ダメージを与える
            }

            Destroy(gameObject); // プレイヤーに当たったら弾を消す
        }

        // 地面に当たった場合
        if (other.CompareTag("Ground"))
        {
            Debug.Log("Bullet hit the ground!");
            Destroy(gameObject); // 地面に当たったら弾を消す
        }
    }
}
