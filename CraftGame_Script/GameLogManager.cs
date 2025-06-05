
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogManager : MonoBehaviour
{
    public Text logText;
    public GameObject logPanel; // ✅ ログパネルを管理
    public int maxLogCount = 5; // 最大表示行数
    private Queue<string> logQueue = new Queue<string>(); // ログのキュー
    private Queue<float> logTimers = new Queue<float>(); // 各ログの削除タイミングを管理
    private bool isRemovingLogs = false; // ログ削除処理が進行中かどうか

    public static GameLogManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 新しいログを追加し、一定時間後に削除処理を開始する
    /// </summary>
    public void AddLog(string message, float duration = 5f)
    {
        logQueue.Enqueue(message);
        logTimers.Enqueue(Time.time + duration); // 削除タイミングを記録

        // ✅ ログパネルを表示
        logPanel.SetActive(true);

        // 最大行数を超えた場合、即座に先頭を削除
        if (logQueue.Count > maxLogCount)
        {
            logQueue.Dequeue();
            logTimers.Dequeue();
        }

        UpdateLogDisplay();

        // 削除処理が進行中でなければ開始
        if (!isRemovingLogs)
        {
            StartCoroutine(RemoveLogsOverTime());
        }
    }

    /// <summary>
    /// 一定時間後に1秒間隔で古いログを削除するコルーチン
    /// </summary>
    private IEnumerator RemoveLogsOverTime()
    {
        isRemovingLogs = true;

        while (logQueue.Count > 0)
        {
            float waitTime = logTimers.Peek() - Time.time; // 次のログの削除タイミング
            if (waitTime > 0)
            {
                yield return new WaitForSeconds(waitTime);
            }

            // ログを削除
            if (logQueue.Count > 0)
            {
                logQueue.Dequeue();
                logTimers.Dequeue();
                UpdateLogDisplay();
            }

            yield return new WaitForSeconds(1f); // 次の削除まで1秒待機
        }

        isRemovingLogs = false;

        // ✅ すべてのログが削除されたらパネルを非表示
        logPanel.SetActive(false);
    }

    /// <summary>
    /// UI にログを反映し、パネルの表示状態を管理
    /// </summary>
    private void UpdateLogDisplay()
    {
        logText.text = string.Join("\n", logQueue.ToArray());

        // ✅ ログが空ならパネルを非表示
        if (logQueue.Count == 0)
        {
            logPanel.SetActive(false);
        }
    }
}
