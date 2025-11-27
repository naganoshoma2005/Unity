/*
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public Text countdownText;
    public float countdownTime = 180f;
    public string nextSceneName;

    private bool isCountingDown = false;

    /// <summary>
    /// ゲームの本番カウントダウンが始まったかどうかを返します。
    /// </summary>
    public bool IsGameActive()
    {
        return isCountingDown;
    }

    /// <summary>
    /// ゲームが終了したかどうか（時間が0以下になったか）を返します。
    /// </summary>
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
        // 事前カウント（3 → 2 → 1 → スタート！）
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
        countdownText.text = "FINISH!"; // 終了メッセージ

        // --- ここからが追加部分 ---
        yield return new WaitForSeconds(2f); // 2秒待ってからシーン移動（FINISH!の表示を少し見せるため）

        // nextSceneNameに設定されたシーンをロードする
        SceneManager.LoadScene(nextSceneName);
        // --- ここまでが追加部分 ---
    }

    void UpdateTimerText()
    {
        if (isCountingDown) // isCountingDownがtrueの時だけタイマー表示を更新
        {
            int minutes = Mathf.FloorToInt(countdownTime / 60f);
            int seconds = Mathf.FloorToInt(countdownTime % 60f);
            countdownText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }
}*/
/*
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public Text countdownText;
    public float countdownTime = 180f;
    public string nextSceneName; // リザルトシーンの名称
    
    // CollisionCounterスクリプトへの参照 (インスペクターで設定)
    public CollisionCounter scoreCounter; 

    private bool isCountingDown = false;

    // GameManagerがゲーム開始を判定するために使用
    public bool IsGameActive()
    {
        return isCountingDown;
    }

    // GameManagerがゲーム終了を判定するために使用
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
        // 事前カウントダウン（3 → 2 → 1 → START!）
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
        countdownText.text = "FINISH!"; // 終了メッセージ

        yield return new WaitForSeconds(2f); // 2秒待ってからシーン移動

        // --- 時間切れ時の処理 ---
        if (scoreCounter != null)
        {
            // GameDataに最終得点を保存
            GameData.finalScoreTeam1 = scoreCounter.scoreTeam1;
            GameData.finalScoreTeam2 = scoreCounter.scoreTeam2;
        }
        
        // リザルトシーンへ移動
        SceneManager.LoadScene(nextSceneName);
        // --- ここまで ---
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
}*/
/*
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public Text countdownText;
    public float countdownTime = 180f;
    public string nextSceneName; // リザルトシーンの名称
    
    // CollisionCounterへの参照 (インスペクターで設定してください)
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
        countdownText.text = "FINISH!"; // 終了メッセージ

        yield return new WaitForSeconds(2f); // 2秒待ってからシーン移動

        // --- 時間切れ時の最終処理 ---
        if (scoreCounter != null)
        {
            // GameDataに最終得点を保存
            GameData.finalScoreTeam1 = scoreCounter.scoreTeam1;
            GameData.finalScoreTeam2 = scoreCounter.scoreTeam2;
        }
        
        // リザルトシーンへ移動
        SceneManager.LoadScene(nextSceneName);
        // --- ここまで ---
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
}*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public Text countdownText;
    public float countdownTime = 180f;
    public string nextSceneName; // リザルトシーンの名称
    
    // CollisionCounterへの参照 (インスペクターで設定してください)
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
        countdownText.text = "FINISH!"; // 終了メッセージ

        // --- BGM停止と最終処理 ---
        
        // ★修正点 1: 試合終了時にBGMを停止
        if (BGMAudioPlayer.Instance != null)
        {
            // BGMAudioPlayerにAudioSourceを停止する公開メソッドを追加するのが理想的ですが、
            // 今回はBGMAudioPlayerのGameObjectごと破棄し、永続化を解除します。
            Destroy(BGMAudioPlayer.Instance.gameObject);
        }

        yield return new WaitForSeconds(2f); // 2秒待ってからシーン移動

        // 時間切れ時の最終処理
        if (scoreCounter != null)
        {
            // GameDataに最終得点を保存
            // GameDataクラスが別途必要です
             GameData.finalScoreTeam1 = scoreCounter.scoreTeam1;
             GameData.finalScoreTeam2 = scoreCounter.scoreTeam2;
        }
        
        // リザルトシーンへ移動
        SceneManager.LoadScene(nextSceneName);
        // --- ここまで ---
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