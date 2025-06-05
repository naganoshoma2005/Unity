using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;       // 一時停止メニューのUI（Inspectorから設定）
    public GameObject confirmDialog;   // タイトルに戻る確認ダイアログのUI（Inspectorから設定）

    // 一時停止メニューの表示・非表示を切り替える
    public void TogglePause()
    {
        bool isPaused = Time.timeScale == 0;

        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    // ゲームを一時停止する
    private void PauseGame()
    {
        Time.timeScale = 0; // 時間を停止
        pauseMenu.SetActive(true); // メニューを表示
    }

    // ゲームを再開する
    public void ResumeGame()
    {
        Time.timeScale = 1; // 時間を再開
        pauseMenu.SetActive(false); // メニューを非表示
        confirmDialog.SetActive(false); // 確認ダイアログも非表示にする
    }

    // タイトルに戻る確認ダイアログを表示
    public void ShowConfirmDialog()
    {
        confirmDialog.SetActive(true); // 確認ダイアログを表示
    }

    // タイトルに戻る（確認済み）
    public void ConfirmReturnToTitle()
    {
        Time.timeScale = 1; // 時間を通常に戻す
        SceneManager.LoadScene("Start_scene"); // タイトルシーンのシーン名に変更
    }

    // タイトルに戻るキャンセル
    public void CancelReturnToTitle()
    {
        confirmDialog.SetActive(false); // 確認ダイアログを非表示
    }
}
