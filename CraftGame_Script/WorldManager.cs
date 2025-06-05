using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance { get; private set; }

    private List<GameObject> destroyedObjects = new List<GameObject>();

    private void Awake()
    {
        // シングルトンの初期化
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 壊れたオブジェクトをリストに追加し、非表示にする
    /// </summary>
    /// <param name="obj">壊れたオブジェクト</param>
    public void ObjectDestroyed(GameObject obj)
    {
        if (!destroyedObjects.Contains(obj))
        {
            destroyedObjects.Add(obj);
            Debug.Log(GetDestroyedObjects());
        }
    }

    /// <summary>
    /// 壊れたオブジェクト一覧を取得する（デバッグ用）
    /// </summary>
    public List<GameObject> GetDestroyedObjects()
    {
        return destroyedObjects;
    }
    public void WorldUpdate()
    {
        foreach(GameObject destroyObject in destroyedObjects)
        {
            destroyObject.SetActive(false);
        }
    }

}