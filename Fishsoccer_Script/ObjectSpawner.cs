
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

    
    public void StartSpawning()
    {
       
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    
    public void StopSpawning()
    {
        
        CancelInvoke(nameof(SpawnObject));
    }
    

    private void SpawnObject()
    {
        
        if (fallingObjectPrefab == null)
        {
            Debug.LogWarning("Falling Object Prefabが設定されていません");
            return;
        }

        
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomZ = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
       
        Vector3 spawnPosition = new Vector3(
            spawnAreaCenter.x + randomX,
            spawnAreaCenter.y,
            spawnAreaCenter.z + randomZ
        );
          GameObject spawnedObject  =Instantiate(fallingObjectPrefab, spawnPosition, Quaternion.identity);

        Destroy(spawnedObject, despawnTime);

    }
       private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(spawnAreaCenter, new Vector3(spawnAreaCenter.x, 0.1f, spawnAreaSize.y));
     } 
    
}
