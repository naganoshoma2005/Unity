using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public float damage = 10f; // プレイヤーに与えるダメージ
    public float lifetime = 5f; // ブロックが消えるまでの寿命
    public AudioClip evadeSound; // 回避成功時のサウンド
    public float evadeWindow = 0.5f; // 回避ウィンドウ (秒)
    public float destroyDelay = 1f; // 衝突後にオブジェクトが消えるまでの遅延時間
    public float gravityScale = 1f; // 通常時の重力スケール
    public float slowedGravityScale = 0.1f; // 回避成功時の重力スケール（遅くする値）



    private AudioSource audioSource;
    private bool attackInitiated = true; // 攻撃が開始されたかどうかのフラグ
    private bool isEvadeSuccessful = false; // プレイヤーが回避に成功したか
    private Rigidbody rb; // リジッドボディの参照
    private HPBarController HPBar;

    void Start()
    {
        HPBar = FindAnyObjectByType<HPBarController>();

        // ブロックが一定時間後に自動的に削除されるようにする
        Destroy(gameObject, lifetime);

        // AudioSourceを初期化
        audioSource = GetComponent<AudioSource>();

        // Rigidbodyを取得z
        rb = GetComponent<Rigidbody>();

        // 重力を手動で調整する（重力スケールを適用）
        if (rb != null)
        {
            rb.useGravity = false; // デフォルトの重力を無効化
        }
    }

    void Update()
    {
        // 手動で重力の影響を加える（回避成功時には重力スケールを変更）
        if (rb != null)
        {
            if (isEvadeSuccessful)
            {
                rb.linearVelocity += Physics.gravity * slowedGravityScale * Time.deltaTime;
            }
            else
            {
                rb.linearVelocity += Physics.gravity * gravityScale * Time.deltaTime;
            }
        }

        // プレイヤーが回避ウィンドウ内に回避したかのチェック
        if (attackInitiated)
        {
            // プレイヤーをシーン内から取得
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                PlayerStatus playerStatus = player.GetComponent<PlayerStatus>();

                if (playerStatus != null && playerStatus.HasMovedWithin(evadeWindow))
                {
                    isEvadeSuccessful = true; // 回避成功
                }
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStatus playerStatus = collision.gameObject.GetComponent<PlayerStatus>();
            Animator playerAnimator = collision.gameObject.GetComponent<Animator>(); // プレイヤーのAnimatorを取得

            if (playerStatus != null && playerAnimator != null)
            {
                // 事前に回避成功が確認されている場合
                if (isEvadeSuccessful)
                {
                    Debug.Log("Evade Successful!");
                    // 回避成功時のアクション
                    playerAnimator.SetTrigger("EvadeSuccess"); // プレイヤーのAnimatorにアクセスしてトリガーを設定
                    audioSource.PlayOneShot(evadeSound); // 特別なSE
                  
                }
                else
                {
                    PlayerHP playerHP = collision.gameObject.GetComponent<PlayerHP>();//ddddd
                    if (playerHP != null)
                    {
                        playerHP.TakeDamage(10); // 例: 10ダメージを与える
                    }
                    // 回避していなかった場合はダメージを与える
                    playerStatus.TakeDamage(damage); // プレイヤーにダメージを与える

                }
            }

            // 衝突後に遅らせてブロックを削除
            Destroy(gameObject, destroyDelay); // 指定した遅延時間後にブロックを削除
        }
        else
        {

            Debug.Log("Evade Failed!");
            // 他のオブジェクトとの衝突でも遅らせてブロックを削除
            Destroy(gameObject, destroyDelay); // 指定した遅延時間後にブロックを削除
        }
    }


    public void InitiateAttack()
    {
        attackInitiated = true; // 攻撃が開始されたことを記録
    }
}