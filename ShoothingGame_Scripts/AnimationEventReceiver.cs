using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    // アニメーションイベント 'EndOfWalk' を受け取るメソッド
    public void EndOfWalk()
    {
        Debug.Log("Walk animation has ended.");
        // 必要な処理をここに記述（例: アニメーション終了後の状態遷移など）
    }
}
