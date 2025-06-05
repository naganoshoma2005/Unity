
using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private Vector3 startPosition;
    public float maxDistance = 6.0f;

    void Start()
    {
        // 発射時の位置を保存
        startPosition = transform.position;
    }

    void Update()
    {
        // 現在位置と発射位置の距離を計算
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);

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
            // エネミーの色を紫に変更
            SpriteRenderer enemySprite = other.gameObject.GetComponent<SpriteRenderer>();
            if (enemySprite != null)
            {
                StartCoroutine(ChangeColorTemporarily(enemySprite, new Color(0.5f, 0, 0.5f), 2f)); // 2秒に変更
            }

            // 発射体を破壊
            Destroy(gameObject);
        }
    }

    IEnumerator ChangeColorTemporarily(SpriteRenderer spriteRenderer, Color newColor, float duration)
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = newColor;
        yield return new WaitForSeconds(duration);

        // Check if the spriteRenderer is still valid before setting the color back
        if (spriteRenderer != null)
        {
            Debug.Log("色が元に戻る");
            spriteRenderer.color = originalColor;
        }
    }
}
