
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public GameObject markerPrefab; // 頭上に表示するマークのプレハブ
    private GameObject markerInstance; // 実際に生成されたマーク

    public Transform player; // プレイヤーのTransform
    public float chaseSpeed = 5f; // 追跡速度
    public float detectionRange = 10f; // プレイヤーを追跡し始める範囲
    public float stopDistance = 30f; // プレイヤーとの最低距離（これ以上近づかない）
    public float rotationSpeed = 5f; // 回転速度

    private bool isChasing = false; // プレイヤーを追跡中かどうか

    private void Start()
    {
        // マーカーのインスタンスを非アクティブな状態で生成
        if (markerPrefab != null)
        {
            markerInstance = Instantiate(markerPrefab, transform.position, Quaternion.identity);
            markerInstance.transform.SetParent(transform);

            // マーカーの位置を調整
            markerInstance.transform.localPosition = new Vector3(0, 15f, 0); // 高さを調整
            markerInstance.SetActive(false); // 初期状態では非表示
        }

        // プレイヤーを自動的に探す（プレイヤーが指定されていない場合）
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    /// <summary>
    /// ダメージを受けたときに呼び出されるメソッド
    /// </summary>
    public void OnDamaged()
    {
        if (markerInstance != null)
        {
            markerInstance.SetActive(true); // マークを表示
        }
        isChasing = true; // 追跡を開始
    }

    private void Update()
    {
        if (isChasing && player != null)
        {
            // プレイヤーとの距離を計算
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // プレイヤーが追跡範囲内かつ追跡停止距離より遠い場合のみ移動
            if (distanceToPlayer <= detectionRange && distanceToPlayer > stopDistance)
            {
                // プレイヤーに向かって移動
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * chaseSpeed * Time.deltaTime;

                // プレイヤーの方向に回転
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    /// <summary>
    /// マークを非表示にする（必要に応じて）
    /// </summary>
    public void HideMarker()
    {
        if (markerInstance != null)
        {
            markerInstance.SetActive(false);
        }
        isChasing = false;
    }
}
