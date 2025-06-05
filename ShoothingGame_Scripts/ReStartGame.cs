using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStartGame : MonoBehaviour
{
    // この関数をボタンのイベントに追加してシーンを移動
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}