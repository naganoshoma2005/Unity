using UnityEngine;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{
    
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject drawPanel;

    public AudioSource audioSource;

    public AudioClip winSE;
    public AudioClip loseSE;
    public AudioClip drawSE;

    void Start()
    {
      
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
      
        SetAllPanelsInactive();

        int finalTeam1Score = GameData.finalScoreTeam1; 
        int finalTeam2Score = GameData.finalScoreTeam2;

      
        DetermineAndDisplayResult(finalTeam1Score, finalTeam2Score);
    }

   
    private void SetAllPanelsInactive()
    {
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
        if (drawPanel != null) drawPanel.SetActive(false);
    }

    
    private void DetermineAndDisplayResult(int team1Score, int team2Score)
    {
        if (team1Score > team2Score)
        {
            // 勝利 (Win)
            if (winPanel != null)
            {
                winPanel.SetActive(true);
            }
            if(audioSource != null && winSE !=null)
            {
                audioSource.PlayOneShot(winSE);
            }
        }
        else if (team1Score < team2Score)
        {
            // 敗北 (Lose)
            if (losePanel != null)
            {
                losePanel.SetActive(true);
            }
            if(audioSource != null  && winSE != null)
            {
                audioSource.PlayOneShot(loseSE);
            }
        }
        else
        {
            // 引き分け (Draw)
            if (drawPanel != null)
            {
                drawPanel.SetActive(true);
            }
            if(audioSource != null && drawSE != null)
            {
                audioSource.PlayOneShot(drawSE);
            }
        }
    }
}