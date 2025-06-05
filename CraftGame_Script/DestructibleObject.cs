using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public string objectName = "木材"; // オブジェクトの名前
    public int maxDurability = 100; // 最大耐久度
    public int currentDurability; // 現在の耐久度
    public GameObject[] dropItems; // ドロップする素材のプレハブ
    public int minDropCount = 1; // 最小ドロップ数
    public int maxDropCount = 3; // 最大ドロップ数

    void Start()
    {
        currentDurability = maxDurability;
    }

    public void TakeDamage(int damageAmount)
    {
        currentDurability -= damageAmount;

        if (currentDurability <= 0)
        {
            DestroyObject();
        }
    }

    void DestroyObject()
    {
        // ドロップアイテムの生成
        int dropCount = Random.Range(minDropCount, maxDropCount + 1);
        for (int i = 0; i < dropCount; i++)
        {
            if (dropItems.Length > 0)
            {
                GameObject droppedItem = dropItems[Random.Range(0, dropItems.Length)];
                Vector3 dropPosition = transform.position + new Vector3(Random.Range(-1f, 1f), 1.2f, Random.Range(-1f, 1f)); // y座標を1f上げる
                Instantiate(droppedItem, dropPosition, Quaternion.identity);
            }
        }
        InvisibleObject();
        // オブジェクトの破壊
        gameObject.SetActive(false);
    }
    public void InvisibleObject()
    {
        // WorldManagerに通知する
        if (WorldManager.Instance != null) { WorldManager.Instance.ObjectDestroyed(this.gameObject); }
    }
}
