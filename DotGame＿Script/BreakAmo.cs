using UnityEngine;
using System.Collections;

public class BreakAmo : MonoBehaviour
{
    private Vector3 firstPosition;
    public float maxDistance = 6.0f;

    void Start()
    {
        // 発射時の位置を保存
        firstPosition = transform.position;
    }

    void Update()
    {
        // 現在位置と発射位置の距離を計算
        float distanceTravelled = Vector3.Distance(firstPosition, transform.position);

        // 一定距離を超えたらオブジェクトを破壊
        if (distanceTravelled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // エネミーのタグを持つオブジェクトにぶつかったら色を変更してオブジェクトを破壊
        if (other.gameObject.CompareTag("Enemy"))
        {

            // 発射体を破壊
            Destroy(gameObject);
        }
    }
}