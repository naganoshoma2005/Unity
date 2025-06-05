using UnityEngine;
using UnityEngine.UI; // Text UI用


public class PlayerPowerUI : MonoBehaviour
{
    public PlayerStats playerStats; // PlayerStatsの参照
    public Text attackPowerText; // 通常のUI Text用
    // public TextMeshProUGUI attackPowerText; // TextMeshProを使う場合

    void Start()
    {
        UpdateAttackPowerUI(); // 初期表示
    }

    void Update()
    {
        UpdateAttackPowerUI(); // 毎フレーム更新（最適化したければイベント駆動に変更）
    }

    void UpdateAttackPowerUI()
    {
        if (attackPowerText != null && playerStats != null)
        {
            attackPowerText.text = $"{playerStats.attackPower}";
        }
    }
}
