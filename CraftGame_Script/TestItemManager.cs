
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class TestItemManager : MonoBehaviour
{
    [System.Serializable]
    public class InventorySlot
    {
        public string assignedItem;     // このスロットに割り当てられたアイテム
        public Sprite itemIcon;         // アイテム画像
        public Text itemCountText;      // アイテムの数表示
        public GameObject itembutton;   // アイテムボタン
        public string effectType;       // アイテムの効果タイプ (例: "Heal", "Buff" など)
    }

    public PlayerInventory playerInventory;
    public List<InventorySlot> slots = new List<InventorySlot>(); // スロットリスト
    public List<int> itemIDs;
    public int selectedItemIndex = 0;
    public GameObject test;
    private RectTransform rt;
    public bool ItemCanvas;

    public BuildingSystem buildingSystem; // BuildingSystem への参照

    // PlayerHealth の参照をインスペクターから設定
    [SerializeField] private PlayerHP playerHP;

    [Obsolete]
    private void Start()
    {
        rt = test.GetComponent<RectTransform>();
        itemIDs = new List<int>(new int[39]); // 39個の要素で初期化

        foreach (InventorySlot slot in slots)
        {
            Image slotImg = slot.itembutton.GetComponent<Image>();
            slotImg.sprite = slot.itemIcon;
        }

        ItemCanvas = false;

        // BuildingSystem が設定されていない場合、自動取得
        if (buildingSystem == null)
        {
            buildingSystem = FindObjectOfType<BuildingSystem>();
        }

        // PlayerHealth が未設定の場合、Player オブジェクトから取得
        if (playerHP == null)
        {
            GameObject playerObject = GameObject.Find("Player");
            if (playerObject != null)
            {
                playerHP = playerObject.GetComponent<PlayerHP>();
            }
        }
    }

    void Update()
    {
        if (selectedItemIndex < 0)
        {
            test.SetActive(false);
        }

        // マウスホイールのスクロール入力を検出
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // ビルドモードでない場合のみアイテム選択を許可
        if (!ItemCanvas && buildingSystem != null && !buildingSystem.isInBuildMode)
        {
            if (scrollInput > 0f) // 上にスクロール
            {
                ChangeSelectedItem(1);
            }
            else if (scrollInput < 0f) // 下にスクロール
            {
                ChangeSelectedItem(-1);
            }
        }

        foreach (InventorySlot slot in slots)
        {
            if (playerInventory.craftInventry.ContainsKey(slot.assignedItem))
            {
                slot.itemCountText.text = playerInventory.craftInventry[slot.assignedItem].ToString();
            }
        }

        // Eキーでアイテムを使用
        if (Input.GetKeyDown(KeyCode.E) && selectedItemIndex >= 0)
        {
            UseSelectedItem();
        }

    }

    // アイテムの選択を変更
    void ChangeSelectedItem(int direction)
    {
        selectedItemIndex += direction;

        // 範囲チェック
        if (selectedItemIndex < 0)
        {
            selectedItemIndex = slots.Count - 1;
        }
        else if (selectedItemIndex >= slots.Count)
        {
            selectedItemIndex = 0;
        }

        if (selectedItemIndex >= 0)
        {
            test.SetActive(true);
            rt.position = slots[selectedItemIndex].itembutton.GetComponent<RectTransform>().position;
        }
        else
        {
            test.SetActive(false);
        }

        Debug.Log($"選択されたアイテムインデックス: {selectedItemIndex}");
    }

    // 選択されたアイテムを使用する
    void UseSelectedItem()
    {
        InventorySlot selectedSlot = slots[selectedItemIndex];

        if (playerInventory.craftInventry.ContainsKey(selectedSlot.assignedItem) && !string.IsNullOrEmpty(selectedSlot.assignedItem))
        {
            Debug.Log($"アイテム '{selectedSlot.assignedItem}' を使用しました。");

            // アイテムの使用数を減らす
            if (playerInventory.craftInventry[selectedSlot.assignedItem] > 0)
            {
                playerInventory.craftInventry[selectedSlot.assignedItem]--;
                selectedSlot.itemCountText.text = playerInventory.craftInventry[selectedSlot.assignedItem].ToString();
            }

            // アイテムの効果を発動
            ApplyItemEffect(selectedSlot.effectType);
        }
        else
        {
            Debug.Log("アイテムがありません");
        }
    }

    // アイテムの効果を適用
    void ApplyItemEffect(string effectType)
    {
        if (playerHP == null)
        {
            Debug.LogWarning("PlayerHealth が設定されていません。");
            return;
        }

        switch (effectType)
        {
            case "Heal":
                int healAmount = 20;
                playerHP.Heal(healAmount);
                Debug.Log($"HPが{healAmount}回復しました。");
                break;

            // 他の効果を追加する場合
            // case "Buff":
            //     Debug.Log("バフを適用しました。");
            //     break;

            default:
                Debug.Log("効果がありません。");
                break;
        }
    }
}
