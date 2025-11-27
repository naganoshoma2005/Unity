using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour
{
    [Header("設定")]
    [Tooltip("生成したい3種類のプレハブをここにドラッグ＆ドロップ")]
    public GameObject[] spawnObjects; 

    [Tooltip("スポーン範囲の基準となるPlane（床）")]
    public GameObject targetPlane; 

    [Tooltip("生成する間隔（秒）")]
    public float spawnInterval = 2.0f; 

    [Tooltip("床からどれくらい高い位置に出すか")]
    public float heightOffset = 1.0f;

    // ★追加: 生成したオブジェクトが消えるまでの時間（秒）
    [Tooltip("生成されたオブジェクトが自動で消滅するまでの時間（秒）。0以下の場合は消滅しません。")]
    public float destructionTime = 5.0f;

    // ゲーム開始時に自動でループを開始
    void Start()
    {
        if (targetPlane == null)
        {
            Debug.LogError("Target Planeが設定されていません！");
            return;
        }

        StartCoroutine(SpawnLoop());
    }

    // 一定時間ごとに処理を繰り返すコルーチン
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // 設定した時間だけ待つ
            yield return new WaitForSeconds(spawnInterval);

            // スポーン処理を実行
            SpawnRandomObject();
        }
    }

    void SpawnRandomObject()
    {
        // 1. 生成するオブジェクトをランダムに1つ選ぶ
        int randomIndex = Random.Range(0, spawnObjects.Length);
        GameObject objectToSpawn = spawnObjects[randomIndex];

        // 2. Planeの範囲内でランダムな位置を計算する
        Collider planeCollider = targetPlane.GetComponent<Collider>();
        
        float minX = planeCollider.bounds.min.x;
        float maxX = planeCollider.bounds.max.x;
        float minZ = planeCollider.bounds.min.z;
        float maxZ = planeCollider.bounds.max.z;

        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        
        float spawnY = planeCollider.bounds.center.y + heightOffset;

        Vector3 spawnPosition = new Vector3(randomX, spawnY, randomZ);

        // 3. オブジェクトを生成（インスタンス化）
        GameObject spawnedInstance = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // ★追加: 破壊時間を設定
        if (destructionTime > 0f)
        {
            // Destroy(オブジェクト, 待機時間) で指定時間後にオブジェクトを削除
            Destroy(spawnedInstance, destructionTime);
        }
    }
}