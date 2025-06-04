using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    float timer = 0.0f;
    public bool shootspeed;
    // 弾のPrefab
    public GameObject projectilePrefab;

    // 発射位置
    public Transform firePoint;

    // 発射間隔（秒）
    public float fireInterval = 1.0f;

    // 弾の速度
    public float projectileSpeed = 10.0f;

    void Start()
    {
        // コルーチンを開始して一定間隔で発射する
        StartCoroutine(FireRoutine());
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            Fire();
            // 発射間隔待機
            yield return new WaitForSeconds(fireInterval);
        }
    }
    public void Power()
    {
        if (shootspeed == false)
        {
            fireInterval /= 5;
            shootspeed = true;
                }
    }
   
    private void Update()
    {
       if(shootspeed==true)
        {
            timer = Time.deltaTime;
            if(timer==5.0f)
            {
                shootspeed = false;
                fireInterval *= 5;
                timer = 0.0f;
            }

        }
    }

    void Fire()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // 弾を発射位置に生成
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // 弾のRigidbody2Dを取得
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // 発射方向に力を加える
                rb.linearVelocity = firePoint.right * projectileSpeed;

              
            }
            else
            {
                Debug.LogError("Projectile does not have a Rigidbody2D component.");
            }
        }
        else
        {
            Debug.LogError("Projectile Prefab or Fire Point is not set.");
        }
    }
}
