
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public Text countdownText;
    public float countdownTime = 180f;
    public string nextSceneName; 
    
  
    public CollisionCounter scoreCounter; 

    private bool isCountingDown = false;

    public bool IsGameActive()
    {
        return isCountingDown;
    }

    public bool IsFinished()
    {
        return countdownTime <= 0f;
    }

    private void Start()
    {
        StartCoroutine(PreCountdownAndStartTimer());
    }

    IEnumerator PreCountdownAndStartTimer()
    {
        // 事前カウントダウン
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "START!";
        yield return new WaitForSeconds(1f);

        // タイマー開始
        isCountingDown = true;

        while (countdownTime > 0f)
        {
            countdownTime -= Time.deltaTime;
            UpdateTimerText();
            yield return null;
        }

        countdownTime = 0f;
        UpdateTimerText();
        countdownText.text = "FINISH!"; 

      
        
       
        if (BGMAudioPlayer.Instance != null)
        {
            
            Destroy(BGMAudioPlayer.Instance.gameObject);
        }

        yield return new WaitForSeconds(2f); 

       
        if (scoreCounter != null)
        {
            
             GameData.finalScoreTeam1 = scoreCounter.scoreTeam1;
             GameData.finalScoreTeam2 = scoreCounter.scoreTeam2;
        }
        
       
        SceneManager.LoadScene(nextSceneName);
       
    }

    void UpdateTimerText()
    {
        if (isCountingDown)
        {
            int minutes = Mathf.FloorToInt(countdownTime / 60f);
            int seconds = Mathf.FloorToInt(countdownTime % 60f);
            countdownText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }
}