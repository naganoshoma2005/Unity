using UnityEngine;

public class SimpleMove : MonoBehaviour
{
 public float speed = 3.0f;      // 動く速さ
    public float moveDistance = 5.0f; // 片道の移動距離

    private Vector3 startPosition;  // 最初にいた位置
    private int direction = 1;      // 移動方向 (1:右, -1:左)

    void Start()
    {
        // ゲーム開始時の位置を記憶しておく
        startPosition = transform.position;
    }

    void Update()
    {
        // 現在位置を計算
        // (現在のX座標) + (移動量 * 方向 * 時間)
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);

        // 移動範囲を超えたら反転する処理
        float currentX = transform.position.x;
        
        // スタート位置より「右」に行き過ぎたら
        if (currentX > startPosition.x + moveDistance)
        {
            direction = -1; // 左へ向かう
        }
        // スタート位置より「左」に行き過ぎたら
        else if (currentX < startPosition.x - moveDistance)
        {
            direction = 1; // 右へ向かう
        }
    }
}
