using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private bool hasMoved = false; // 移動が行われたかどうか
    private float lastMoveTime = -1f; // 最後に移動した時間を記録
    private float moveThreshold = 0.1f; // 移動とみなす閾値
    private Vector2 startTouchPosition; // タッチの開始位置

    private Animator animator; // Animatorの参照

    void Start()
    {
        // Animatorコンポーネントの取得
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // タッチ入力をチェック
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position; // タッチの開始位置を記録
                    break;

                case TouchPhase.Ended:
                    Vector2 endTouchPosition = touch.position; // タッチの終了位置
                    Vector2 swipeDelta = endTouchPosition - startTouchPosition; // スワイプの距離

                    // スワイプの閾値を超えた場合、移動を記録
                    if (swipeDelta.magnitude > moveThreshold)
                    {
                        hasMoved = true;
                        lastMoveTime = Time.time; // 移動した時間を記録
                    }
                    else
                    {
                        hasMoved = false; // スワイプが閾値を超えない場合は移動していないとする
                    }
                    break;
            }
        }
        else
        {
            hasMoved = false; // タッチがない場合、移動していないとする
        }
    }

    // 指定した回避ウィンドウ内に移動があったかをチェック
    public bool HasMovedWithin(float window)
    {
        return hasMoved && Time.time - lastMoveTime <= window;
    }

    public void TakeDamage(float damage)
    {
        // ダメージ処理
        Debug.Log("Player takes " + damage + " damage.");

        // ダメージを食らったアニメーションを再生
        if (animator != null)
        {
            animator.SetTrigger("TakeDamage"); // "TakeDamage" というトリガーをアニメーションコントローラーで設定する必要があります
        }
    }
}