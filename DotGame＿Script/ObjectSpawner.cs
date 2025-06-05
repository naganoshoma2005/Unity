
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    public Text Cointext;
    public GameObject objectPrefab; // 生成するオブジェクトのプレハブ
    public float minSpawnInterval = 1f; // 最小生成間隔（秒）
    public float maxSpawnInterval = 5f; // 最大生成間隔（秒）
    public Vector3 spawnPosition = new Vector3(10f, 0f, 0f); // 生成位置

    void Start()
    {
        // 一定間隔でオブジェクトを生成するコルーチンを開始
        StartCoroutine(SpawnObjectRoutine());
    }

    private IEnumerator SpawnObjectRoutine()
    {
        while (true)
        {
            // オブジェクトを生成

            string[] itemList = { "Gem", "Coin" };

            // 例としてクリック可能なオブジェクトを一つ生成して設定
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            GameObject obj = Instantiate(objectPrefab, spawnPos, Quaternion.identity);
            Onclick clickable = obj.GetComponent<Onclick>();
            clickable.items = itemList;
            clickable.dropChance = 0.5f; // アイテムがドロップする確率を50%に設定
            DestroyOnCollision destroy = obj.GetComponent<DestroyOnCollision>();
            PlayerStats playerStats = Cointext.GetComponent<PlayerStats>();
            if (destroy != null)
            {
                destroy.playerStats = playerStats;
            }

            // ランダムな生成間隔を待つ
            float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(randomInterval);
        }
    }
}
