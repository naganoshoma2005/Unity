using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int level = 1;
    public int experience = 0;
    public int experienceToNextLevel = 100;
    public int attackPower = 20; // 初期攻撃力
    public int attackPowerIncrease = 5; // レベルアップごとの攻撃力増加

    public void GainExperience(int amount)
    {
        experience += amount;
        Debug.Log($"経験値獲得: {amount}, 現在の経験値: {experience}/{experienceToNextLevel}");
        GameLogManager.instance.AddLog($"経験値獲得: {amount}, 現在の経験値: {experience}/{experienceToNextLevel}");

        while (experience >= experienceToNextLevel) // **経験値が次のレベルに必要な値を超えている間、レベルアップを繰り返す**
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        experience -= experienceToNextLevel; // 余剰経験値を次のレベルへ繰り越し
        level++;
        attackPower += attackPowerIncrease;
        experienceToNextLevel = Mathf.RoundToInt(experienceToNextLevel * 1.3f); // 必要経験値を増加
        Debug.Log($"レベルアップ！現在のレベル: {level}, 新しい攻撃力: {attackPower}");
        // **レベルアップログを表示**
        GameLogManager.instance.AddLog($"レベル {level} にアップ！");
    }
}
