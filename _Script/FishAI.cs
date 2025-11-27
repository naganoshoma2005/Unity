
/*
// FishAI.cs
using UnityEngine;

public enum FishState
{
    Idle,
    ChaseBall,
    Shoot,
    Pass,
    Return,
    Defend
}

public class FishAI : MonoBehaviour
{
    public FishState currentState = FishState.Idle;
    public Transform ball;
    public Transform goal;
    public Transform homePosition;
    public Transform[] allies;
    public Transform[] enemies;

    public float maxSpeed = 3f;
    public float detectionRadius = 5f;
    public float cohesionWeight = 0.5f;
    public float separationWeight = 2.0f;
    public float alignmentWeight = 0.5f;
    public float ballChaseDistance = 10f;
    public float smoothTurnSpeed = 5f;

    public float shootForce = 15f; // シュートの力を設定するための変数

    private Vector3 currentVelocity;

    void Update()
    {
        UpdateState();
        MoveBasedOnState();
    }

    void UpdateState()
    {
        float distToBall = Vector3.Distance(transform.position, ball.position);

        // ボールが近く、壁を使ったシュートが可能かをチェック
        if (distToBall < ballChaseDistance)
        {
            if (IsWallShotPossible())
            {
                currentState = FishState.Shoot;
                return;
            }
        }
        
        if (distToBall < 2.5f)
        {
            currentState = FishState.Shoot;
        }
        else if (distToBall < ballChaseDistance)
        {
            currentState = FishState.ChaseBall;
        }
        else
        {
            currentState = FishState.Return;
        }
    }

    void MoveBasedOnState()
    {
        Vector3 alignment = ComputeAlignment() * alignmentWeight;
        Vector3 cohesion = ComputeCohesion() * cohesionWeight;
        Vector3 separation = ComputeSeparation() * separationWeight;
        Vector3 baseSteering = alignment + cohesion + separation;

        Vector3 purposeSteering = Vector3.zero;

        switch (currentState)
        {
            case FishState.ChaseBall:
                purposeSteering = (ball.position - transform.position).normalized;
                break;
            case FishState.Shoot:
            {
                Vector3 ballToGoal = (goal.position - ball.position).normalized;
                Vector3 ballToFish = (ball.position - transform.position);
                
                // ゴール方向へ押しやすくするため、球の方向へ斜めからアプローチ
                purposeSteering = (ballToGoal + ballToFish.normalized).normalized;
                break;
            }

            case FishState.Return:
                purposeSteering = (homePosition.position - transform.position).normalized;
                break;
            case FishState.Defend:
                Transform nearestEnemy = GetNearestEnemy();
                if (nearestEnemy != null)
                    purposeSteering = -(nearestEnemy.position - transform.position).normalized;
                break;
        }

        Vector3 desiredDirection = (baseSteering + purposeSteering).normalized;
        currentVelocity = Vector3.Lerp(currentVelocity, desiredDirection * maxSpeed, Time.deltaTime * smoothTurnSpeed);

        transform.position += currentVelocity * Time.deltaTime;
        if (currentVelocity != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, currentVelocity.normalized, Time.deltaTime * smoothTurnSpeed);
        }
    }

    // 壁を使ったシュートが可能か判断する新しい関数
    bool IsWallShotPossible()
    {
        // ボールが壁に当たるか予測するためのレイキャスト
        RaycastHit hit;
        if (Physics.Raycast(ball.position, ball.GetComponent<Rigidbody>().linearVelocity.normalized, out hit, 5f))
        {
            // 衝突したオブジェクトが壁であることを確認
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                Vector3 reflectedDirection = Vector3.Reflect(ball.GetComponent<Rigidbody>().linearVelocity.normalized, hit.normal);

                // 衝突地点から、反射方向にレイキャストを再度飛ばしてゴールに当たるか確認
                RaycastHit goalHit;
                if (Physics.Raycast(hit.point, reflectedDirection, out goalHit, 100f))
                {
                    // 2つ目のレイキャストがゴールに当たったかチェック
                    if (goalHit.collider.gameObject.CompareTag("Goal"))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    
    // シュートの実行
    void ShootBall()
    {
        // ボールのRigidbodyを取得
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        if (ballRb == null) return; // Rigidbodyがない場合は何もしない

        // ゴールに向かう方向を計算
        Vector3 shootDirection = (goal.position - ball.position).normalized;

        // ボールに力を加える
        ballRb.AddForce(shootDirection * shootForce, ForceMode.Impulse);
    }
    
    // 魚が他のオブジェクトに触れたときに呼ばれる
    void OnCollisionEnter(Collision other)
    {
        // 接触したのがボールであり、かつ現在の状態がシュートモードであるかチェック
        if (other.gameObject == ball.gameObject && currentState == FishState.Shoot)
        {
            ShootBall();
        }
    }

    Vector3 ComputeAlignment()
    {
        Vector3 avgDir = Vector3.zero;
        int count = 0;
        foreach (var ally in allies)
        {
            if (ally == transform) continue;
            if (Vector3.Distance(transform.position, ally.position) < detectionRadius)
            {
                avgDir += ally.forward;
                count++;
            }
        }
        return count > 0 ? avgDir.normalized : Vector3.zero;
    }

    Vector3 ComputeCohesion()
    {
        Vector3 center = Vector3.zero;
        int count = 0;
        foreach (var ally in allies)
        {
            if (ally == transform) continue;
            if (Vector3.Distance(transform.position, ally.position) < detectionRadius)
            {
                center += ally.position;
                count++;
            }
        }
        return count > 0 ? (center / count - transform.position).normalized : Vector3.zero;
    }

    Vector3 ComputeSeparation()
    {
        Vector3 avoid = Vector3.zero;
        foreach (var ally in allies)
        {
            if (ally == transform) continue;
            float dist = Vector3.Distance(transform.position, ally.position);
            if (dist < 1.0f)
            {
                avoid -= (ally.position - transform.position).normalized / dist;
            }
        }
        return avoid.normalized;
    }

    Transform GetNearestEnemy()
    {
        Transform nearest = null;
        float minDist = float.MaxValue;
        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }
        return nearest;
    }
}*/
/*
// FishAI.cs
using UnityEngine;

public enum FishState
{
    Idle,
    ChaseBall,
    Shoot,
    Pass,
    Return,
    Defend
}

public class FishAI : MonoBehaviour
{
    public FishState currentState = FishState.Idle;
    public Transform ball;
    public Transform myGoal;      // AIが守るゴール
    public Transform opponentGoal; // AIが攻めるゴール
    public Transform homePosition;
    public Transform[] allies;
    public Transform[] enemies;

    public float maxSpeed = 3f;
    public float detectionRadius = 5f;
    public float cohesionWeight = 0.5f;
    public float separationWeight = 2.0f;
    public float alignmentWeight = 0.5f;
    public float ballChaseDistance = 10f;
    public float smoothTurnSpeed = 5f;

    public float shootForce = 15f; // シュートの力を設定するための変数

    private Vector3 currentVelocity;

    void Update()
    {
        UpdateState();
        MoveBasedOnState();
    }

    void UpdateState()
    {
        float distToBall = Vector3.Distance(transform.position, ball.position);

        if (distToBall < ballChaseDistance)
        {
            if (IsWallShotPossible())
            {
                currentState = FishState.Shoot;
                return;
            }
        }
        
        if (distToBall < 2.5f)
        {
            currentState = FishState.Shoot;
        }
        else if (distToBall < ballChaseDistance)
        {
            currentState = FishState.ChaseBall;
        }
        else
        {
            currentState = FishState.Return;
        }
    }

    void MoveBasedOnState()
    {
        Vector3 alignment = ComputeAlignment() * alignmentWeight;
        Vector3 cohesion = ComputeCohesion() * cohesionWeight;
        Vector3 separation = ComputeSeparation() * separationWeight;
        Vector3 baseSteering = alignment + cohesion + separation;

        Vector3 purposeSteering = Vector3.zero;

        switch (currentState)
        {
            case FishState.ChaseBall:
                purposeSteering = (ball.position - transform.position).normalized;
                break;
            case FishState.Shoot:
            {
                // 攻めるゴールへの方向を使用
                Vector3 ballToGoal = (opponentGoal.position - ball.position).normalized;
                Vector3 ballToFish = (ball.position - transform.position);
                
                purposeSteering = (ballToGoal + ballToFish.normalized).normalized;
                break;
            }

            case FishState.Return:
                purposeSteering = (homePosition.position - transform.position).normalized;
                break;
            case FishState.Defend:
                Transform nearestEnemy = GetNearestEnemy();
                if (nearestEnemy != null)
                    purposeSteering = -(nearestEnemy.position - transform.position).normalized;
                break;
        }

        Vector3 desiredDirection = (baseSteering + purposeSteering).normalized;
        currentVelocity = Vector3.Lerp(currentVelocity, desiredDirection * maxSpeed, Time.deltaTime * smoothTurnSpeed);

        transform.position += currentVelocity * Time.deltaTime;
        if (currentVelocity != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, currentVelocity.normalized, Time.deltaTime * smoothTurnSpeed);
        }
    }

    bool IsWallShotPossible()
    {
        RaycastHit hit;
        if (Physics.Raycast(ball.position, ball.GetComponent<Rigidbody>().linearVelocity.normalized, out hit, 5f))
        {
            if (hit.collider.gameObject.CompareTag("Wall3"))
            {
                Vector3 reflectedDirection = Vector3.Reflect(ball.GetComponent<Rigidbody>().linearVelocity.normalized, hit.normal);

                RaycastHit goalHit;
                if (Physics.Raycast(hit.point, reflectedDirection, out goalHit, 100f))
                {
                    // 衝突したオブジェクトが攻めるゴールであるか、参照で直接比較
                    if (goalHit.collider.gameObject == opponentGoal.gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    
    void ShootBall()
    {
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        if (ballRb == null) return;

        // 攻めるゴールへの方向を使用
        Vector3 shootDirection = (opponentGoal.position - ball.position).normalized;

        ballRb.AddForce(shootDirection * shootForce, ForceMode.Impulse);
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == ball.gameObject && currentState == FishState.Shoot)
        {
            ShootBall();
        }
    }

    Vector3 ComputeAlignment()
    {
        Vector3 avgDir = Vector3.zero;
        int count = 0;
        foreach (var ally in allies)
        {
            if (ally == transform) continue;
            if (Vector3.Distance(transform.position, ally.position) < detectionRadius)
            {
                avgDir += ally.forward;
                count++;
            }
        }
        return count > 0 ? avgDir.normalized : Vector3.zero;
    }

    Vector3 ComputeCohesion()
    {
        Vector3 center = Vector3.zero;
        int count = 0;
        foreach (var ally in allies)
        {
            if (ally == transform) continue;
            if (Vector3.Distance(transform.position, ally.position) < detectionRadius)
            {
                center += ally.position;
                count++;
            }
        }
        return count > 0 ? (center / count - transform.position).normalized : Vector3.zero;
    }

    Vector3 ComputeSeparation()
    {
        Vector3 avoid = Vector3.zero;
        foreach (var ally in allies)
        {
            if (ally == transform) continue;
            float dist = Vector3.Distance(transform.position, ally.position);
            if (dist < 1.0f)
            {
                avoid -= (ally.position - transform.position).normalized / dist;
            }
        }
        return avoid.normalized;
    }

    Transform GetNearestEnemy()
    {
        Transform nearest = null;
        float minDist = float.MaxValue;
        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }
        return nearest;
    }
}*/


