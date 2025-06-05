using UnityEngine;

public class WeaponUpgradeUI : MonoBehaviour
{
    public GameObject upgradePanel; // 武器強化パネル
    private bool isPanelOpen = false; // パネルの状態を管理

    void Start()
    {
        LockCursor(); // ゲーム開始時にカーソルをロック
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Fキーが押されたら
        {
            ToggleUpgradePanel();
        }
    }

    void ToggleUpgradePanel()
    {
        isPanelOpen = !isPanelOpen;
        upgradePanel.SetActive(isPanelOpen); // パネルを表示/非表示

        if (isPanelOpen)
        {
            PauseGame(); // ゲームを停止し、マウスカーソルを使用可能にする
        }
        else
        {
            ResumeGame(); // ゲームを再開し、マウスカーソルをロックする
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // 時間を停止
        UnlockCursor(); // カーソルを使用可能にする
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // 時間を再開
        LockCursor(); // カーソルをロックする
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // カーソルをロック
        Cursor.visible = false; // カーソルを非表示
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None; // カーソルをロック解除
        Cursor.visible = true; // カーソルを表示
    }
}
