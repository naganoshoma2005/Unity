using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public string itemName = "木材";
    public int quantity = 1;
    public float despawnTime = 180f; // 3分 = 180秒

    private void Start()
    {
        // 3分後にオブジェクトを破壊
        Destroy(gameObject, despawnTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        // プレイヤーに接触したら拾得
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInventory inventory = other.gameObject.GetComponent<PlayerInventory>();

            if (inventory != null)
            {
                inventory.AddItem(itemName, quantity);
                Destroy(gameObject);
            }
        }
    }
}