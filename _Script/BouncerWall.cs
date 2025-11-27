using UnityEngine;
using System.Collections.Generic; // Dictionary を使用するために必要

public class BouncerWall : MonoBehaviour
{
    public float bounceForce = 10f; // 反発の強さ（調整可能）
    public float requiredContactTime = 2f; // 何秒接していると跳ね返るか

    private Dictionary<GameObject, float> contactingBalls = new Dictionary<GameObject, float>();
    private Dictionary<GameObject, Vector3> initialContactNormals = new Dictionary<GameObject, Vector3>(); // 衝突時の法線方向を保存

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (!contactingBalls.ContainsKey(collision.gameObject))
            {
                contactingBalls.Add(collision.gameObject, Time.time);
                // 衝突時の法線方向を保存
                initialContactNormals.Add(collision.gameObject, collision.contacts[0].normal);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (contactingBalls.ContainsKey(collision.gameObject))
            {
                contactingBalls.Remove(collision.gameObject);
                initialContactNormals.Remove(collision.gameObject);
            }
        }
    }

    private void Update()
    {
        // 反復処理中にDictionaryを変更しないように、一時的なリストを使用
        List<GameObject> ballsToBounce = new List<GameObject>();

        foreach (var entry in contactingBalls)
        {
            GameObject ball = entry.Key;
            float contactStartTime = entry.Value;

            if (Time.time - contactStartTime >= requiredContactTime)
            {
                Rigidbody rb = ball.GetComponent<Rigidbody>();
                if (rb != null && initialContactNormals.ContainsKey(ball))
                {
                    Vector3 bounceDirection = initialContactNormals[ball];
                    rb.AddForce(-bounceDirection * bounceForce, ForceMode.Impulse);
                    ballsToBounce.Add(ball); // 跳ね返ったボールをリストに追加
                }
            }
        }

        // 跳ね返ったボールを contactingBalls から削除
        foreach (GameObject ball in ballsToBounce)
        {
            contactingBalls.Remove(ball);
            initialContactNormals.Remove(ball);
        }
    }
}