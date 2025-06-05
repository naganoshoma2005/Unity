
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed = 4f; // 移動速度
    private bool isMoving = true; // 移動フラグ

    void Update()
    {
        // isMovingがtrueの場合にのみ移動
        if (isMoving)
        {
            // 左方向に移動
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    void OnMouseDown()
    {
        // isMovingを切り替える
        isMoving = false;
    }
}
