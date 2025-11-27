/*
using UnityEngine;
using System.Collections;

public class TransformationTimer : MonoBehaviour
{
    // このスクリプトの唯一のインスタンスを保持（シングルトンパターン）
    public static TransformationTimer Instance { get; private set; }

    private void Awake()
    {
        // シーン内にインスタンスが一つだけになるようにする
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも破壊されないようにする
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 指定されたプレイヤーの変身タイマーを開始します
    /// </summary>
    public void StartTransformationTimer(PlayerTransformation player, float duration)
    {
        StartCoroutine(TransformationCountdown(player, duration));
    }

    private IEnumerator TransformationCountdown(PlayerTransformation player, float duration)
    {
        // 指定された時間だけ待つ
        yield return new WaitForSeconds(duration);

        // 時間が来たら、指定されたプレイヤーに変身を解除するよう命令する
        if (player != null)
        {
            player.RevertTransformation(); // プレイヤーの変身解除メソッドを呼び出す
        }
    }
}*/
/*
using UnityEngine;
using System.Collections;

public class TransformationTimer : MonoBehaviour
{
    // このスクリプトの唯一のインスタンスを保持（シングルトンパターン）
    public static TransformationTimer Instance { get; private set; }

    // ★変更点：現在実行中のコルーチンを保持するための変数を追加
    private Coroutine currentCountdownCoroutine;

    private void Awake()
    {
        // シーン内にインスタンスが一つだけになるようにする
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも破壊されないようにする
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 指定されたプレイヤーの変身タイマーを開始します
    /// </summary>
    public void StartTransformationTimer(PlayerTransformation player, float duration)
    {
        // ★変更点：もし既にカウントダウンが実行中なら、それを停止する
        if (currentCountdownCoroutine != null)
        {
            StopCoroutine(currentCountdownCoroutine);
        }

        // ★変更点：新しいカウントダウンを開始し、その参照を保持する
        currentCountdownCoroutine = StartCoroutine(TransformationCountdown(player, duration));
    }

    private IEnumerator TransformationCountdown(PlayerTransformation player, float duration)
    {
        // 指定された時間だけ待つ
        yield return new WaitForSeconds(duration);

        // 時間が来たら、指定されたプレイヤーに変身を解除するよう命令する
        if (player != null)
        {
            player.RevertTransformation(); // プレイヤーの変身解除メソッドを呼び出す
        }

        // ★追加（任意ですが推奨）：コルーチンが終了したので、参照をnullに戻す
        currentCountdownCoroutine = null;
    }
}*/
/*
using UnityEngine;
using System.Collections;

public class TransformationTimer : MonoBehaviour
{
    // シングルトンインスタンス
    public static TransformationTimer Instance { get; private set; }

    private Coroutine activeTimerCoroutine; // 実行中のタイマーコルーチンを保持

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // シーンを跨いで残したい場合は以下のコメントアウトを外す
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartTransformationTimer(PlayerTransformation player, float duration)
    {
        // 既にタイマーが動いている場合は、一度停止してから再開
        if (activeTimerCoroutine != null)
        {
            StopCoroutine(activeTimerCoroutine);
        }
        activeTimerCoroutine = StartCoroutine(TimerCoroutine(player, duration));
        Debug.Log("変身タイマー開始: " + duration + "秒");
    }

    private IEnumerator TimerCoroutine(PlayerTransformation player, float duration)
    {
        yield return new WaitForSeconds(duration);

        // 時間切れになったら変身解除を呼び出す
        player.RevertTransformation();
        activeTimerCoroutine = null;
        Debug.Log("変身時間切れにより解除されました。");
    }

    // ★CS1061エラーを解消し、二重解除を防ぐために必要なメソッド
    public void StopTransformationTimer()
    {
        if (activeTimerCoroutine != null)
        {
            StopCoroutine(activeTimerCoroutine);
            activeTimerCoroutine = null; // コルーチンが停止したことを示す
            Debug.Log("変身タイマーを手動で停止しました。");
        }
    }
}*/

using UnityEngine;

// PlayerTransformationが呼び出すためのタイマー管理クラス
public class TransformationTimer : MonoBehaviour
{
    public static TransformationTimer Instance { get; private set; }

    private PlayerTransformation currentPlayer;
    private float timer = 0f;
    private bool isRunning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // シーンをまたいで保持したい場合は、DontDestroyOnLoad(gameObject); を追加
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                // タイマー切れで変身解除を呼び出す
                if (currentPlayer != null)
                {
                    currentPlayer.RevertTransformation(); 
                }
                StopTransformationTimer();
            }
        }
    }

    public void StartTransformationTimer(PlayerTransformation player, float duration)
    {
        StopTransformationTimer(); // 既にタイマーが動いていたら停止
        currentPlayer = player;
        timer = duration;
        isRunning = true;
    }

    public void StopTransformationTimer()
    {
        isRunning = false;
        timer = 0f;
        currentPlayer = null;
    }
}