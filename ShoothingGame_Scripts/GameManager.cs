using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string SceneName="GAMEOVER_scene";
    private void Start()
    {
        Debug.Log(SceneName);
    }
    public void OnPlayerDestroy()
    {
        SceneManager.LoadScene(SceneName);
    }
}
