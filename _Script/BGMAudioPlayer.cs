/*using UnityEngine;

public class BGMAudioPlayer : MonoBehaviour
{
    // BGM再生オブジェクト用の静的インスタンス
    public static BGMAudioPlayer Instance; 
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // シーンを跨いでも破棄しない
            DontDestroyOnLoad(this.gameObject);
            
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("BGMAudioPlayer is missing an AudioSource component.");
                return;
            }

            // 🚨 BGMの再生を開始する処理 🚨
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // 既にBGMが鳴っている古いオブジェクトがあるため、新しいものは破棄
            Destroy(this.gameObject);
        }
    }
}*/
using UnityEngine;

public class BGMAudioPlayer : MonoBehaviour
{
    // 唯一のインスタンス
    public static BGMAudioPlayer Instance; 
    private AudioSource audioSource;

    void Awake()
    {
        // 🚨 シングルトンパターンによる重複防止と永続化 🚨
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("BGMAudioPlayer is missing an AudioSource component.");
                return;
            }

            // BGMの再生を開始
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // 既にBGMが鳴っている古いオブジェクトがあるため、新しいものは破棄
            Destroy(this.gameObject);
        }
    }
}