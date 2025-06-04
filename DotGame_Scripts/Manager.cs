using UnityEngine;
using Game;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    // Inspectorで設定できるようにpublicで指定
    public GameOverController gameOverController;
    public  GameObject[] objectToSpawn;
    public int ObjectNumber;
    public PlayerStats playerStats;
    private bool isStart = true;
    public GameObject Canvas;
    public bool CatchItem;
    public GameObject CatchingItem;
    public GameObject[] select;
    

    public string sceneName;
    public GameObject[] selectableObjects; // 選択可能なオブジェクトを格納する配列
    private int currentSelectedIndex = -1; // 現在選択されているオブジェクトのインデックス




    private void Start()
    {
        ObjectNumber = 10;
        gameOverController = FindFirstObjectByType<GameOverController>();
        foreach (var obj in selectableObjects)
        {
            obj.SetActive(false);
        }

    }
    void Update()
    {
      /*if(isStart==true)
        {
            StartGame();
            isStart = false;
        }*/

        // マウスの左クリックを監視
        if (Input.GetMouseButtonDown(0))
        {
            // マウス位置を取得し、スクリーン座標をワールド座標に変換
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            // クリックした位置にRayを飛ばす
            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);

            // 何かに当たったかどうかをチェック
            if (hit.collider != null)
            {
                // 衝突したオブジェクトの情報を取得
                GameObject selectedObject = hit.transform.gameObject;
                // オブジェクトの名前をコンソールに出力
                Debug.Log("Selected Object: " + selectedObject.name);
                Setting setting = selectedObject.GetComponent<Setting>();
                if (selectedObject.CompareTag("Set") && setting.set == true && playerStats.buy == true)
                {
                    // オブジェクトの位置にスナップ
                    Vector3 hitPosition = selectedObject.transform.position;

                    Vector3 spawnPosition = new Vector3(hitPosition.x + 0.065f, hitPosition.y, -2); // Z座標は固定

                    // 新しいオブジェクトを召喚

                        GameObject SpawnObject = Instantiate(objectToSpawn[ObjectNumber], spawnPosition, Quaternion.identity);
                        Stop stop = SpawnObject.GetComponent<Stop>();
                        if (stop != null)
                        {
                            stop.setting = setting;
                        }
                        setting.Put();
                        playerStats.Shopping();
                }

                if (selectedObject.CompareTag("Block"))
                {
                    ObjectNumber = 1;
                    SelectObject(1);
                }
                if (selectedObject.CompareTag("Trap"))
                {
                    ObjectNumber = 0;
                    SelectObject(0);
                }
                if (selectedObject.CompareTag("Trap1"))
                {
                    ObjectNumber = 2;
                    SelectObject(2);

                }
                if (selectedObject.CompareTag("PowerUp"))
                {
                    Debug.Log("URTanaka");
                    CatchItem = true;
                    CatchingItem = selectedObject;
                    Chase chase = CatchingItem.GetComponent<Chase>();
                    chase.MGR = this;
                    
                }
            }
        }
        if (Input.GetMouseButtonUp(0)&&CatchItem ==true)
        {
            CatchItem = false;
        }
      
    }

    // 新しいメソッド
    private void SelectObject(int index)
    {
        if (index < 0 || index >= selectableObjects.Length)
        {
            Debug.LogWarning($"Invalid index: {index}");
            return;
        }

        // 現在選択されているオブジェクトを非アクティブにする
        if (currentSelectedIndex != -1 && currentSelectedIndex < selectableObjects.Length)
        {
            selectableObjects[currentSelectedIndex].SetActive(false);
        }

        // 新しく選択されたオブジェクトをアクティブにする
        selectableObjects[index].SetActive(true);

        // 現在の選択を更新
        currentSelectedIndex = index;
        ObjectNumber = index;

        Debug.Log($"Selected object: {selectableObjects[index].name}");
    }

    public void overLine()
    {
        SceneManager.LoadScene(sceneName);
    }
}


// 目標オブジェクトへの方向を計算
//Vector3 direction = (targetObject.transform.position - transform.position).normalized;
// 移動
//transform.position += direction * enemyStats.AGI * Time.deltaTime;
