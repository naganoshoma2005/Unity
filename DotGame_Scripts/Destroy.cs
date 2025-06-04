using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public string targetTag = "Target"; // 目標オブジェクトのタグ
    public PlayerStats playerStats;
    public GameObject summonPrefab; // 召喚するオブジェクトのプレハブ

    void OnTriggerEnter2D(Collider2D other)
    {
        
        // 衝突したオブジェクトのタグが目標タグと一致する場合
        if (other.CompareTag(targetTag))
        {
            playerStats.decreasecoin();
            // このオブジェクトを消去
            Destroy(gameObject);
            // 召喚するオブジェクトを生成
            GameObject summonedObject = Instantiate(summonPrefab, transform.position, transform.rotation);
            // 1秒後に召喚したオブジェクトを破壊する
            Destroy(summonedObject, 1f);
        }
    }
}
