
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform cameraRotation;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform attackPoint;

    public float attackRange = 2f;
    public float attackDistance = 5f;
    public float attackAngle = 45f;
    public LayerMask enemyLayer;
    public LayerMask destructibleLayer;

    public float moveSpeed;
    public float dashSpeed;
    public float dodgeSpeed = 5f;
    public float jumpPower;
    public float dodgeCooldown = 1f;
    public float attackCooldown = 0.5f;
    public bool isJumping = false;  // 地面にいない時のみ true

    private Rigidbody rb;
    private bool canDodge = true;
    private bool canAttack = true;
    private float lastDodgeTime;
    private float lastAttackTime;
    private bool isGuarding = false;

    private bool ignoreAttackInput = false;
    private bool ignoreGuardInput = false;

    private PlayerStats playerStats;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>(); // PlayerStats を取得
    }

    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (!ignoreAttackInput && Input.GetMouseButtonDown(0) && CanPerformAttack())
        {
            PerformAttack();
        }

        if (!ignoreGuardInput)
        {
            if (Input.GetMouseButtonDown(1)) // 右クリック
            {
                StartGuard();
            }
            else if (Input.GetMouseButtonUp(1)) // 右クリックを離した時
            {
                StopGuard();
            }
        }

        ManageAttackCooldown();
        ManageDodgeCooldown();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = cameraRotation.transform.forward;
        forward.y = 0f;
        forward.Normalize();
        Vector3 right = cameraRotation.transform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 moveDirection = forward * vertical + right * horizontal;

        if (Input.GetKey(KeyCode.Q) && canDodge)
        {
            PerformDodge(moveDirection);
            return;
        }

        float currentMoveSpeed = Input.GetKey(KeyCode.LeftShift) ? dashSpeed : moveSpeed;

        if (isGuarding)
        {
            currentMoveSpeed *= 0.5f;
        }

        Vector3 targetPosition = rb.position + moveDirection * currentMoveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);

        float speedBlendValue = moveDirection.magnitude * (Input.GetKey(KeyCode.LeftShift) ? 2f : 1f);
        playerAnimator.SetFloat("Speed", speedBlendValue);

        RotatePlayerToCameraDirection();
    }

    private void Jump()
    {
        if (!isJumping) // 地面にいるときのみジャンプ可能
        {
            isJumping = true;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpPower, rb.linearVelocity.z);
            playerAnimator.SetTrigger("Jump");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false; // 地面に着地したらジャンプ可能にする
        }
    }

    private bool CanPerformAttack()
    {
        return canAttack && Time.time >= lastAttackTime + attackCooldown;
    }

    private void PerformAttack()
    {
        if (playerStats == null) return;

        canAttack = false;
        lastAttackTime = Time.time;

        playerAnimator.SetTrigger("Attack");

        Vector3 attackDirection = transform.forward;
        int combinedLayerMask = enemyLayer | destructibleLayer;

        RaycastHit[] hits = Physics.SphereCastAll(
            attackPoint.position,
            attackRange / 2f,
            attackDirection,
            attackDistance,
            combinedLayerMask
        );

        foreach (RaycastHit hit in hits)
        {
            Vector3 directionToTarget = (hit.collider.transform.position - attackPoint.position).normalized;
            float angleToTarget = Vector3.Angle(attackDirection, directionToTarget);

            if (angleToTarget <= attackAngle)
            {
                if ((enemyLayer.value & (1 << hit.collider.gameObject.layer)) != 0)
                {
                    EnemyHP enemyHP = hit.collider.GetComponent<EnemyHP>();
                    if (enemyHP != null)
                    {
                        enemyHP.TakeDamage(playerStats.attackPower, transform);
                        Debug.Log($"敵 {enemyHP.gameObject.name} に {playerStats.attackPower} のダメージを与えた！");
                    }
                }

                if ((destructibleLayer.value & (1 << hit.collider.gameObject.layer)) != 0)
                {
                    DestructibleObject destructibleObject = hit.collider.GetComponent<DestructibleObject>();
                    if (destructibleObject != null)
                    {
                        destructibleObject.TakeDamage(playerStats.attackPower);
                        Debug.Log($"{destructibleObject.objectName} を攻撃！ 残り耐久度: {destructibleObject.currentDurability}");
                    }
                }
            }
        }
    }

    private void ManageAttackCooldown()
    {
        if (!canAttack && Time.time >= lastAttackTime + attackCooldown)
        {
            canAttack = true;
        }
    }

    private void ManageDodgeCooldown()
    {
        if (!canDodge && Time.time >= lastDodgeTime + dodgeCooldown)
        {
            canDodge = true;
        }
    }

    private void PerformDodge(Vector3 moveDirection)
    {
        canDodge = false;
        lastDodgeTime = Time.time;

        playerAnimator.SetTrigger("Dodge");

        if (moveDirection == Vector3.zero)
        {
            moveDirection = -cameraRotation.forward;
        }

        Vector3 dodgeVector = moveDirection.normalized * dodgeSpeed;
        rb.AddForce(dodgeVector, ForceMode.Impulse);
    }

    private void RotatePlayerToCameraDirection()
    {
        Vector3 cameraForward = cameraRotation.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        if (cameraForward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }

    private void StartGuard()
    {
        isGuarding = true;
        playerAnimator.SetBool("IsGuarding", true);
    }

    private void StopGuard()
    {
        isGuarding = false;
        playerAnimator.SetBool("IsGuarding", false);
    }

    public void SetBuildMode(bool buildModeActive)
    {
        if (buildModeActive)
        {
            canAttack = false;
            isGuarding = false;
            playerAnimator.SetBool("IsGuarding", false);
            ignoreAttackInput = true;
            ignoreGuardInput = true;
        }
        else
        {
            canAttack = true;
            ignoreAttackInput = false;
            ignoreGuardInput = false;
        }
    }
}
