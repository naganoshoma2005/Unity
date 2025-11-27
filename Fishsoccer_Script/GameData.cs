using UnityEngine;

public static class GameData
{

    public static int finalScoreTeam1 = 0; 
    

    public static int finalScoreTeam2 = 0;

   
    public static void ResetScores()
    {
        finalScoreTeam1 = 0;
        finalScoreTeam2 = 0;
    }
}