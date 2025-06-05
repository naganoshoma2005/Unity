using UnityEngine;

public class EscapeKeyPanelController : MonoBehaviour
{
    public GameObject targetPanel; // 表示/非表示にするパネル
    private bool isPanelOpen = false; // パネルの状態を管理

    void Update()
    {
        // エスケープキーが押されたら
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel();
        }
    }

    public　void TogglePanel()
    {
        isPanelOpen = !isPanelOpen;
        targetPanel.SetActive(isPanelOpen);

        if (isPanelOpen)
        {
            Time.timeScale = 0; // 時間を止める
            Cursor.lockState = CursorLockMode.None; // マウスカーソルを表示
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1; // 時間を元に戻す
            Cursor.lockState = CursorLockMode.Locked; // カーソルをロック
            Cursor.visible = false;
        }
    }
}
