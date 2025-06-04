
/*using UnityEngine;
public class startOptions
{
    public System.Action CloseDelegete;
}
public class StartUI : ViewController
{
    public static string prefabName = "StartCanvas";
    public static GameObject prefab;
    private startOptions start;
    public Manager gameManager;
    private void Start()
    {
        gameManager = FindFirstObjectByType<Manager>();
    }
    public static StartUI Show(startOptions st)
    {
        if (prefab == null)
        {
            prefab = Resources.Load(prefabName) as GameObject;
        }
        GameObject obj = Instantiate(prefab);
        StartUI Dlog = obj.GetComponent<StartUI>();
        Dlog.UpdateContent(st);
        return Dlog;
    }
    void UpdateContent(startOptions st)
    {
        Cursor.lockState = CursorLockMode.None;
        start = st;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnTapClose();
        }
    }
    void OnTapClose()
    {
        if (start.CloseDelegete != null)
            start.CloseDelegete.Invoke();
        //gameManager.OnGameStartDestroyed();
        Destroy(gameObject);
    }
}*/