using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour
{
    [Header("設定")]
    [Tooltip("生成したい3種類のプレハブをここにドラッグ&ドロップ")]
    public GameObject[] spawnObjects; 

    [Tooltip("スポーン範囲の基準となるPlane(床)")]
    public GameObject targetPlane; 

    [Tooltip("生成する間隔（秒）")]
    public float spawnInterval = 2.0f; 

    [Tooltip("床からどれくらい高い位置に出すか")]
    public float heightOffset = 1.0f;


    [Tooltip("生成されたオブジェクトが自動で消滅するまでの時間(秒)。0以下の場合は消滅しません。")]
    public float destructionTime = 5.0f;

    
    void Start()
    {
        if (targetPlane == null)
        {
            Debug.LogError("Target Planeが設定されていません");
            return;
        }

        StartCoroutine(SpawnLoop());
    }

  
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(spawnInterval);

       
            SpawnRandomObject();
        }
    }

    void SpawnRandomObject()
    {
        
        int randomIndex = Random.Range(0, spawnObjects.Length);
        GameObject objectToSpawn = spawnObjects[randomIndex];

      
        Collider planeCollider = targetPlane.GetComponent<Collider>();
        
        float minX = planeCollider.bounds.min.x;
        float maxX = planeCollider.bounds.max.x;
        float minZ = planeCollider.bounds.min.z;
        float maxZ = planeCollider.bounds.max.z;

        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        
        float spawnY = planeCollider.bounds.center.y + heightOffset;

        Vector3 spawnPosition = new Vector3(randomX, spawnY, randomZ);

    
        GameObject spawnedInstance = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

       
        if (destructionTime > 0f)
        {
           
            Destroy(spawnedInstance, destructionTime);
        }
    }
}