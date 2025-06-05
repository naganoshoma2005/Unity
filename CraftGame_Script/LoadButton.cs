using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class LoadButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

   public void Load(int LoadNumber)
    {
        string path = Path.Combine(Application.persistentDataPath, $"Savedata{LoadNumber}.json");
        if (File.Exists(path))
        {
            Global.Instance.SaveNumber = LoadNumber;
            Global.Instance.Load = true;
            SceneManager.LoadScene("Game_scene");
        }
        else
        {
            Debug.LogWarning("セーブデータが見つかりません");
            Global.Instance.Load = false;
            Global.Instance.SaveNumber = LoadNumber;
            SceneManager.LoadScene("Game_scene");
        }

    }
}
