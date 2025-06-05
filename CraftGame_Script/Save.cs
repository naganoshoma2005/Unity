using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using Newtonsoft.Json;

public class Save : MonoBehaviour
{
    private string path;
    public int saveNumber;
    public PlayerInventory PlayerInventory;
    public PlayerStats PlayerStats;
    public TestItemManager TestItemManager;
    public List<GameObject> destroyobjects;
    //セーブ内容を追加するならここにそれを扱っているスクリプトをかく
    private void Awake()
    {
        path = Path.Combine(Application.persistentDataPath, $"Savedata{Global.Instance.SaveNumber}.json");
    }
    private void Start()
    {
        if (Global.Instance.Load)
        {
            path = Path.Combine(Application.persistentDataPath, $"Savedata{Global.Instance.SaveNumber}.json");
            GameStatus temp = Load();
            if (temp != null)
            {
                PlayerStats.level = temp.level;
                PlayerStats.experience = temp.experience;
            }
        }
    }
    public GameStatus Load()
    {
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameStatus gameStatus = JsonConvert.DeserializeObject<GameStatus>(json);
            Debug.Log("読み込み完了");
            return gameStatus;
        }
        else
        {
            Debug.LogWarning("ファイルが見つかりません:" + path);
            return null;
        }
    }

    public void SaveGame()
    {
        GameStatus gameStatus = new GameStatus();
        gameStatus.level = PlayerStats.level;
        gameStatus.experience = PlayerStats.experience;
        Saved(gameStatus);
    }
    public void Saved(GameStatus gameStatus)
    {
        string json = JsonConvert.SerializeObject(gameStatus, Formatting.Indented);
        File.WriteAllText(path, json);
        Debug.Log("保存完了:" + path);
        
    }


}
[System.Serializable]
public class GameStatus
{
    public Dictionary<string, int> ItemInventry;
    public List<int> itemIDs;
    public int level ;
    public int experience;

}
