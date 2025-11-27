using UnityEngine;

public class PanelReferenceSetter : MonoBehaviour
{
    void Awake()
    {
        // PanelReferenceSetter は設定パネルのルートにアタッチされている
        // シーンロード時に必ず実行され、現在のシーンのパネル参照をSettingManagerに渡す。

        if (SettingManager.Instance != null)
        {
            // ★ 新しいパネルの参照を設定マネージャーに渡す
            SettingManager.Instance.settingsPanel = this.gameObject;
            
            // ★ シーンロード完了後、初期状態を強制的に非表示にする
            // これで2回押しの問題を解決
            this.gameObject.SetActive(false); 

            Debug.Log("PanelReferenceSetter: 新しい設定パネルの参照を設定し、非表示にしました。", this);
        }
        else
        {
             // 別のシーンから戻る際に、このパネルが破棄されないようにするため
             // DontDestroyOnLoad(this.gameObject); // これは通常不要
        }
    }
}