// FishAI.cs
using UnityEngine;

public enum FishState
{
    Idle,
    ChaseBall,
    Shoot,
    Pass,
    Return,
    Defend
}

public class FishAI : MonoBehaviour
{
    public FishState currentState = FishState.Idle;
    public Transform ball;
    public Transform myGoal;      // AIが守るゴール
    public Transform opponentGoal; // AIが攻めるゴール
    public Transform homePosition;
    public Transform[] allies;
    public Transform[] enemies;

    public float maxSpeed = 3f;
    public float detectionRadius = 5f;
    public float cohesionWeight = 0.5f;
    public float separationWeight = 2.0f;
    public float alignmentWeight = 0.5f;
    public float ballChaseDistance = 10f;
    public float smoothTurnSpeed = 5f;

    public float shootForce = 15f; // シュートの力を設定するための変数

    private Vector3 currentVelocity;
    // --- ▼ここから追加▼ ---
    private bool isControllable = false;
    // --- ▲ここまで追加▲ ---

    void Update()
    {
        // --- ▼ここから追加▼ ---
        // isControllableがfalseの場合は処理を中断する
        if (!isControllable)
        {
            return;
        }
        // --- ▲ここまで追加▲ ---
        UpdateState();
        MoveBasedOnState();
    }

    void UpdateState()
    {
        float distToBall = Vector3.Distance(transform.position, ball.position);

        if (distToBall < ballChaseDistance)
        {
            if (IsWallShotPossible())
            {
                currentState = FishState.Shoot;
                return;
            }
        }
        
        if (distToBall < 2.5f)
        {
            currentState = FishState.Shoot;
        }
        else if (distToBall < ballChaseDistance)
        {
            currentState = FishState.ChaseBall;
        }
        else
        {
            currentState = FishState.Return;
        }
    }

