

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

   
    [Header("UI設定")]
    [Tooltip("一時停止メニューのルートGameObjectをアタッチ")]
    public GameObject pauseMenuUI; // 設定パネルのGameObject

    
    private bool isGameStarted = false;
    private bool isGameFinished = false;
    private bool isGamePaused = false; // 一時停止の状態
   
    void Start()
    {
       
        if (countdownTimer == null || player == null || objectSpawner == null)
        {
            Debug.LogError("GameManagerの管理対象スクリプトが設定されていません。インスペクターを確認してください。");
            return; 
        }
        
        if (fishAIs.Length == 0)
        {
            Debug.LogWarning("FishAIの配列が空です。インスペクターでAIの魚をアタッチしてください。");
        }

        
        if (pauseMenuUI == null)
        {
            Debug.LogError("一時停止メニュー(Pause Menu UI)のGameObjectが設定されていません。インスペクターを確認してください。");
        }
        else
        {
            
            pauseMenuUI.SetActive(false);
        }
        
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
      
        player.SetControllable(false);
        
        foreach (var fishAI in fishAIs)
        {
            fishAI.SetControllable(false);
        }
    }

    void Update()
    {
       
        if (countdownTimer == null || player == null || objectSpawner == null) return;
        
      
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameFinished)
        {
            
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
        if (!isGameStarted && countdownTimer.IsGameActive())
        {
            StartGame();
        }

        
        if (isGameStarted && !isGameFinished && countdownTimer.IsFinished())
        {
            EndGame();
        }
    }

       private void StartGame()
    {
        isGameStarted = true;
        Debug.Log("GAME START!");

        
        player.SetControllable(true);
       
        foreach (var fishAI in fishAIs)
        {
            fishAI.SetControllable(true);
        }

      
        Invoke(nameof(TriggerObjectSpawning), objectSpawnDelay);
    }
    
   
    private void TriggerObjectSpawning()
    {
        Debug.Log("イベント発生！オブジェクトの落下を開始します。");
        
        objectSpawner.StartSpawning();
    }

       private void EndGame()
    {
        isGameFinished = true;
        Debug.Log("GAME FINISH!");

        
        player.SetControllable(false);
        
        foreach (var fishAI in fishAIs)
        {
            fishAI.SetControllable(false);
        }

        
        CancelInvoke(nameof(TriggerObjectSpawning));
       
        objectSpawner.StopSpawning();
        
        
        Time.timeScale = 1f;
       
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
       
    }
    
   
    public void PauseGame()
    {
        if (isGameFinished) return; 

        isGamePaused = true;
        
       
        player.SetControllable(false); 
        foreach (var fishAI in fishAIs)
        {
            fishAI.SetControllable(false);
        }
        
        
        Time.timeScale = 0f; 
        
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
       
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
        Debug.Log("GAME PAUSED. (Cursor Visible)");
    }

  
    public void ResumeGame()
    {
        isGamePaused = false;

       
        Time.timeScale = 1f; 
        
       
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        
       
        if (isGameStarted && !isGameFinished)
        {
            player.SetControllable(true);
            foreach (var fishAI in fishAIs)
            {
                fishAI.SetControllable(true);
            }
        }
        
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        Debug.Log("GAME RESUMED. (Cursor Hidden)");
    }
   
}