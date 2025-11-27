using UnityEngine;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{
    // 勝敗結果を表示するためのUI要素 (Unityのインスペクターで設定)
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject drawPanel;

    public AudioSource audioSource;

    public AudioClip winSE;
    public AudioClip loseSE;
    public AudioClip drawSE;

    void Start()
    {
        // マウスカーソルの再表示とロック解除
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        // 最初にすべての結果パネルを非表示にする
        SetAllPanelsInactive();

        // GameDataクラスから得点を読み込む
        int finalTeam1Score = GameData.finalScoreTeam1; // プレイヤーチームのスコア
        int finalTeam2Score = GameData.finalScoreTeam2; // 敵チームのスコア

        // 勝敗の判定とUIの表示
        DetermineAndDisplayResult(finalTeam1Score, finalTeam2Score);
    }

    /// <summary>
    /// 全ての結果パネルを非表示に設定します。
    /// </summary>
    private void SetAllPanelsInactive()
    {
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
        if (drawPanel != null) drawPanel.SetActive(false);
    }

    /// <summary>
    /// スコアを比較し、勝敗を判定して対応するUIパネルを表示します。
    /// </summary>
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