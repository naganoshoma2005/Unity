
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("落下させるオブジェクトのPrefab")]
    public GameObject fallingObjectPrefab;

    [Header("生成間隔(秒)")]
    public float spawnInterval = 3.0f;

    [Header("生成エリアの中心")]
    public Vector3 spawnAreaCenter;

    [Header("生成エリアのサイズ(XZ平面)")]
    public Vector2 spawnAreaSize;
    [Header("ブロックの消滅までの時間")]
    public float despawnTime = 8f;

    ///<summary>
    /// オブジェクトの生成を開始します。
    ///</summary>
    public void StartSpawning()
    {
        //指定した間隔でSpawnObjectメソッドを繰り返し呼び出します。
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    ///<summary>
    /// オブジェクトの生成を停止します。
    /// </summary>
    public void StopSpawning()
    {
        //繰り返し実行されているメソッドを停止します。
        CancelInvoke(nameof(SpawnObject));
    }
    ///<summary>
    /// オブジェクトを一つ生成します。
    /// </summary>

    private void SpawnObject()
    {
        //Prefabが設定されていなければ何もしません。
        if (fallingObjectPrefab == null)
        {
            Debug.LogWarning("Falling Object Prefabが設定されていません");
            return;
        }

        //生成エリア内のランダムなX,Y座標を計算します。
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomZ = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
        //中心座標にランダムな値を加えて生成位置を決定します。
        Vector3 spawnPosition = new Vector3(
            spawnAreaCenter.x + randomX,
            spawnAreaCenter.y,//Y座標(高さ)は中心点のものをそのまま使用する。
            spawnAreaCenter.z + randomZ
        );
          GameObject spawnedObject  =Instantiate(fallingObjectPrefab, spawnPosition, Quaternion.identity);

        Destroy(spawnedObject, despawnTime);

    }
    ///<summary>
    /// Sceneビューで生成範囲を視覚的に表示するためのヘルパー機能 
    ///</summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(spawnAreaCenter, new Vector3(spawnAreaCenter.x, 0.1f, spawnAreaSize.y));
     } 
    
}
