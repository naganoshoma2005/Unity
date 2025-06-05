
using UnityEngine;

public class RandomMoveWithRotation : MonoBehaviour
{
    public float moveSpeed = 3f; // 移動速度
    public float directionChangeInterval = 2f; // 方向を変える間隔（秒）
    public float moveDuration = 1.5f; // 1回の移動継続時間（秒）
    public float rotationSpeed = 5f; // 回転速度

    private Vector3 moveDirection;
    private float changeDirectionTimer;
    private float moveTimer;

    void Start()
    {
        // 初期方向をランダムに設定
        SetRandomDirection();
    }

    void Update()
    {
        // 方向を変更するタイマーを更新
        changeDirectionTimer += Time.deltaTime;

        // 一定間隔でランダム方向を設定
        if (changeDirectionTimer >= directionChangeInterval)
        {
            SetRandomDirection();
            changeDirectionTimer = 0f;
        }

        // 移動継続時間内なら移動と回転を実行
        if (moveTimer > 0)
        {
            moveTimer -= Time.deltaTime;

            // オブジェクトを移動
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

            // オブジェクトの向きを移動方向に合わせる
            RotateTowardsMoveDirection();
        }
    }

    void SetRandomDirection()
    {
        // ランダムな方向を設定
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);

        // Y軸を0に固定して水平移動
        moveDirection = new Vector3(randomX, 0, randomZ).normalized;

        // 移動継続時間をリセット
        moveTimer = moveDuration;
    }

    void RotateTowardsMoveDirection()
    {
        // 現在の向きと移動方向の間の回転を計算
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}

/*
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RandomMoveWithRotation : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float directionChangeInterval = 2f;
    public float moveDuration = 1.5f;
    public float rotationSpeed = 5f;

    private Vector3 moveDirection;
    private float changeDirectionTimer;
    private float moveTimer;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetRandomDirection();
    }

    void FixedUpdate()
    {
        // 方向を変更するタイマーを更新
        changeDirectionTimer += Time.fixedDeltaTime;

        if (changeDirectionTimer >= directionChangeInterval)
        {
            SetRandomDirection();
            changeDirectionTimer = 0f;
        }

        // 移動継続時間内なら移動と回転を実行
        if (moveTimer > 0)
        {
            moveTimer -= Time.fixedDeltaTime;

            // Rigidbody を使った移動
            rb.linearVelocity = moveDirection * moveSpeed;

            // 回転方向を変更
            RotateTowardsMoveDirection();
        }
        else
        {
            // 移動が終わったら速度を 0 にする
            rb.linearVelocity = Vector3.zero;
        }
    }

    void SetRandomDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        moveDirection = new Vector3(randomX, 0, randomZ).normalized;
        moveTimer = moveDuration;
    }

    void RotateTowardsMoveDirection()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}*/
