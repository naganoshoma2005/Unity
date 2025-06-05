using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    private Vector3 movement; // プレイヤーの移動ベクトル

    void Update()
    {
        // 移動入力を取得
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal, 0, vertical); // 水平および垂直方向の移動を設定

        // プレイヤーの移動を実行
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    // プレイヤーが移動しているかどうかを確認するメソッド
    public bool IsMoving()
    {
        return movement.magnitude > 0; // 移動ベクトルの大きさが0より大きいかをチェック
    }
}
