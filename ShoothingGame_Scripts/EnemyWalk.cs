using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;      // 移動速度
    public Transform target;          // ターゲット（プレイヤーなど）
    public float stopDistance = 0.5f; // ターゲットとの到達距離（この距離内に入ったら消える）

    void Update()
    {
        if (target != null)
        {
            // ターゲットに向かって移動する
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // ターゲットとの距離を計算
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // ターゲットに到達したら消える
            if (distanceToTarget <= stopDistance)
            {
                Destroy(gameObject); // 敵キャラを消去
            }
        }
    }
}
