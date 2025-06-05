using System.Collections;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;           // 発射する弾のプレハブ
    public Transform[] firePoints;            // 弾が発射される複数の位置
    public float fireRate = 2.0f;             // 弾の発射間隔
    public float bulletSpeed = 10.0f;         // 弾の速度

    private Transform target;                 // プレイヤーのターゲット
    private float fireCountdown = 0f;         // 発射までのカウントダウンタイマー
    private Animator animator;
    private EnemyHealth enemyHealth;          // EnemyHealth コンポーネントへの参照

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>(); // EnemyHealth コンポーネントを取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        fireCountdown = 3f; // 初回の発射を遅らせる
    }


    void Update()
    {
        // ターゲットがない、またはHPが0以下なら発射しない
        if (target == null || enemyHealth == null || enemyHealth.currentHealth <= 0) return;

        // 一定間隔ごとに弾を発射
        if (fireCountdown <= 0f)
        {
            animator.SetBool("Fire", true);
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        foreach (Transform firePoint in firePoints)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direction = (target.position - firePoint.position).normalized;
                rb.linearVelocity = direction * bulletSpeed;
            }
            else
            {
                Debug.LogWarning("Rigidbodyがアタッチされていません: " + bullet.name);
            }
        }
    }
}