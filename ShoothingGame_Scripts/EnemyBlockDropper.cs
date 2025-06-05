using UnityEngine;

public class EnemyBlockDropper : MonoBehaviour
{
    public GameObject blockPrefab;  // 落とすブロックのプレハブ
    public GameObject markerPrefab; // 落下地点に生成するオブジェクトのプレハブ
    public Transform player;        // プレイヤーのTransform
    public float dropInterval = 3f; // ブロックを落とす間隔
    public float blockSpawnHeight = 15f; // プレイヤーの頭上に落とすブロックの高さ
    public Vector3 blockSpawnOffset = new Vector3(0, 0, 0); // プレイヤーの頭上に対するオフセット
    public float markerDuration = 2f; // マーカーを一定時間で消すための時間
    public float dropDelay = 2f;  // ブロックが落下を開始するまでの遅延時間
    public AudioClip dropSound;    // ブロックが生成されたときのサウンド

    private float dropTimer;        // ブロックを落とすタイマー
    private AudioSource audioSource; // AudioSourceの参照
    private EnemyHealth enemyHealth; // 敵のHP管理コンポーネント

    void Start()
    {
        dropTimer = 0f; // タイマーを初期化
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSourceを追加
        audioSource.clip = dropSound; // サウンドを設定

        // 敵のHP管理コンポーネントを取得
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        // 敵のHPが0以下の場合、ブロック生成を停止して既存のブロックを削除
        if (gameObject != null && enemyHealth.currentHealth <= 0)
        {
            DestroyAllBlocks();
            return;
        }

        // 一定時間が経過したらブロックを落とす
        dropTimer += Time.deltaTime;
        if (dropTimer >= dropInterval)
        {
            DropBlock();
            dropTimer = 0f; // タイマーをリセット
        }
    }

    void DropBlock()
    {
        Vector3 spawnPosition = player.position + blockSpawnOffset + Vector3.up * blockSpawnHeight;
        GameObject block = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);

        if (block != null)
        {
            audioSource.PlayOneShot(dropSound);
        }

        Vector3 markerPosition = player.position + blockSpawnOffset;
        markerPosition.y = 0.01f;
        GameObject marker = Instantiate(markerPrefab, markerPosition, Quaternion.identity);
        Destroy(marker, markerDuration);

        StartCoroutine(DelayedDrop(block));
    }

    // 一定時間後にブロックの落下を開始する
    System.Collections.IEnumerator DelayedDrop(GameObject block)
    {
        yield return new WaitForSeconds(dropDelay);

        Rigidbody rb = block.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    // 生成済みのすべてのブロックを削除するメソッド
    void DestroyAllBlocks()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Block"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}