    void MoveBasedOnState()
    {
        Vector3 alignment = ComputeAlignment() * alignmentWeight;
        Vector3 cohesion = ComputeCohesion() * cohesionWeight;
        Vector3 separation = ComputeSeparation() * separationWeight;
        Vector3 baseSteering = alignment + cohesion + separation;

        Vector3 purposeSteering = Vector3.zero;

        switch (currentState)
        {
            case FishState.ChaseBall:
                purposeSteering = (ball.position - transform.position).normalized;
                break;
            case FishState.Shoot:
            {
                // 攻めるゴールへの方向を使用
                Vector3 ballToGoal = (opponentGoal.position - ball.position).normalized;
                Vector3 ballToFish = (ball.position - transform.position);
                
                purposeSteering = (ballToGoal + ballToFish.normalized).normalized;
                break;
            }

            case FishState.Return:
                purposeSteering = (homePosition.position - transform.position).normalized;
                break;
            case FishState.Defend:
                Transform nearestEnemy = GetNearestEnemy();
                if (nearestEnemy != null)
                    purposeSteering = -(nearestEnemy.position - transform.position).normalized;
                break;
        }

        Vector3 desiredDirection = (baseSteering + purposeSteering).normalized;
        currentVelocity = Vector3.Lerp(currentVelocity, desiredDirection * maxSpeed, Time.deltaTime * smoothTurnSpeed);

        transform.position += currentVelocity * Time.deltaTime;
        if (currentVelocity != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, currentVelocity.normalized, Time.deltaTime * smoothTurnSpeed);
        }
    }

    bool IsWallShotPossible()
    {
        RaycastHit hit;
        if (Physics.Raycast(ball.position, ball.GetComponent<Rigidbody>().linearVelocity.normalized, out hit, 5f))
        {
            if (hit.collider.gameObject.CompareTag("Wall3"))
            {
                Vector3 reflectedDirection = Vector3.Reflect(ball.GetComponent<Rigidbody>().linearVelocity.normalized, hit.normal);

                RaycastHit goalHit;
                if (Physics.Raycast(hit.point, reflectedDirection, out goalHit, 100f))
                {
                    // 衝突したオブジェクトが攻めるゴールであるか、参照で直接比較
                    if (goalHit.collider.gameObject == opponentGoal.gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    
    void ShootBall()
    {
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        if (ballRb == null) return;

        // 攻めるゴールへの方向を使用
        Vector3 shootDirection = (opponentGoal.position - ball.position).normalized;

        ballRb.AddForce(shootDirection * shootForce, ForceMode.Impulse);
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == ball.gameObject && currentState == FishState.Shoot)
        {
            ShootBall();
        }
    }

    Vector3 ComputeAlignment()
    {
        Vector3 avgDir = Vector3.zero;
        int count = 0;
        foreach (var ally in allies)
        {
            if (ally == transform) continue;
            if (Vector3.Distance(transform.position, ally.position) < detectionRadius)
            {
                avgDir += ally.forward;
                count++;
            }
        }
        return count > 0 ? avgDir.normalized : Vector3.zero;
    }

    Vector3 ComputeCohesion()
    {
        Vector3 center = Vector3.zero;
        int count = 0;
        foreach (var ally in allies)
        {
            if (ally == transform) continue;
            if (Vector3.Distance(transform.position, ally.position) < detectionRadius)
            {
                center += ally.position;
                count++;
            }
        }
        return count > 0 ? (center / count - transform.position).normalized : Vector3.zero;
    }

    Vector3 ComputeSeparation()
    {
        Vector3 avoid = Vector3.zero;
        foreach (var ally in allies)
        {
            if (ally == transform) continue;
            float dist = Vector3.Distance(transform.position, ally.position);
            if (dist < 1.0f)
            {
                avoid -= (ally.position - transform.position).normalized / dist;
            }
        }
        return avoid.normalized;
    }

    Transform GetNearestEnemy()
    {
        Transform nearest = null;
        float minDist = float.MaxValue;
        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }
        return nearest;
    }
    
    // --- ▼ここから追加▼ ---
    // AIの動きを有効/無効にするパブリックメソッド
    public void SetControllable(bool isControllable)
    {
        this.isControllable = isControllable;
    }
    // --- ▲ここまで追加▲ ---
}