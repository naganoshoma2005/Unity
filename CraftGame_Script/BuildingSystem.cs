
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [Header("建築オブジェクト")]
    public GameObject wallPrefab;  // 壁のプレハブ
    public GameObject floorPrefab;  // 床のプレハブ
    public GameObject rampPrefab;   // スロープのプレハブ

    [Header("建築設定")]
    public float buildDistance = 5f;  // 建築可能な距離
    public LayerMask buildableSurfaces;  // 建築可能な表面のレイヤー
    public float overlapCheckRadius = 0.1f;  // オーバーラップチェックの半径
    public LayerMask buildingLayer;  // 建築オブジェクトのレイヤー
    public float Layerofset = 0.45f;

    // 建築可能なオブジェクトの配列
    private GameObject[] buildablePrefabs;
    private int currentPrefabIndex = 0;

    private GameObject currentPreviewObject;  // 現在のプレビューオブジェクト
    public bool isInBuildMode = false;

    // コンポーネント参照
    private PlayerMove playerMove;
    private BuildingModeUI buildingModeUI;

    [System.Obsolete]
    void Start()
    {
        // 建築可能なプレハブを配列に格納
        buildablePrefabs = new GameObject[] { wallPrefab, floorPrefab, rampPrefab };

        // 必要なコンポーネントを取得
        playerMove = GetComponent<PlayerMove>();
        buildingModeUI = FindObjectOfType<BuildingModeUI>();

        // 初期状態は建築モードOFF
        isInBuildMode = false;
        if (playerMove != null)
        {
            playerMove.SetBuildMode(false);
        }
    }

    void Update()
    {
        // 建築モードの切り替え
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBuildMode();
        }

        if (isInBuildMode)
        {
            HandleBuildingPreview();
            HandleBuildingRotation();
            HandlePrefabSelection();
        }
    }

    public void ToggleBuildMode()
    {
        isInBuildMode = !isInBuildMode;
        Debug.Log($"Building mode toggled: {isInBuildMode}");

        // プレイヤーの攻撃とガードを無効化
        if (playerMove != null)
        {
            playerMove.SetBuildMode(isInBuildMode);
        }

        // UIの状態を更新
        if (buildingModeUI != null)
        {
            buildingModeUI.SetBuildingMode(isInBuildMode);
        }

        // 建築モードを終了する際にプレビューオブジェクトを削除
        if (!isInBuildMode && currentPreviewObject != null)
        {
            Destroy(currentPreviewObject);
            currentPreviewObject = null;
        }
    }

    void HandleBuildingPreview()
    {
        Vector3 rayDirection = transform.forward;
        RaycastHit hit;

        bool isHit = Physics.Raycast(transform.position, rayDirection, out hit, buildDistance);

        if (isHit)
        {
            Debug.Log("aaaaaaaaaaadddddddddddd");
            Vector3 adjustedPosition = hit.point + Vector3.up * Layerofset;
            // 現在選択されているプレハブを使用
            GameObject currentPrefab = buildablePrefabs[currentPrefabIndex];

            // プレビューオブジェクトがまだない場合は作成
            if (currentPreviewObject == null)
            {
                currentPreviewObject = Instantiate(currentPrefab, hit.point, Quaternion.identity);

                // コリジョンを無効化
                Collider[] colliders = currentPreviewObject.GetComponentsInChildren<Collider>();
                foreach (Collider collider in colliders)
                {
                    collider.enabled = false;
                }

                // プレビューモードの設定
                Renderer[] renderers = currentPreviewObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    Material previewMaterial = new Material(renderer.material);
                    Color color = previewMaterial.color;
                    color.a = 0.5f;
                    previewMaterial.color = color;
                    renderer.material = previewMaterial;
                }
            }
            else
            {
                currentPreviewObject.transform.position = adjustedPosition;
            }// ← 位置更新時も補正 

                // 左クリックで設置
                if (Input.GetMouseButtonDown(0))
                {
                    PlaceBuilding();
                }
            }
        

        else if (currentPreviewObject != null)
        {

            Destroy(currentPreviewObject);
            currentPreviewObject = null;
        }
    }

    void HandleBuildingRotation()
    {
        if (currentPreviewObject != null)
        {
            // 右クリックで水平方向（Y軸）に回転
            if (Input.GetMouseButtonDown(1))
            {
                currentPreviewObject.transform.Rotate(Vector3.up, 90f);
            }

            // Rキーで垂直方向（X軸）に回転
            if (Input.GetKeyDown(KeyCode.R))
            {
                currentPreviewObject.transform.Rotate(Vector3.right, 90f);
            }
        }
    }

    void HandlePrefabSelection()
    {
        // Fキーでの選択
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangePrefab(1);
        }

        // マウスホイールでの選択
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            float scrollSpeed = 0.2f;
            scroll *= scrollSpeed;
            ChangePrefab(scroll > 0 ? 1 : -1);
        }
    }

    void ChangePrefab(int direction)
    {
        currentPrefabIndex += direction;

        // インデックスを配列の範囲内に収める
        if (currentPrefabIndex < 0)
            currentPrefabIndex = buildablePrefabs.Length - 1;
        else if (currentPrefabIndex >= buildablePrefabs.Length)
            currentPrefabIndex = 0;

        // 現在のプレビューオブジェクトを再作成
        if (currentPreviewObject != null)
        {
            Destroy(currentPreviewObject);
            currentPreviewObject = null;
        }

        Debug.Log($"現在選択中のプレハブ: {buildablePrefabs[currentPrefabIndex].name}");
    }

    void PlaceBuilding()
    {
        Debug.Log("PlaceBuilding");

        if (currentPreviewObject != null)
        {
            // オーバーラップチェック
            Collider[] overlappingColliders = Physics.OverlapSphere(
                currentPreviewObject.transform.position,
               overlapCheckRadius,
                buildingLayer
            );

            // 重なっているオブジェクトがある場合は設置をキャンセル
            if (overlappingColliders.Length > 0)
            {
                Debug.Log("建築場所に他のオブジェクトが重なっているため、設置できません。");
                return;
            }

            // プレビューオブジェクトを実際の建築物に変換
            Renderer[] renderers = currentPreviewObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                Material placedMaterial = new Material(renderer.material);
                Color color = placedMaterial.color;
                color.a = 1f;
                placedMaterial.color = color;
                renderer.material = placedMaterial;
            }

            // コリジョンを再有効化
            Collider[] colliders = currentPreviewObject.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }
            Debug.Log($"建築を設置しました！");

            currentPreviewObject = null;
        }
    }
}
