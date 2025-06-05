
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public Manager MGR;
    private Vector3 initialPosition;
    private bool isChasing = false;

    void Start()
    {
        // 初期位置を保存
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (MGR != null)
        {
            if (MGR.CatchItem == true)
            {
                isChasing = true;
                Debug.Log("URkawakami");
                Vector3 mouseScreenPosition = Input.mousePosition;
                // マウスのスクリーン座標をワールド座標に変換
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

                // Z軸の値を0に設定
                mouseWorldPosition.z = 0;
                // 目標オブジェクトへの方向を計算
                Vector3 direction = (mouseWorldPosition - transform.position).normalized;
                // 移動
                transform.position += direction * 30 * Time.deltaTime;
            }
            else if (isChasing)
            {
                // マウス追尾が終わった後、一度だけ初期位置に戻す
                transform.position = initialPosition;
                isChasing = false;
            }
        }
    }
}

