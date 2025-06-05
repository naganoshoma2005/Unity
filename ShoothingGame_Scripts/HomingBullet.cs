using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 20f; // 弾丸の速度
    public float lifetime = 5f; // 弾丸が消えるまでの時間（秒）
    public float offsetHeight = 1.5f; // ターゲット位置を調整する高さのオフセット

    
    private Transform target;
    private float lifetimeTimer; // 経過時間をカウントするためのタイマー

    // ターゲットを設定するメソッド
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Start()
    {
        lifetimeTimer = 0f;

        // Rigidbodyの重力を無効化
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }

        // ターゲットが存在する場合、初期方向を設定
        if (target != null)
        {
            Vector3 directionToTarget = CalculateTargetDirection();
            transform.forward = directionToTarget;
        }
    }

    void Update()
    {
        lifetimeTimer += Time.deltaTime;

        // 寿命が尽きるか、ターゲットが存在しない場合は弾丸を削除
        if (lifetimeTimer >= lifetime || target == null)
        {
            Destroy(gameObject);
            return;
        }

        // ターゲットの方向に進行方向を更新
        Vector3 directionToTarget = CalculateTargetDirection();
        transform.forward = directionToTarget;

        // ターゲットに向かって前進
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    // ターゲット位置にオフセットを加えた方向を計算するメソッド
    Vector3 CalculateTargetDirection()
    {
        // ターゲットの位置にオフセットを加える
        Vector3 targetPosition = target.position + Vector3.up * offsetHeight;
        return (targetPosition - transform.position).normalized;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // 衝突した敵のHPを減らす
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(10); // 例: ダメージを5与える
            }

            // 弾丸を削除
            Destroy(gameObject);
        }
    }
}



