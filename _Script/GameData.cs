using UnityEngine;

// staticクラスなので、GameObjectにアタッチする必要はありません
public static class GameData
{
    // プレイヤーチームの最終得点
    public static int finalScoreTeam1 = 0; 
    
    // 敵チームの最終得点
    public static int finalScoreTeam2 = 0;

    // シーン移動時に得点をリセットしたい場合は、リセットメソッドを追加することもできます
    public static void ResetScores()
    {
        finalScoreTeam1 = 0;
        finalScoreTeam2 = 0;
    }
}