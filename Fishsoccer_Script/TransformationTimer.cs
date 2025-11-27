

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