
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance { get; private set; } 
    
    [Tooltip("表示・非表示を切り替える設定パネルのルートGameObjectを指定します。")]
    public GameObject settingsPanel; 

    private void Awake()
    {
      
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
        
        
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            Debug.Log("設定パネルを初期状態（非表示）に設定しました。");
        }
    }

    
    public void ToggleSettingsPanel()
    {
       
        if (settingsPanel == null)
        {
           
            Debug.LogError("設定パネルの参照がありません。SettingManagerのインスペクターにパネルを割り当ててください。", this);
            return;
        }

       
        bool isActive = settingsPanel.activeSelf;
        settingsPanel.SetActive(!isActive);
        
        Debug.Log($"設定パネルを {(isActive ? "非表示" : "表示")} にしました。");
    }
    
    
}