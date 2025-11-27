
/*
using UnityEngine;
using System.Collections;
using UnityEngine.UI; // UIを扱うために必要

public class MoveFish : MonoBehaviour
{
    // --- 基本的な移動とカメラ ---
    private Camera mainCamera;
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    private Rigidbody rb;
    public float rotationSpeed = 5f;

    // --- スタミナ関連 ---
    public float maxStamina = 100f;
    private float currentStamina;
    public float dashCost = 25f;
    public float staminaRegenRate = 15f;
    public Slider staminaSlider; // スタミナゲージ用のSlider

    // --- シュート機能関連 ---
    public float shootForce = 15f;
    public float shootCooldown = 1.0f;
    public Collider shootTrigger;
    private bool canShoot = true;
    private Rigidbody targetBallRb;

    // --- ガード機能関連 ---
    private bool isGuarding = false;
    public float guardRotationSpeed = 10f;
    private Quaternion targetGuardRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        currentStamina = maxStamina;

        // Sliderの初期設定
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }
    }

    void Update()
    {
        // HandleGuardInputメソッドの呼び出し
        HandleGuardInput();

        // シュートの入力検知
        if (!isGuarding && Input.GetMouseButtonDown(0) && canShoot && targetBallRb != null)
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (isGuarding)
        {
            // ガード中の処理
            rb.linearVelocity = Vector3.zero;
            rb.rotation = Quaternion.Lerp(rb.rotation, targetGuardRotation, guardRotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // 通常の移動処理
            HandleMovement();
        }

        // スタミナUIの更新
        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }
    }

    // ▼▼▼ エラーの原因となっていた、欠けていたメソッド ▼▼▼
    // このメソッドをクラス内に追加します。
    void HandleGuardInput()
    {
        // 右クリックでガード開始
        if (Input.GetMouseButtonDown(1) && !isGuarding)
        {
            isGuarding = true;
            // 現在の向きからY軸周りに90度回転した角度を目標に設定
            targetGuardRotation = rb.rotation * Quaternion.Euler(0, 90, 0);
        }
        // 右クリックを離したらガード終了
        else if (Input.GetMouseButtonUp(1) && isGuarding)
        {
            isGuarding = false;
        }
    }

    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = mainCamera.transform.forward * moveVertical + mainCamera.transform.right * moveHorizontal;
        movement.y = 0f;

        bool isMoving = movement.magnitude > 0.1f;
        movement.Normalize();

        bool isDashing = Input.GetKey(KeyCode.LeftShift) && isMoving && currentStamina > 0;

        float currentSpeed;
        if (isDashing)
        {
            currentSpeed = dashSpeed;
            currentStamina = Mathf.Max(0, currentStamina - dashCost * Time.fixedDeltaTime);
        }
        else
        {
            currentSpeed = moveSpeed;
            if (currentStamina < maxStamina)
            {
                currentStamina = Mathf.Min(maxStamina, currentStamina + staminaRegenRate * Time.fixedDeltaTime);
            }
        }
        
        rb.linearVelocity = movement * currentSpeed;

        Quaternion targetRotation;
        if (isMoving)
        {
            targetRotation = Quaternion.LookRotation(movement);
        }
        else
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0;
            targetRotation = Quaternion.LookRotation(cameraForward);
        }
        
        rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    void Shoot()
    {
        Vector3 forceDirection = transform.forward;
        targetBallRb.AddForce(forceDirection * shootForce, ForceMode.Impulse);
        StartCoroutine(ShootCooldown());
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (shootTrigger != null && other.CompareTag("Ball"))
        {
            if (other.bounds.Intersects(shootTrigger.bounds))
            {
                targetBallRb = other.GetComponent<Rigidbody>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (shootTrigger != null && other.CompareTag("Ball"))
        {
            if (targetBallRb != null && targetBallRb.gameObject == other.gameObject)
            {
                targetBallRb = null;
            }
        }
    }
}*/
/*
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveFish : MonoBehaviour
{
    // ...（変数の定義は変更なし）...
    
    // --- 基本的な移動とカメラ ---
    private Camera mainCamera;
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    private Rigidbody rb;
    public float rotationSpeed = 5f;

    // --- スタミナ関連 ---
    public float maxStamina = 100f;
    private float currentStamina;
    public float dashCost = 25f;
    public float staminaRegenRate = 15f;
    public Slider staminaSlider;

    // --- シュート機能関連 ---
    public float shootForce = 15f;
    public float shootCooldown = 1.0f;
    public Collider shootTrigger;
    private bool canShoot = true;
    private Rigidbody targetBallRb;

    // --- ガード機能関連 ---
    private bool isGuarding = false;
    public float guardRotationSpeed = 10f;
    private Quaternion targetGuardRotation;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        currentStamina = maxStamina;

        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }

        // ★追加: ゲーム開始時にカーソルをロックして非表示にする
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // ★追加: Escapeキーでカーソルロックの状態を切り替える
        HandleCursorLock();
        
        HandleGuardInput();
        
        if (!isGuarding && Input.GetMouseButtonDown(0) && canShoot && targetBallRb != null)
        {
            Shoot();
        }
    }

    // ★追加: カーソルロックを管理する新しいメソッド
    void HandleCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                // ロックを解除してカーソルを表示
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                // カーソルをロックして非表示
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    // ...（FixedUpdateやその他のメソッドは変更なし）...


    void FixedUpdate()
    {
        if (isGuarding)
        {
            rb.linearVelocity = Vector3.zero;
            rb.rotation = Quaternion.Lerp(rb.rotation, targetGuardRotation, guardRotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            HandleMovement();
        }

        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }
    }
    
    void HandleGuardInput()
    {
        if (Input.GetMouseButtonDown(1) && !isGuarding)
        {
            isGuarding = true;
            targetGuardRotation = rb.rotation * Quaternion.Euler(0, 90, 0);
        }
        else if (Input.GetMouseButtonUp(1) && isGuarding)
        {
            isGuarding = false;
        }
    }

    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = mainCamera.transform.forward * moveVertical + mainCamera.transform.right * moveHorizontal;
        movement.y = 0f;

        bool isMoving = movement.magnitude > 0.1f;
        movement.Normalize();

        bool isDashing = Input.GetKey(KeyCode.LeftShift) && isMoving && currentStamina > 0;

        float currentSpeed;
        if (isDashing)
        {
            currentSpeed = dashSpeed;
            currentStamina = Mathf.Max(0, currentStamina - dashCost * Time.fixedDeltaTime);
        }
        else
        {
            currentSpeed = moveSpeed;
            if (currentStamina < maxStamina)
            {
                currentStamina = Mathf.Min(maxStamina, currentStamina + staminaRegenRate * Time.fixedDeltaTime);
            }
        }
        
        rb.linearVelocity = movement * currentSpeed;

        Quaternion targetRotation;
        if (isMoving)
        {
            targetRotation = Quaternion.LookRotation(movement);
        }
        else
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0;
            targetRotation = Quaternion.LookRotation(cameraForward);
        }
        
        rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    void Shoot()
    {
        Vector3 forceDirection = transform.forward;
        targetBallRb.AddForce(forceDirection * shootForce, ForceMode.Impulse);
        StartCoroutine(ShootCooldown());
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (shootTrigger != null && other.CompareTag("Ball"))
        {
            if (other.bounds.Intersects(shootTrigger.bounds))
            {
                targetBallRb = other.GetComponent<Rigidbody>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (shootTrigger != null && other.CompareTag("Ball"))
        {
            if (targetBallRb != null && targetBallRb.gameObject == other.gameObject)
            {
                targetBallRb = null;
            }
        }
    }
}*/
/*
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveFish : MonoBehaviour
{
    // --- フラグ ---
    private bool isControllable = true; // プレイヤーが操作可能かどうかのフラグ

    // --- 基本的な移動とカメラ ---
    private Camera mainCamera;
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    private Rigidbody rb;
    public float rotationSpeed = 5f;

    // --- スタミナ関連 ---
    public float maxStamina = 100f;
    private float currentStamina;
    public float dashCost = 25f;
    public float staminaRegenRate = 15f;
    public Slider staminaSlider;

    // --- シュート機能関連 ---
    public float shootForce = 15f;
    public float shootCooldown = 1.0f;
    public Collider shootTrigger;
    private bool canShoot = true;
    private Rigidbody targetBallRb;

    // --- ガード機能関連 ---
    private bool isGuarding = false;
    public float guardRotationSpeed = 10f;
    private Quaternion targetGuardRotation;
    
    // --- 衝突による停止設定 ---
    [Header("衝突による停止設定")]
    public float freezeDuration = 2f; // 障害物に当たった時に動けなくなる時間（秒）

    /// <summary>
    /// 外部から操作の可否を設定します。
    /// </summary>
    /// <param name="controllable">trueなら操作可能、falseなら操作不能</param>
    public void SetControllable(bool controllable)
    {
        this.isControllable = controllable;

        // 操作不能になった場合、物理的な動きを即座に停止させます。
        if (!controllable && rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        currentStamina = maxStamina;

        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 操作不能な場合は、この先の処理をすべて中断します。
        if (!isControllable) return;

        HandleCursorLock();
        HandleGuardInput();

        if (!isGuarding && Input.GetMouseButtonDown(0) && canShoot && targetBallRb != null)
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        // 操作不能な場合は、この先の処理をすべて中断します。
        if (!isControllable) return;

        if (isGuarding)
        {
            rb.linearVelocity = Vector3.zero;
            rb.rotation = Quaternion.Lerp(rb.rotation, targetGuardRotation, guardRotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            HandleMovement();
        }

        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }
    }

    void HandleCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void HandleGuardInput()
    {
        if (Input.GetMouseButtonDown(1) && !isGuarding)
        {
            isGuarding = true;
            targetGuardRotation = rb.rotation * Quaternion.Euler(0, 90, 0);
        }
        else if (Input.GetMouseButtonUp(1) && isGuarding)
        {
            isGuarding = false;
        }
    }

    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = mainCamera.transform.forward * moveVertical + mainCamera.transform.right * moveHorizontal;
        movement.y = 0f;

        bool isMoving = movement.magnitude > 0.1f;
        movement.Normalize();

        bool isDashing = Input.GetKey(KeyCode.LeftShift) && isMoving && currentStamina > 0;

        float currentSpeed;
        if (isDashing)
        {
            currentSpeed = dashSpeed;
            currentStamina = Mathf.Max(0, currentStamina - dashCost * Time.fixedDeltaTime);
        }
        else
        {
            currentSpeed = moveSpeed;
            if (currentStamina < maxStamina)
            {
                currentStamina = Mathf.Min(maxStamina, currentStamina + staminaRegenRate * Time.fixedDeltaTime);
            }
        }

        rb.linearVelocity = movement * currentSpeed;

        Quaternion targetRotation;
        if (isMoving)
        {
            targetRotation = Quaternion.LookRotation(movement);
        }
        else
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0;
            targetRotation = Quaternion.LookRotation(cameraForward);
        }

        rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    void Shoot()
    {
        Vector3 forceDirection = transform.forward;
        targetBallRb.AddForce(forceDirection * shootForce, ForceMode.Impulse);
        StartCoroutine(ShootCooldown());
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (shootTrigger != null && other.CompareTag("Ball"))
        {
            if (other.bounds.Intersects(shootTrigger.bounds))
            {
                targetBallRb = other.GetComponent<Rigidbody>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (shootTrigger != null && other.CompareTag("Ball"))
        {
            if (targetBallRb != null && targetBallRb.gameObject == other.gameObject)
            {
                targetBallRb = null;
            }
        }
    }
    
    // 他のオブジェクトのコライダーと接触した時に呼び出されるメソッド
    private void OnCollisionEnter(Collision collision)
    {
        // 衝突した相手のタグが "Obstacle" で、かつ現在操作可能な場合
        if (collision.gameObject.CompareTag("Obstacle") && isControllable)
        {
            Destroy(collision.gameObject);

            // プレイヤーを一時的に停止させるコルーチンを開始します。
            StartCoroutine(FreezePlayerTemporarily());
        }
    }

    // プレイヤーを一時的に停止させるためのコルーチン
    private IEnumerator FreezePlayerTemporarily()
    {
        Debug.Log("障害物に衝突！ " + freezeDuration + "秒間停止します。");

        // 既存のメソッドを利用してプレイヤーを操作不能にします。
        SetControllable(false);

        // freezeDurationで設定した秒数だけ処理を待機します。
        yield return new WaitForSeconds(freezeDuration);

        Debug.Log("操作を再開します。");

        // 再びプレイヤーを操作可能にします。
        SetControllable(true);
    }
}*/
/*
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveFish : MonoBehaviour
{
    // --- フラグ ---
    private bool isControllable = true;

    // --- 基本的な移動とカメラ ---
    private Camera mainCamera;
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    private Rigidbody rb;
    public float rotationSpeed = 5f; // 回転補間の速さ
    
    // ★調整: 操作設定
    [Header("操作設定")]
    public float mouseYSpeedFactor = 10f; // マウスY軸を前進速度に変換する係数
    public float wasdRotationSpeed = 80f; // WASDキーでの回転速度 (度/秒)
    public float mouseInputDeadZone = 0.05f; // これ以下のマウス入力は無視

    // --- スタミナ関連 ---
    public float maxStamina = 100f;
    private float currentStamina;
    public float dashCost = 25f;
    public float staminaRegenRate = 15f;
    public Slider staminaSlider;

    // --- シュート機能関連 ---
    public float shootForce = 15f;
    public float shootCooldown = 1.0f;
    public Collider shootTrigger;
    private bool canShoot = true;
    private Rigidbody targetBallRb;

    // --- ガード機能関連 ---
    private bool isGuarding = false;
    public float guardRotationSpeed = 10f;
    private Quaternion targetGuardRotation;
    
    // --- 衝突による停止設定 ---
    [Header("衝突による停止設定")]
    public float freezeDuration = 2f;

    /// <summary>
    /// 外部から操作の可否を設定します。
    /// </summary>
    public void SetControllable(bool controllable)
    {
        this.isControllable = controllable;

        if (!controllable && rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        currentStamina = maxStamina;

        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!isControllable) return;

        HandleCursorLock();
        HandleGuardInput();

        if (!isGuarding && Input.GetMouseButtonDown(0) && canShoot && targetBallRb != null)
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (!isControllable) return;

        if (isGuarding)
        {
            rb.linearVelocity = Vector3.zero;
            rb.rotation = Quaternion.Lerp(rb.rotation, targetGuardRotation, guardRotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            HandleMovement();
        }

        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }
    }
    
    void HandleCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void HandleGuardInput()
    {
        if (Input.GetMouseButtonDown(1) && !isGuarding)
        {
            isGuarding = true;
            targetGuardRotation = rb.rotation * Quaternion.Euler(0, 90, 0);
        }
        else if (Input.GetMouseButtonUp(1) && isGuarding)
        {
            isGuarding = false;
        }
    }

    void HandleMovement()
    {
        // ----------------------------------------------------
        // ★ WASDキーの左右入力でプレイヤーを回転させる
        // ----------------------------------------------------
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(moveHorizontal) > 0.1f)
        {
            // Y軸周りの回転角度を計算 (度/秒 * 経過時間)
            float rotationAngle = moveHorizontal * wasdRotationSpeed * Time.fixedDeltaTime;
            
            // 現在の回転に加算
            Quaternion targetRotation = rb.rotation * Quaternion.Euler(0f, rotationAngle, 0f);
            
            // Lerpでスムーズに回転
            rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime * 10f);
        }
        
        // ----------------------------------------------------
        // ★ マウスのY軸の動きを前進速度として取得する
        // ----------------------------------------------------
        float mouseMoveY = Input.GetAxis("Mouse Y");
        
        // デッドゾーンを適用
        if (Mathf.Abs(mouseMoveY) < mouseInputDeadZone)
        {
            mouseMoveY = 0f;
        }
        
        // マウスの動きの絶対値（速度を決定するための強度）
        float movementIntensity = Mathf.Abs(mouseMoveY);
        
        // ダッシュ判定 (スペースキーでのダッシュを採用)
        bool isDashing = Input.GetKey(KeyCode.Space) && currentStamina > 0 && movementIntensity > 0;

        float targetSpeed;
        if (isDashing)
        {
            // ダッシュ速度 + マウスによる加速
            targetSpeed = dashSpeed + movementIntensity * mouseYSpeedFactor; 
            currentStamina = Mathf.Max(0, currentStamina - dashCost * Time.fixedDeltaTime);
        }
        else
        {
            // 基本速度 + マウスによる加速
            // マウス入力がない場合(movementIntensity=0)は、targetSpeedも0になる
            targetSpeed = moveSpeed * movementIntensity + (movementIntensity > 0 ? moveSpeed : 0f);

            // スタミナ回復
            if (currentStamina < maxStamina)
            {
                currentStamina = Mathf.Min(maxStamina, currentStamina + staminaRegenRate * Time.fixedDeltaTime);
            }
        }
        
        // ----------------------------------------------------
        // ★ Rigidbodyの速度制御 (常に前方に移動、入力がない場合は停止)
        // ----------------------------------------------------
        Vector3 targetVelocity;
        
        if (movementIntensity > 0)
        {
            // マウス入力がある場合、前方に移動
            Vector3 movement = transform.forward;
            movement.y = 0f;
            targetVelocity = movement.normalized * targetSpeed;
        }
        else
        {
            // マウス入力がない場合、停止
            targetVelocity = Vector3.zero;
        }
        
        // 速度を目標速度へスムーズに補間する
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, Time.fixedDeltaTime * 10f);
    }

    void Shoot()
    {
        Vector3 forceDirection = transform.forward;
        targetBallRb.AddForce(forceDirection * shootForce, ForceMode.Impulse);
        StartCoroutine(ShootCooldown());
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (shootTrigger != null && other.CompareTag("Ball"))
        {
            if (other.bounds.Intersects(shootTrigger.bounds))
            {
                targetBallRb = other.GetComponent<Rigidbody>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (shootTrigger != null && other.CompareTag("Ball"))
        {
            if (targetBallRb != null && targetBallRb.gameObject == other.gameObject)
            {
                targetBallRb = null;
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && isControllable)
        {
            Destroy(collision.gameObject);
            StartCoroutine(FreezePlayerTemporarily());
        }
    }

    private IEnumerator FreezePlayerTemporarily()
    {
        Debug.Log("障害物に衝突！ " + freezeDuration + "秒間停止します。");
        SetControllable(false);
        yield return new WaitForSeconds(freezeDuration);
        Debug.Log("操作を再開します。");
        SetControllable(true);
    }
}*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveFish : MonoBehaviour
{
    // --- フラグ ---
    private bool isControllable = true;

    // --- 基本的な移動とカメラ ---
    private Camera mainCamera;
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    private Rigidbody rb;
    public float rotationSpeed = 5f; // カメラの向きに追従する回転補間の速さ
    
    // ★調整: 操作設定
    [Header("操作設定")]
    // mouseYSpeedFactor の名前はそのままで、X軸の加速に使います
    public float mouseYSpeedFactor = 10f; 
    public float mouseInputDeadZone = 0.05f; // これ以下のマウス入力は無視

    // --- スタミナ関連 ---
    public float maxStamina = 100f;
    private float currentStamina;
    public float dashCost = 25f;
    public float staminaRegenRate = 15f;
    public Slider staminaSlider;

    // --- シュート機能関連 ---
    public float shootForce = 15f;
    public float shootCooldown = 1.0f;
    public Collider shootTrigger;
    private bool canShoot = true;
    private Rigidbody targetBallRb;

    // --- ガード機能関連 ---
    private bool isGuarding = false;
    public float guardRotationSpeed = 10f;
    private Quaternion targetGuardRotation;
    
    // --- 衝突による停止設定 ---
    [Header("衝突による停止設定")]
    public float freezeDuration = 2f;

    /// <summary>
    /// 外部から操作の可否を設定します。
    /// </summary>
    public void SetControllable(bool controllable)
    {
        this.isControllable = controllable;

        if (!controllable && rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        currentStamina = maxStamina;

        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!isControllable) return;

        HandleCursorLock();
        HandleGuardInput();

        if (!isGuarding && Input.GetMouseButtonDown(0) && canShoot && targetBallRb != null)
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (!isControllable) return;

        if (isGuarding)
        {
            rb.linearVelocity = Vector3.zero;
            rb.rotation = Quaternion.Lerp(rb.rotation, targetGuardRotation, guardRotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            HandleMovement();
        }

        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }
    }
    
    void HandleCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void HandleGuardInput()
    {
        if (Input.GetMouseButtonDown(1) && !isGuarding)
        {
            isGuarding = true;
            targetGuardRotation = rb.rotation * Quaternion.Euler(0, 90, 0);
        }
        else if (Input.GetMouseButtonUp(1) && isGuarding)
        {
            isGuarding = false;
        }
    }

    void HandleMovement()
    {
        // ----------------------------------------------------
        // 1. 魚の向きをカメラの正面向きに合わせる
        // ----------------------------------------------------
        
        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0f;

        if (cameraForward.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward.normalized);
            rb.rotation = Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        
        // ----------------------------------------------------
        // 2. ★修正: マウスのX軸の動きを前進速度として取得する
        // ----------------------------------------------------
        // ★修正: Mouse Y -> Mouse X
        float mouseMoveX = Input.GetAxis("Mouse X");
        
        // デッドゾーンを適用
        // ★修正: Mouse Y の代わりに Mouse X を使用
        if (Mathf.Abs(mouseMoveX) < mouseInputDeadZone)
        {
            mouseMoveX = 0f;
        }
        
        // マウスの動きの絶対値（速度を決定するための強度）
        // ★修正: Mouse Y の代わりに Mouse X を使用
        float movementIntensity = Mathf.Abs(mouseMoveX);
        
        // ダッシュ判定 (LeftShiftキーを押しながらマウス入力がある場合にダッシュ)
        bool isDashing = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && movementIntensity > 0;

        float targetSpeed;
        if (isDashing)
        {
            targetSpeed = dashSpeed + movementIntensity * mouseYSpeedFactor; 
            currentStamina = Mathf.Max(0, currentStamina - dashCost * Time.fixedDeltaTime);
        }
        else
        {
            // マウス入力がない場合(movementIntensity=0)は、targetSpeedも0になる
            targetSpeed = moveSpeed * movementIntensity + (movementIntensity > 0 ? moveSpeed : 0f);

            // スタミナ回復
            if (currentStamina < maxStamina)
            {
                currentStamina = Mathf.Min(maxStamina, currentStamina + staminaRegenRate * Time.fixedDeltaTime);
            }
        }
        
        // ----------------------------------------------------
        // 3. Rigidbodyの速度制御
        // ----------------------------------------------------
        Vector3 targetVelocity;
        
        if (movementIntensity > 0)
        {
            // マウス入力がある場合、魚の正面（transform.forward）に移動
            Vector3 movement = transform.forward;
            movement.y = 0f;
            targetVelocity = movement.normalized * targetSpeed;
        }
        else
        {
            // マウス入力がない場合、停止
            targetVelocity = Vector3.zero;
        }
        
        // 速度を目標速度へスムーズに補間する
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, Time.fixedDeltaTime * 10f);
    }

    void Shoot()
    {
        Vector3 forceDirection = transform.forward;
        targetBallRb.AddForce(forceDirection * shootForce, ForceMode.Impulse);
        StartCoroutine(ShootCooldown());
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (shootTrigger != null && other.CompareTag("Ball"))
        {
            if (other.bounds.Intersects(shootTrigger.bounds))
            {
                targetBallRb = other.GetComponent<Rigidbody>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (shootTrigger != null && other.CompareTag("Ball"))
        {
            if (targetBallRb != null && targetBallRb.gameObject == other.gameObject)
            {
                targetBallRb = null;
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && isControllable)
        {
            Destroy(collision.gameObject);
            StartCoroutine(FreezePlayerTemporarily());
        }
    }

    private IEnumerator FreezePlayerTemporarily()
    {
        Debug.Log("障害物に衝突！ " + freezeDuration + "秒間停止します。");
        SetControllable(false);
        yield return new WaitForSeconds(freezeDuration);
        Debug.Log("操作を再開します。");
        SetControllable(true);
    }
}