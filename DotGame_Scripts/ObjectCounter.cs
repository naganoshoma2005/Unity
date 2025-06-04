using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCounter : MonoBehaviour
{
    // カウントするタグの名前
    public string targetTag = "Enemy";

    // オブジェクトが破壊された回数を記録するカウンター
    private int destroyCount = 0;

    // カウントを表示するためのUI（必要であれば使用）
    public UnityEngine.UI.Text countText;

    // カウントの取得用プロパティ
    public int DestroyCount
    {
        get { return destroyCount; }
    }

    // ゲーム開始時に呼ばれる
    private void Start()
    {
        // ゲーム開始時にUIを更新（初期カウントを表示）
        UpdateUI();
    }

    // オブジェクトが破壊される時に呼ばれる
    private void OnDestroy()
    {
        // もしこのオブジェクトが指定されたタグを持っていればカウントを増やす
        if (gameObject.CompareTag(targetTag))
        {
            destroyCount++;
            UpdateUI();
        }
    }

    // UIのテキストを更新する関数
    private void UpdateUI()
    {
        if (countText != null)
        {
            countText.text = "Destroyed: " + destroyCount.ToString();
        }
    }
}
