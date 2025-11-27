
using UnityEngine;

public class TransformItem : MonoBehaviour
{
    [Header("有効化する魚モデル")]
    public GameObject fishModelToActivate; // ★シーンに配置済みの「非表示の」魚モデルを設定

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTransformation player = other.GetComponent<PlayerTransformation>();
            if (player != null)
            {
                // どのモデルを有効化するかを渡す
                player.StartTransformation(fishModelToActivate);
            }
            Destroy(gameObject);
        }
    }
}
