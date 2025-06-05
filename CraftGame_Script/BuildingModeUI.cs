
using UnityEngine;

public class BuildingModeUI : MonoBehaviour
{
    [Header("Mode UI References")]
    [SerializeField] private GameObject normalModeUI;    // 通常モード時のUI
    [SerializeField] private GameObject buildingModeUI;  // 建築モード時のUI

    private BuildingSystem buildingSystem;
    private bool initialized = false;

    private void Start()
    {
        // BuildingSystemコンポーネントの取得
        buildingSystem = FindObjectOfType<BuildingSystem>();
        if (buildingSystem == null)
        {
            Debug.LogError("BuildingSystem not found in the scene!");
            return;
        }

        // 初期状態を設定
        initialized = true;
        SetBuildingMode(false);
    }

    public void SetBuildingMode(bool isBuildingMode)
    {
        if (!initialized) return;

        Debug.Log($"Setting UI building mode: {isBuildingMode}");

        // 通常モードUIと建築モードUIの切り替え
        if (normalModeUI != null)
        {
            normalModeUI.SetActive(!isBuildingMode);
            Debug.Log($"Normal mode UI active: {!isBuildingMode}");
        }

        if (buildingModeUI != null)
        {
            buildingModeUI.SetActive(isBuildingMode);
            Debug.Log($"Building mode UI active: {isBuildingMode}");
        }
    }
}