using UnityEngine;

public class PanelReferenceSetter : MonoBehaviour
{
    void Awake()
    {
        

        if (SettingManager.Instance != null)
        {
            
            SettingManager.Instance.settingsPanel = this.gameObject;
            
            
            this.gameObject.SetActive(false); 

            Debug.Log("PanelReferenceSetter: 新しい設定パネルの参照を設定し、非表示にしました。", this);
        }
        
    }
}