
using UnityEngine;
public class GameoverOptions
{
    public System.Action CloseDelegete;
}
public class GameoverUI : ViewController
{
    public static string prefabName = "Gameover_Panel";
    public static GameObject prefab;
    private GameoverOptions gameoverOptions;
    public Manager gameManager;
    private void Start()
    {
        gameManager = FindFirstObjectByType<Manager>();
    }
    public static GameoverUI Show(GameoverOptions gameover)
    {
        if (prefab == null)
        {
            prefab = Resources.Load(prefabName) as GameObject;
        }
        GameObject obj = Instantiate(prefab);
        GameoverUI Dlog = obj.GetComponent<GameoverUI>();
        Dlog.UpdateContent(gameover);
        return Dlog;
    }
    void UpdateContent(GameoverOptions gmov)
    {
        Cursor.lockState = CursorLockMode.None;
        gameoverOptions = gmov;
    }
}