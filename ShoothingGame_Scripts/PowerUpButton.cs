using UnityEngine;
using UnityEngine.UI; // UIのボタンを扱うために必要

public class PowerUpButton : MonoBehaviour
{
    public int requiredEvades = 3; // 必要な回避回数
    public int boostedShotCount = 5; // 強化弾の発射回数
    public int damageMultiplier = 2; // 強化時のダメージ倍率

    private int evadeSuccessCount = 0; // 回避成功回数
    private int remainingBoostedShots = 0; // 残りの強化弾数

    // UIボタンの参照
    public Button powerUpButton;  // UIのButton型を参照


    void Start()
    {
        // ボタンのクリックイベントにメソッドを追加
        if (powerUpButton != null)
        {
            powerUpButton.onClick.AddListener(ActivatePowerUp);
        }
    }

    // 回避成功時に呼ばれるメソッド
    public void OnEvadeSuccess()
    {
        evadeSuccessCount++;

        if (evadeSuccessCount >= requiredEvades)
        {
            evadeSuccessCount = 0; // カウントをリセット
            ActivatePowerUp(); // 強化弾の発動
        }
    }

    // 強化弾発動メソッド（ボタンを押したときに呼ばれる）
    private void ActivatePowerUp()
    {
        if (remainingBoostedShots <= 0) // 強化弾が残っていない場合に発動
        {
            remainingBoostedShots = boostedShotCount;
            Debug.Log("強化弾が使用可能になりました！");
        }
    }

    // 弾丸ダメージを取得するメソッド
    public int GetBulletDamage(int baseDamage)
    {
        if (remainingBoostedShots > 0)
        {
            remainingBoostedShots--;
            return baseDamage * damageMultiplier; // 強化されたダメージを返す
        }
        return baseDamage; // 通常のダメージを返す
    }
}
