using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // 最大HP
    public int currentHealth;   // 現在のHP
    private Animator animator;  // Animatorコンポーネントを参照する変数
    public GameObject explosionEffect; // 通常の爆発エフェクトのプレハブ
    public GameObject smallExplosionEffect; // 小さい爆発エフェクトのプレハブ
    public AudioClip smallExplosionSound; // 小さい爆発音の音声ファイル
    public AudioClip finalExplosionSound; // 最後の爆発音の音声ファイル
    public Transform[] smallExplosionPositions; // 小さい爆発を発生させる位置（配列）
    public float smallExplosionDelay = 0.5f; // 小さい爆発の時間差
    private bool hasExploded = false; // 爆発が一回だけ発生するフラグ
    private AudioSource audioSource; // AudioSourceコンポーネントを参照する変数

    public GameObject resultPanel; // 結果パネル（非表示状態で設定）

    void Start()
    {
        currentHealth = maxHealth; // 最初は最大HPに設定
        animator = GetComponent<Animator>(); // Animatorコンポーネントを取得
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSourceを追加

        // パネルを非表示にする（初期設定）
        if (resultPanel != null)
        {
            resultPanel.SetActive(false);
        }
    }

    // 弾が当たった時にHPを減らす
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // ダメージを受ける
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // HPを0〜最大値に制限

        if (currentHealth <= 0)
        {
            animator.SetBool("Fire", false);
            Die(); // HPが0になったら敵を倒す
        }
    }

    // 敵が倒れた時の処理
    void Die()
    {
        // 死亡アニメーションを再生
        if (animator != null)
        {
            animator.SetTrigger("Die"); // "Die"というトリガーが設定されている前提
        }

        // 小さな爆発エフェクトを指定した位置で時間差で再生
        if (!hasExploded && smallExplosionEffect != null && smallExplosionPositions.Length > 0)
        {
            StartCoroutine(PlaySmallExplosions());
        }

        // アニメーションの長さが終了した後にオブジェクトを削除
        StartCoroutine(DestroyAfterAnimation());
    }

    // 小さな爆発を時間差で再生
    private IEnumerator PlaySmallExplosions()
    {
        // 配列に格納されたすべての位置で小さい爆発を発生させる
        for (int i = 0; i < smallExplosionPositions.Length; i++)
        {
            Transform explosionPosition = smallExplosionPositions[i];
            GameObject smallExplosion = Instantiate(smallExplosionEffect, explosionPosition.position, Quaternion.identity);
            smallExplosion.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            PlaySound(smallExplosionSound);
            yield return new WaitForSeconds(smallExplosionDelay);
        }

        hasExploded = true;
    }

    // アニメーション終了後にオブジェクトを削除
    private IEnumerator DestroyAfterAnimation()
    {
        if (animator != null)
        {
            yield return new WaitForSeconds(2f);
        }

        // ここで通常の爆発エフェクトを再生
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            PlaySound(finalExplosionSound);
            yield return new WaitForSeconds(1f);
        }

        // パネルを表示
        if (resultPanel != null)
        {
            resultPanel.SetActive(true);
        }

        Destroy(gameObject); // オブジェクトを削除
    }

    // 指定した音声クリップを再生するメソッド
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
