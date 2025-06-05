using UnityEngine;

public class PlayerMapPin : MonoBehaviour
{
    public GameObject pinPrefab; // 頭上に生成するオブジェクトのプレハブ
    private GameObject pinInstance; // 生成されたオブジェクト
    public float heightOffset = 2.0f; // 頭上の高さ調整

    void Start()
    {
        if (pinPrefab != null)
        {
            // 初期回転をプレイヤーの回転 + X軸90度に設定
            Quaternion initialRotation = transform.rotation * Quaternion.Euler(90, 0, 0);
            pinInstance = Instantiate(pinPrefab, transform.position + Vector3.up * heightOffset, initialRotation);
        }
    }

    void Update()
    {
        if (pinInstance != null)
        {
            // プレイヤーの頭上に追従
            pinInstance.transform.position = transform.position + Vector3.up * heightOffset;

            // プレイヤーの回転に追従（X軸90度の回転を維持）
            pinInstance.transform.rotation = transform.rotation * Quaternion.Euler(90, 0, 0);
        }
    }

    // プレイヤーが削除されたら、ピンも一緒に削除
    void OnDestroy()
    {
        if (pinInstance != null)
        {
            Destroy(pinInstance);
        }
    }
}
