 using UnityEngine;
using UnityEngine.UI; // スタミナバーのUI要素に使用

public class StaminaSystem : MonoBehaviour
{
    public float maxStamina = 100f;  // スタミナの最大値
    public float currentStamina;     // 現在のスタミナ
    public float staminaDrainRate = 20f; // 移動中に1秒間に減少するスタミナ量
    public float staminaRecoveryRate = 5f; // 移動停止中に1秒間に回復するスタミナ量
    public Slider staminaSlider;     // スタミナバーへの参照
    private bool isMoving = false;   // プレイヤーが移動中かどうかを判定
    private bool canMove = true;     // プレイヤーが移動可能かどうかを判定

    void Start()
    {
        currentStamina = maxStamina; // スタミナを初期化
        staminaSlider.maxValue = maxStamina; // スライダーの最大値を設定
        staminaSlider.value = currentStamina; // スタミナバーに現在のスタミナを反映
    }

    void Update()
    {
        HandleMovement();

        if (isMoving && canMove)
        {
            // 移動中はスタミナを減少させる
            currentStamina -= staminaDrainRate * Time.deltaTime;
            if (currentStamina <= 0)
            {
                currentStamina = 0;
                canMove = false; // スタミナがゼロになると移動を禁止
            }
        }
        else
        {
            // 移動していない場合はスタミナを回復
            currentStamina += staminaRecoveryRate * Time.deltaTime;
            if (currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
                canMove = true; // スタミナが回復したら再び移動可能に
            }
        }

        // UIのスタミナバーを更新
        staminaSlider.value = currentStamina;
    }

    void HandleMovement()
    {
        // 移動の処理（ここは自分の移動システムに合わせて変更可能）
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if (moveX != 0 || moveZ != 0)
        {
            isMoving = true;  // プレイヤーが移動中
        }
        else
        {
            isMoving = false; // プレイヤーが停止中
        }

        if (canMove)
        {
            // 移動のロジック（ここで具体的な移動処理を追加）
            transform.Translate(new Vector3(moveX, 0, moveZ) * Time.deltaTime * 5f); // 移動速度5f
        }
    }
}

