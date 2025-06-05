using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement; // PlayerMovement スクリプトへの参照

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>(); // PlayerMovement スクリプトを取得
    }

    void Update()
    {
        // プレイヤーが移動しているかどうかをチェック
        if (playerMovement.IsMoving()) // IsMoving メソッドが移動しているかを返す
        {
            animator.SetBool("Step", true); // ステップアニメーションを有効にする
        }
        else
        {
            animator.SetBool("Step", false); // ステップアニメーションを無効にする
        }
    }
}
