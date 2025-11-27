/*using UnityEngine;
using Unity.UI;

public class SettingManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public void ToggleSettingsPanel()
    {
        bool isActive = settingsPanel.activeSelf;
        settingsPanel.SetActive(!isActive);
    }
}*/
/*using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    // 唯一のインスタンス
    public static SettingManager Instance;

    // シーンごとに参照が更新される設定パネルのルートGameObject
    [HideInInspector]
    public GameObject settingsPanel; 

    void Awake()
    {
        // 🚨 シングルトンパターンによる重複防止と永続化 🚨
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // 既に存在する場合は新しい方を破棄
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        // 念のため、初期状態を非表示に設定（PanelReferenceSetterでも設定可）
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    public void ToggleSettingsPanel()
    {
        // settingsPanelがそのシーンで正しく設定されているか確認
        if (settingsPanel != null)
        {
            // パネルの状態を切り替える
            bool isActive = settingsPanel.activeSelf;
            settingsPanel.SetActive(!isActive);
        }
        else
        {
            Debug.LogWarning("Setting panel reference is missing in the current scene. Did you attach PanelReferenceSetter to the panel?");
        }
    }
}*/
// Assets/_Script/SettingManager.cs

using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance { get; private set; } 
    
    [Tooltip("表示・非表示を切り替える設定パネルのルートGameObjectを指定します。")]
    public GameObject settingsPanel; 

    private void Awake()
    {
        // シングルトンの初期化処理
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        // ★★★ この処理が、SettingManagerのAwakeで確実に実行されるようにします ★★★
        // パネルがインスペクターで設定済みであれば、初期状態として必ず非表示にする
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            Debug.Log("設定パネルを初期状態（非表示）に設定しました。");
        }
    }

    /// <summary>
    /// 設定パネルの表示状態を切り替えます。
    /// ボタンのOnClickイベントなどから呼び出します。
    /// </summary>
    public void ToggleSettingsPanel()
    {
        // 参照がない場合は、エラーログを出して処理を中断します
        if (settingsPanel == null)
        {
            // 元の警告の意図を汲みつつ、より強力なエラーに変更
            Debug.LogError("設定パネルの参照がありません。SettingManagerのインスペクターにパネルを割り当ててください。", this);
            return;
        }

        // 現在の状態を反転させます
        bool isActive = settingsPanel.activeSelf;
        settingsPanel.SetActive(!isActive);
        
        Debug.Log($"設定パネルを {(isActive ? "非表示" : "表示")} にしました。");
    }
    
    // 他のセッティングに関するメソッド（音量調整など）をここに追加します...
}