using UnityEngine;

public class EnemyMoveAnimator : MonoBehaviour
{
    // Animatorコンポーネント
    private Animator animator;

    // Rigidbodyコンポーネント（3D用）
    private Rigidbody rb;

    // 初期化処理
    void Start()
    {
        // Animatorコンポーネントを取得
        animator = GetComponent<Animator>();

        // Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
    }

    // 毎フレーム実行される処理
    [System.Obsolete]
    void Update()
    {
        // Rigidbodyの速度を取得
        float speed = rb.velocity.magnitude;

        // アニメーションの切り替え
        if (speed > 0.1f)
        {
            // 歩行アニメーションを再生
            animator.SetBool("IsWalking", true);
        }
        else
        {
            // 待機アニメーションを再生
            animator.SetBool("IsWalking", false);
        }
    }
}
