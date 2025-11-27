using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // シーン名で指定する場合（推奨）
    public void LoadSceneByName(string sceneName)
    {

        // シーン遷移の直前にカーソルを確実に表示し、ロックを解除する
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


        SceneManager.LoadScene(sceneName);
    }
}
