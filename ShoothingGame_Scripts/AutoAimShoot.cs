
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAimShoot : MonoBehaviour
{
    public Transform gunBarrel; // 銃の発射位置
    public GameObject bulletPrefab; // 弾丸のプレハブ
    public float fireRate = 1f; // 銃の発射速度
    public float range = 50f; // 自動標準の範囲
    public LayerMask targetLayer; // 敵がいるレイヤー
    public float turnSpeed = 5f; // 自動標準時の回転速度
    public float shootDelay = 0.5f; // 移動停止後の弾発射遅延時間

    private Transform target;
    private float nextFireTime = 0f;
    private Coroutine shootCoroutine;
    private bool isEvading = false; // 回避中かどうかを示すフラグ

    void Update()
    {
        FindTarget();
        if (target != null && !isEvading) // 回避中でない場合にのみ射撃
        {
            AimAtTarget();
            // AキーまたはDキーが押されていない場合
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                // 移動キーが押されていない時に射撃コルーチンを開始
                if (shootCoroutine == null)
                {
                    shootCoroutine = StartCoroutine(ShootAfterDelay());
                }
            }
            else
            {
                // 移動キーが押されている場合はコルーチンを停止
                if (shootCoroutine != null)
                {
                    StopCoroutine(shootCoroutine);
                    shootCoroutine = null;
                }
            }
        }
    }

    // 一番近い敵を見つける
    void FindTarget()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, range, targetLayer);

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }

        target = closestEnemy != null ? closestEnemy : null;
    }

    // 敵に向かって銃を回転させる
    void AimAtTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    // 移動停止後に弾を発射するコルーチン
    IEnumerator ShootAfterDelay()
    {
        yield return new WaitForSeconds(shootDelay);

        // 弾を発射
        ShootAtTarget();

        // コルーチンを再度開始
        shootCoroutine = StartCoroutine(ShootAfterDelay());
    }

    // 一定の間隔で敵に向けて撃つ
    void ShootAtTarget()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;

            // 弾丸を生成して発射
            GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);

            // 発射者と弾丸のコライダーの衝突を無視する
            Collider bulletCollider = bullet.GetComponent<Collider>();
            Collider gunBarrelCollider = gunBarrel.GetComponent<Collider>();
            if (bulletCollider != null && gunBarrelCollider != null)
            {
                Physics.IgnoreCollision(bulletCollider, gunBarrelCollider);
            }

            // 生成した弾丸にターゲットを設定する
            HomingBullet homingBullet = bullet.GetComponent<HomingBullet>();
            if (homingBullet != null)
            {
                homingBullet.SetTarget(target); // ターゲットを弾丸に渡す
            }
        }
    }

    // 回避が開始されたときに呼び出すメソッド
    public void StartEvade()
    {
        isEvading = true; // 回避中フラグを立てる
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine); // コルーチンを停止
            shootCoroutine = null;
        }

        // 発射中の弾を消去（必要に応じて）
        // ここに弾を消すロジックを追加
        DestroyActiveBullets();
    }

    // 回避が終了したときに呼び出すメソッド
    public void EndEvade()
    {
        isEvading = false; // 回避中フラグを解除
        if (shootCoroutine == null)
        {
            shootCoroutine = StartCoroutine(ShootAfterDelay()); // 再開
        }
    }

    // 発射中の弾を消去するメソッド
    void DestroyActiveBullets()
    {
        // シーン内の全弾を検索し、削除する
        HomingBullet[] bullets = FindObjectsOfType<HomingBullet>();
        foreach (HomingBullet bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
    }
}*/

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAimShoot : MonoBehaviour
{
    public Transform gunBarrel; // 銃の発射位置
    public GameObject bulletPrefab; // 弾丸のプレハブ
    public float fireRate = 1f; // 銃の発射速度
    public float range = 50f; // 自動標準の範囲
    public LayerMask targetLayer; // 敵がいるレイヤー
    public float turnSpeed = 5f; // 自動標準時の回転速度
    public float shootDelay = 0.5f; // 移動停止後の弾発射遅延時間

    private Transform target;
    private float nextFireTime = 0f;
    private Coroutine shootCoroutine;
    private bool isEvading = false; // 回避中かどうかを示すフラグ

    void Update()
    {
        FindTarget();
        if (target != null && !isEvading) // 回避中でない場合にのみ射撃
        {
            AimAtTarget();

            // タッチ入力をチェック
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // タッチが動いているかどうかをチェック
                if (touch.phase == TouchPhase.Moved)
                {
                    // スワイプ中は射撃を停止
                    if (shootCoroutine != null)
                    {
                        StopCoroutine(shootCoroutine);
                        shootCoroutine = null;
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    // タッチが終わったときに射撃コルーチンを開始
                    if (shootCoroutine == null)
                    {
                        shootCoroutine = StartCoroutine(ShootAfterDelay());
                    }
                }
            }
            else
            {
                // タッチがない場合は射撃コルーチンを開始
                if (shootCoroutine == null)
                {
                    shootCoroutine = StartCoroutine(ShootAfterDelay());
                }
            }
        }
    }

    // 一番近い敵を見つける
    void FindTarget()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, range, targetLayer);

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }

        target = closestEnemy != null ? closestEnemy : null;
    }

    // 敵に向かって銃を回転させる
    void AimAtTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    // 移動停止後に弾を発射するコルーチン
    IEnumerator ShootAfterDelay()
    {
        yield return new WaitForSeconds(shootDelay);

        // 弾を発射
        ShootAtTarget();

        // コルーチンを再度開始
        shootCoroutine = StartCoroutine(ShootAfterDelay());
    }

    // 一定の間隔で敵に向けて撃つ
    void ShootAtTarget()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;

            // 弾丸を生成して発射
            GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);

            // 発射者と弾丸のコライダーの衝突を無視する
            Collider bulletCollider = bullet.GetComponent<Collider>();
            Collider gunBarrelCollider = gunBarrel.GetComponent<Collider>();
            if (bulletCollider != null && gunBarrelCollider != null)
            {
                Physics.IgnoreCollision(bulletCollider, gunBarrelCollider);
            }

            // 生成した弾丸にターゲットを設定する
            HomingBullet homingBullet = bullet.GetComponent<HomingBullet>();
            if (homingBullet != null)
            {
                homingBullet.SetTarget(target); // ターゲットを弾丸に渡す
            }
        }
    }

    // 回避が開始されたときに呼び出すメソッド
    public void StartEvade()
    {
        isEvading = true; // 回避中フラグを立てる
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine); // コルーチンを停止
            shootCoroutine = null;
        }

        // 発射中の弾を消去（必要に応じて）
        // ここに弾を消すロジックを追加
        DestroyActiveBullets();
    }

    // 回避が終了したときに呼び出すメソッド
    public void EndEvade()
    {
        isEvading = false; // 回避中フラグを解除
        if (shootCoroutine == null)
        {
            shootCoroutine = StartCoroutine(ShootAfterDelay()); // 再開
        }
    }

    // 発射中の弾を消去するメソッド
    void DestroyActiveBullets()
    {
        // シーン内の全弾を検索し、削除する
        HomingBullet[] bullets = FindObjectsOfType<HomingBullet>();
        foreach (HomingBullet bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
    }
}*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAimShoot : MonoBehaviour
{
    public Transform gunBarrel; // 銃の発射位置
    public GameObject bulletPrefab; // 弾丸のプレハブ
    public float fireRate = 1f; // 銃の発射速度
    public float range = 50f; // 自動標準の範囲
    public LayerMask targetLayer; // 敵がいるレイヤー
    public float turnSpeed = 5f; // 自動標準時の回転速度
    public float shootDelay = 0.5f; // 移動停止後の弾発射遅延時間

    private Transform target;
    private float nextFireTime = 0f;
    private Coroutine shootCoroutine;
    private bool isEvading = false; // 回避中かどうかを示すフラグ

    void Update()
    {
        FindTarget();
        if (target != null && !isEvading) // 回避中でない場合にのみ射撃
        {
            AimAtTarget();

            // タッチ入力をチェック
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // タッチが動いているかどうかをチェック
                if (touch.phase == TouchPhase.Moved)
                {
                    // スワイプ中は射撃を停止
                    if (shootCoroutine != null)
                    {
                        StopCoroutine(shootCoroutine);
                        shootCoroutine = null;
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    // タッチが終わったときに射撃コルーチンを開始
                    if (shootCoroutine == null)
                    {
                        shootCoroutine = StartCoroutine(ShootAfterDelay());
                    }
                }
            }
            else
            {
                // タッチがない場合は射撃コルーチンを開始
                if (shootCoroutine == null)
                {
                    shootCoroutine = StartCoroutine(ShootAfterDelay());
                }
            }
        }
    }

    // 一番近い敵を見つける
    void FindTarget()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, range, targetLayer);

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }

        target = closestEnemy != null ? closestEnemy : null;
    }

    // 敵に向かって銃を回転させる
    void AimAtTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    // 移動停止後に弾を発射するコルーチン
    IEnumerator ShootAfterDelay()
    {
        yield return new WaitForSeconds(shootDelay);

        // 弾を発射
        ShootAtTarget();

        // コルーチンを再度開始
        shootCoroutine = StartCoroutine(ShootAfterDelay());
    }

    // 一定の間隔で敵に向けて撃つ
    void ShootAtTarget()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;

            // 弾丸を生成して発射
            GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);

            // 発射者と弾丸のコライダーの衝突を無視する
            Collider bulletCollider = bullet.GetComponent<Collider>();
            Collider gunBarrelCollider = gunBarrel.GetComponent<Collider>();
            if (bulletCollider != null && gunBarrelCollider != null)
            {
                Physics.IgnoreCollision(bulletCollider, gunBarrelCollider);
            }

            // 生成した弾丸にターゲットを設定する
            HomingBullet homingBullet = bullet.GetComponent<HomingBullet>();
            if (homingBullet != null)
            {
                homingBullet.SetTarget(target); // ターゲットを弾丸に渡す
            }
        }
    }

    // 回避が開始されたときに呼び出すメソッド
    
    public void StartEvade()
    {
        isEvading = true; // 回避中フラグを立てる
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine); // コルーチンを停止
            shootCoroutine = null;
        }

        // 発射中の弾を消去（必要に応じて）
        // ここに弾を消すロジックを追加
        DestroyActiveBullets();
    }

    // 回避が終了したときに呼び出すメソッド
    public void EndEvade()
    {
        isEvading = false; // 回避中フラグを解除
        if (shootCoroutine == null)
        {
            shootCoroutine = StartCoroutine(ShootAfterDelay()); // 再開
        }
    }

    // 発射中の弾を消去するメソッド
   
    void DestroyActiveBullets()
    {
        // シーン内の全弾を検索し、削除する
        HomingBullet[] bullets = FindObjectsOfType<HomingBullet>();
        foreach (HomingBullet bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
    }
}