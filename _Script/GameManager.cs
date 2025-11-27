  /*
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("管理対象のスクリプト")]
    [Tooltip("シーン内のCountdownTimerスクリプトを持つオブジェクトをアタッチ")]
    public CountdownTimer countdownTimer;

    [Tooltip("シーン内のMoveFishスクリプトを持つプレイヤーオブジェクトをアタッチ")]
    public MoveFish player;

    [Tooltip("シーン内のObjectSpawnerスクリプトを持つオブジェクトをアタッチ")]
    public ObjectSpawner objectSpawner;

    [Header("イベント設定")]
    [Tooltip("ゲーム開始からオブジェクト落下が始まるまでの時間(秒)")]
    public float objectSpawnDelay = 60f;

    // --- ▼ここから追加▼ ---
    [Tooltip("シーン内のFishAIスクリプトを持つAI魚オブジェクトをアタッチ")]
    public FishAI[] fishAIs;
    // --- ▲ここまで追加▲ ---

    // ゲームの状態を管理するフラグ
    private bool isGameStarted = false;
    private bool isGameFinished = false;

    void Start()
    {
        // GameManagerが管理対象を見つけられなかった場合に警告を出す
        if (countdownTimer == null || player == null || objectSpawner == null)
        {
            Debug.LogError("GameManagerの管理対象スクリプトが設定されていません。インスペクターを確認してください。");
            return; // 処理を中断
        }
        
        // --- ▼ここから追加▼ ---
        if (fishAIs.Length == 0)
        {
            Debug.LogWarning("FishAIの配列が空です。インスペクターでAIの魚をアタッチしてください。");
        }
        // --- ▲ここまで追加▲ ---

        // ゲーム開始直後はプレイヤーを操作不能にする
        player.SetControllable(false);
        // --- ▼ここから追加▼ ---
        // ゲーム開始直後はAI魚を操作不能にする
        foreach (var fishAI in fishAIs)
        {
            fishAI.SetControllable(false);
        }
        // --- ▲ここまで追加▲ ---
    }

    void Update()
    {
        // GameManagerのセットアップが完了していなければ何もしない
        if (countdownTimer == null || player == null || objectSpawner == null) return;
        
        // まだゲームが始まっておらず、タイマーが「START!」の後になったらゲームを開始
        if (!isGameStarted && countdownTimer.IsGameActive())
        {
            StartGame();
        }

        // ゲームが始まっていて、かつタイマーが0になったらゲームを終了
        if (isGameStarted && !isGameFinished && countdownTimer.IsFinished())
        {
            EndGame();
        }
    }

    /// <summary>
    /// ゲームを開始する処理
    /// </summary>
    private void StartGame()
    {
        isGameStarted = true;
        Debug.Log("GAME START!");

        // プレイヤーを操作可能にする
        player.SetControllable(true);
        // --- ▼ここから追加▼ ---
        // AI魚を操作可能にする
        foreach (var fishAI in fishAIs)
        {
            fishAI.SetControllable(true);
        }
        // --- ▲ここまで追加▲ ---

        // objectSpawnDelayで指定した秒数後にTriggerObjectSpawningメソッドを呼び出す
        Invoke(nameof(TriggerObjectSpawning), objectSpawnDelay);
    }
    
    /// <summary>
    /// オブジェクトの生成を開始するイベント用のメソッド
    /// </summary>
    private void TriggerObjectSpawning()
    {
        Debug.Log("イベント発生！オブジェクトの落下を開始します。");
        // オブジェクトの生成を開始する
        objectSpawner.StartSpawning();
    }

    /// <summary>
    /// ゲームを終了する処理
    /// </summary>
    private void EndGame()
    {
        isGameFinished = true;
        Debug.Log("GAME FINISH!");

        // プレイヤーを操作不能にする
        player.SetControllable(false);
        // --- ▼ここから追加▼ ---
        // AI魚を操作不能にする
        foreach (var fishAI in fishAIs)
        {
            fishAI.SetControllable(false);
        }
        // --- ▲ここまで追加▲ ---

        // もしTriggerObjectSpawningの呼び出しが予約されていたらキャンセルする
        CancelInvoke(nameof(TriggerObjectSpawning));
        // オブジェクトの生成を停止する
        objectSpawner.StopSpawning();
    }
}*/

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("管理対象のスクリプト")]
    [Tooltip("シーン内のCountdownTimerスクリプトを持つオブジェクトをアタッチ")]
    public CountdownTimer countdownTimer;

    [Tooltip("シーン内のMoveFishスクリプトを持つプレイヤーオブジェクトをアタッチ")]
    public MoveFish player;

    [Tooltip("シーン内のObjectSpawnerスクリプトを持つオブジェクトをアタッチ")]
    public ObjectSpawner objectSpawner;

    [Header("イベント設定")]
    [Tooltip("ゲーム開始からオブジェクト落下が始まるまでの時間(秒)")]
    public float objectSpawnDelay = 60f;

    [Tooltip("シーン内のFishAIスクリプトを持つAI魚オブジェクトをアタッチ")]
    public FishAI[] fishAIs;

    // --- ▼ここから追加/変更▼ ---
    [Header("UI設定")]
    [Tooltip("一時停止メニューのルートGameObjectをアタッチ")]
    public GameObject pauseMenuUI; // 設定パネルのGameObject

    // ゲームの状態を管理するフラグ
    private bool isGameStarted = false;
    private bool isGameFinished = false;
    private bool isGamePaused = false; // 一時停止の状態
    // --- ▲ここまで追加/変更▲ ---

    void Start()
    {
        // GameManagerが管理対象を見つけられなかった場合に警告を出す
        if (countdownTimer == null || player == null || objectSpawner == null)
        {
            Debug.LogError("GameManagerの管理対象スクリプトが設定されていません。インスペクターを確認してください。");
            return; // 処理を中断
        }
        
        if (fishAIs.Length == 0)
        {
            Debug.LogWarning("FishAIの配列が空です。インスペクターでAIの魚をアタッチしてください。");
        }

        // --- ▼追加：UIとカーソルの初期設定▼ ---
        if (pauseMenuUI == null)
        {
            Debug.LogError("一時停止メニュー(Pause Menu UI)のGameObjectが設定されていません。インスペクターを確認してください。");
        }
        else
        {
            // ゲーム開始時はパネルを非表示にする
            pauseMenuUI.SetActive(false);
        }
        
        // ゲーム開始時はカーソルを非表示＆ロック（プレイヤー操作用）
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // --- ▲追加：UIとカーソルの初期設定▲ ---

        // ゲーム開始直後はプレイヤーを操作不能にする
        player.SetControllable(false);
        // ゲーム開始直後はAI魚を操作不能にする
        foreach (var fishAI in fishAIs)
        {
            fishAI.SetControllable(false);
        }
    }

    void Update()
    {
        // GameManagerのセットアップが完了していなければ何もしない
        if (countdownTimer == null || player == null || objectSpawner == null) return;
        
        // --- ▼追加：ESCキー処理▼ ---
        // ESCキーが押されたか、かつゲームが終了していない場合
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameFinished)
        {
            // isGamePausedの状態に応じて、一時停止/再開を切り替える
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        // --- ▲追加：ESCキー処理▲ ---
        
        // まだゲームが始まっておらず、タイマーが「START!」の後になったらゲームを開始
        if (!isGameStarted && countdownTimer.IsGameActive())
        {
            StartGame();
        }

        // ゲームが始まっていて、かつタイマーが0になったらゲームを終了
        if (isGameStarted && !isGameFinished && countdownTimer.IsFinished())
        {
            EndGame();
        }
    }

    /// <summary>
    /// ゲームを開始する処理
    /// </summary>
    private void StartGame()
    {
        isGameStarted = true;
        Debug.Log("GAME START!");

        // プレイヤーを操作可能にする
        player.SetControllable(true);
        // AI魚を操作可能にする
        foreach (var fishAI in fishAIs)
        {
            fishAI.SetControllable(true);
        }

        // objectSpawnDelayで指定した秒数後にTriggerObjectSpawningメソッドを呼び出す
        Invoke(nameof(TriggerObjectSpawning), objectSpawnDelay);
    }
    
    /// <summary>
    /// オブジェクトの生成を開始するイベント用のメソッド
    /// </summary>
    private void TriggerObjectSpawning()
    {
        Debug.Log("イベント発生！オブジェクトの落下を開始します。");
        // オブジェクトの生成を開始する
        objectSpawner.StartSpawning();
    }

    /// <summary>
    /// ゲームを終了する処理
    /// </summary>
    private void EndGame()
    {
        isGameFinished = true;
        Debug.Log("GAME FINISH!");

        // プレイヤーを操作不能にする
        player.SetControllable(false);
        // AI魚を操作不能にする
        foreach (var fishAI in fishAIs)
        {
            fishAI.SetControllable(false);
        }

        // もしTriggerObjectSpawningの呼び出しが予約されていたらキャンセルする
        CancelInvoke(nameof(TriggerObjectSpawning));
        // オブジェクトの生成を停止する
        objectSpawner.StopSpawning();
        
        // --- ▼追加：ゲーム終了時のクリーンアップ▼ ---
        // ゲーム終了時は一時停止状態を解除し、時間の流れを元に戻す
        Time.timeScale = 1f;
        // ゲーム終了時に一時停止メニューが表示されていたら非表示にする
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        // --- ▲追加：ゲーム終了時のクリーンアップ▲ ---
    }
    
    // --- ▼ここから追加：一時停止/再開処理▼ ---
    /// <summary>
    /// ゲームを一時停止する処理
    /// </summary>
    public void PauseGame()
    {
        if (isGameFinished) return; // ゲームが終了していたら一時停止しない

        isGamePaused = true;
        
        // プレイヤー、AIの操作を不能にする
        player.SetControllable(false); 
        foreach (var fishAI in fishAIs)
        {
            fishAI.SetControllable(false);
        }
        
        // 時間を停止
        Time.timeScale = 0f; 
        
        // カーソルを表示し、画面にロックしない
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        // ポーズメニューを表示
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
        Debug.Log("GAME PAUSED. (Cursor Visible)");
    }

    /// <summary>
    /// ゲームを再開する処理
    /// </summary>
    public void ResumeGame()
    {
        isGamePaused = false;

        // 時間を再開
        Time.timeScale = 1f; 
        
        // ポーズメニューを非表示
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        
        // プレイヤー、AIの操作を可能にする (ゲームが進行中の場合のみ)
        if (isGameStarted && !isGameFinished)
        {
            player.SetControllable(true);
            foreach (var fishAI in fishAIs)
            {
                fishAI.SetControllable(true);
            }
        }
        
        // カーソルを非表示にし、画面中央にロックする
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        Debug.Log("GAME RESUMED. (Cursor Hidden)");
    }
    // --- ▲ここまで追加：一時停止/再開処理▲ ---
}