using UnityEngine;

public class CameraTargetControl : MonoBehaviour
{
    public Transform targetA;
    public Transform targetB;
    public float rotationSpeed = 50f;
    public float distanceFromTarget = 5f;
    public Transform player;
    public float rotationDelayFactor = 0.1f;
    public float lookAtDelayFactor = 0.1f;
    public float additionalRotationAngle = 90f;

    private Vector3 targetBStartPos;
    private Quaternion targetARotation;
    private Quaternion playerLookRotation;
    private Animator playerAnimator;

    void Start()
    {
        targetBStartPos = targetB.localPosition;
        targetARotation = targetA.rotation;
        playerLookRotation = player.rotation;

        // プレイヤーのAnimatorを取得
        if (player != null)
        {
            playerAnimator = player.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (targetA == null || targetB == null || player == null)
        {
            Debug.LogWarning("TargetA, TargetB, or Player is null. Please check references.");

            // アニメーションを停止する
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("Clear", true);
            }
            return; // null の場合、処理を中断
        }

        float horizontal = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                horizontal = touch.deltaPosition.x * rotationSpeed * Time.deltaTime;
            }
        }

        // Aターゲットの回転
        targetARotation *= Quaternion.Euler(0, horizontal, 0);
        targetA.rotation = Quaternion.Slerp(targetA.rotation, targetARotation, rotationDelayFactor);

        // Bの位置を固定したままプレイヤーの移動アニメーションを制御
        player.position = Vector3.Lerp(player.position, targetB.position, 0.1f);

        // プレイヤーがAを向く
        Vector3 directionToLook = targetA.position - player.position;
        directionToLook.y = 0;
        playerLookRotation = Quaternion.LookRotation(directionToLook) * Quaternion.Euler(0, additionalRotationAngle, 0);
        player.rotation = Quaternion.Slerp(player.rotation, playerLookRotation, lookAtDelayFactor);

        // 横方向の移動があればアニメーションを再生
        bool isMoving = Mathf.Abs(horizontal) > 0.1f;
        playerAnimator.SetBool("isWalking", isMoving);
    }
}
