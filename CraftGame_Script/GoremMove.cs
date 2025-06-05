using UnityEngine;

public class GoremMove : MonoBehaviour
{
    private Rigidbody rb;
    public float movespeed = 7f;
    public float rotatespeed = 6f;
    public Transform playerTransform;
    public float playerdistance;
    private Animator animator;
    public GameObject markerPrefab; // 頭上に表示するマークのプレハブ
    private GameObject markerInstance; // 実際に生成されたマーク


    public float attackCooldown = 2f; // 攻撃のクールタイム（秒）
    private float nextAttackTime = 0f; // 次に攻撃できる時間

    public float minimumDistance = 5f; // 最小距離（これ以下には近づかない）
    public float attackRange = 5f;    // 攻撃の範囲
    public int attackDamage = 10;     // 攻撃のダメージ量

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // マーカーのインスタンスを非アクティブな状態で生成
        if (markerPrefab != null)
        {
            markerInstance = Instantiate(markerPrefab, transform.position, Quaternion.identity);
            markerInstance.transform.SetParent(transform);

            // マーカーの位置を調整
            markerInstance.transform.localPosition = new Vector3(0, 3f, 0); // 高さを調整
            markerInstance.SetActive(false); // 初期状態では非表示
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
        
    }


    private void FixedUpdate()
    {
        // プレイヤーとの距離を計算
        playerdistance = Vector3.Distance(transform.position, playerTransform.position);
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // 攻撃処理: クールタイム中は攻撃しない
        if (playerdistance <= 15f && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown; // 次の攻撃可能時間を設定
            animator.SetTrigger("Attack");
            Debug.Log("攻撃アニメーションを再生しました。");
        }

        // 移動処理: 最小距離以上の場合のみ移動
        if (playerdistance > minimumDistance && playerdistance <= 30f)
        {
            animator.SetFloat("Speed", 1f, 0.1f, Time.fixedDeltaTime);

            // 回転処理
            Vector3 rotation = new Vector3(direction.x, 0, direction.z);
            if (rotation != Vector3.zero)
            {
                Quaternion targetRotate = Quaternion.LookRotation(rotation);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotate, rotatespeed * Time.fixedDeltaTime);
            }

            // 移動処理
            Vector3 moveposition = rb.position + direction * movespeed * Time.fixedDeltaTime;
            rb.MovePosition(moveposition);
        }
        else
        {
            animator.SetFloat("Speed", 0f, 0.1f, Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// 攻撃時に呼び出されるメソッド
    /// </summary>

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHP playerHP = collision.gameObject.GetComponent<PlayerHP>();
            if (playerHP != null)
            {
                playerHP.TakeDamage(attackDamage);
                Debug.Log($"プレイヤーに{attackDamage}のダメージを与えました！ 残りHP: {playerHP}");
            }
            else
            {
                Debug.Log("PlayerHPスクリプトが見つかりませんでした。");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 攻撃範囲をシーンビューで可視化
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
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
     
    }
}
