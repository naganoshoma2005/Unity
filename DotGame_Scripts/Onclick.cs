/*
using UnityEngine;
using System.Collections;

public class Onclick : MonoBehaviour
{
    public string[] items; // ランダムでゲットできるアイテムの配列
    public float dropChance = 0.5f; // アイテムがドロップする確率（0.5は50%）
    public Animator animator; // アニメーターの参照
    public GameObject summonPrefab; // 召喚するオブジェクトのプレハブ

    // マウスボタンが押されたときに呼び出される
    void OnMouseDown()
    {
        StartCoroutine(Rat_Death());
    }

    IEnumerator Rat_Death()
    {
        // アニメーションを再生
        animator.SetTrigger("Die");

        // アニメーターが次のフレームで更新されるまで待機
        yield return null;

        // アニメーションの状態が変更されるまで待機
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Rat_Death") == false)
        {
            yield return null;
        }

        // アニメーションの長さを取得
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

        // アニメーションの再生が完了するまで待つ
        yield return new WaitForSeconds(animationLength);

        // 召喚するオブジェクトを生成
        GameObject summonedObject = Instantiate(summonPrefab, transform.position, transform.rotation);
        // 1秒後に召喚したオブジェクトを破壊する
        Destroy(summonedObject, 1f);

        // オブジェクトを破壊する
        Destroy(gameObject);
    }
}*/


using UnityEngine;
using System.Collections;

public class Onclick : MonoBehaviour
{
    public string[] items; // ランダムでゲットできるアイテムの配列
    public float dropChance = 0.5f; // アイテムがドロップする確率（0.5は50%）
    public Animator animator; // アニメーターの参照
    public GameObject summonPrefab; // 召喚するオブジェクトのプレハブ
    public int moneyToAdd = 100; // 追加するお金の量（適宜調整してください）

    // マウスボタンが押されたときに呼び出される
    void OnMouseDown()
    {
        StartCoroutine(Rat_Death());
    }

    IEnumerator Rat_Death()
    {
        // アニメーションを再生
        animator.SetTrigger("Die");

        // アニメーターが次のフレームで更新されるまで待機
        yield return null;

        // アニメーションの状態が変更されるまで待機
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Rat_Death") == false)
        {
            yield return null;
        }

        // アニメーションの長さを取得
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

        // アニメーションの再生が完了するまで待つ
        yield return new WaitForSeconds(animationLength);

        // PlayerStatsにお金を追加
        PlayerStats.Instance.AddMoney(moneyToAdd);

        // 召喚するオブジェクトを生成
        GameObject summonedObject = Instantiate(summonPrefab, transform.position, transform.rotation);
        // 1秒後に召喚したオブジェクトを破壊する
        Destroy(summonedObject, 1f);

        // オブジェクトを破壊する
        Destroy(gameObject);
    }
}