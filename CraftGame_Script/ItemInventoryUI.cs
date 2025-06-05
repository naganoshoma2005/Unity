
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventoryUI : MonoBehaviour
{
    [System.Serializable]
    public class InventorySlot
    {
        public Item assignedItem;     // このスロットに割り当てられたアイテム
        public Image itemIcon;        // アイテム画像を表示
        public Text itemCountText;    // アイテムの数を表示
        public Button useButton;      // 使用ボタン
        public Image selectionFrame;  // 選択枠表示用
    }

    public List<InventorySlot> slots = new List<InventorySlot>(); // スロットリスト
    private Dictionary<Item, int> itemCounts = new Dictionary<Item, int>(); // アイテムの数を管理
    private int selectedSlotIndex = 0; // 現在選択中のスロットインデックス

    // マウスホイールの感度調整用（必要に応じて Inspector で調整可能）
    [SerializeField]
    private float scrollSensitivity = 1.0f;

    private void Start()
    {
       
        // 各スロットの初期化
        foreach (var slot in slots)
        {
            if (slot == null)
            {
                Debug.LogError("スロットが設定されていません！");
                continue;
            }

            if (slot.assignedItem != null)
            {
                InitializeSlot(slot);
            }
            else
            {
                Debug.LogWarning("スロットに割り当てられたアイテムがありません。");
                SetEmptySlot(slot);
            }
        }

        // 最初のスロットを選択状態に
        UpdateSelection(0);
    }

    private void Update()
    {
        // マウスホイールの入力を検出
        float scrollDelta = Input.mouseScrollDelta.y * scrollSensitivity;

        if (scrollDelta != 0 && slots.Count > 0)
        {
            if (scrollDelta > 0)
            {
                // 上にスクロールした場合、前のスロットへ
                selectedSlotIndex--;
                if (selectedSlotIndex < 0)
                    selectedSlotIndex = slots.Count - 1;
            }
            else
            {
                // 下にスクロールした場合、次のスロットへ
                selectedSlotIndex++;
                if (selectedSlotIndex >= slots.Count)
                    selectedSlotIndex = 0;
            }

            UpdateSelection(selectedSlotIndex);
        }
    }

    /// <summary>
    /// 選択状態を更新
    /// </summary>
    private void UpdateSelection(int newIndex)
    {
        // すべてのスロットの選択表示をリセット
        foreach (var slot in slots)
        {
            if (slot.selectionFrame != null)
            {
                slot.selectionFrame.enabled = false;
            }
        }

        // 新しく選択したスロットの選択表示を有効化
        if (slots[newIndex].selectionFrame != null)
        {
            slots[newIndex].selectionFrame.enabled = true;
        }

        selectedSlotIndex = newIndex;
    }

    /// <summary>
    /// 選択中のアイテムを使用
    /// </summary>
    public void UseSelectedItem()
    {
        if (selectedSlotIndex >= 0 && selectedSlotIndex < slots.Count)
        {
            var selectedSlot = slots[selectedSlotIndex];
            if (selectedSlot.assignedItem != null)
            {
                UseItem(selectedSlot.assignedItem);
            }
        }
    }

    /// <summary>
    /// スロットを初期化する
    /// </summary>
    private void InitializeSlot(InventorySlot slot)
    {
        if (slot.itemIcon == null || slot.itemCountText == null || slot.useButton == null)
        {
            Debug.LogError($"スロット {slot.assignedItem.name} に必要なコンポーネントが設定されていません！");
            return;
        }

        if (slot.assignedItem.icon == null)
        {
            Debug.LogError($"アイテム {slot.assignedItem.name} にアイコン(Sprite)が設定されていません！");
            return;
        }

        // まずアイテムをディクショナリに追加
        itemCounts[slot.assignedItem] = 0; // 初期値は 0

        // アイテムの初期化
        slot.itemIcon.sprite = slot.assignedItem.icon;
        slot.itemIcon.color = Color.white; // 完全不透明で表示
        slot.itemIcon.enabled = true;
        slot.itemCountText.text = "0"; // 初期値は 0
        slot.itemCountText.enabled = true;
        slot.useButton.gameObject.SetActive(true); // ボタンは常に表示

        // 選択枠の初期設定
        if (slot.selectionFrame != null)
        {
            slot.selectionFrame.enabled = false;
        }
    }

    /// <summary>
    /// 空のスロットを設定
    /// </summary>
    private void SetEmptySlot(InventorySlot slot)
    {
        slot.itemIcon.color = Color.white; // 完全不透明
        slot.itemIcon.enabled = true;
        slot.itemCountText.text = "0"; // 数は 0 として表示
        slot.itemCountText.enabled = true;
        slot.useButton.gameObject.SetActive(true); // ボタンは常に表示

        // 選択枠の初期設定
        if (slot.selectionFrame != null)
        {
            slot.selectionFrame.enabled = false;
        }
    }

    /// <summary>
    /// アイテムを追加
    /// </summary>
    public void AddItem(Item item, int count)
    {
        if (itemCounts.ContainsKey(item))
        {
            itemCounts[item] += count;
            UpdateSlotUI(GetSlotForItem(item));
        }
        else
        {
            Debug.LogWarning($"アイテム {item.name} はどのスロットにも割り当てられていません！");
        }
    }

    /// <summary>
    /// アイテムを使用
    /// </summary>
    public void UseItem(Item item)
    {
        if (itemCounts.ContainsKey(item) && itemCounts[item] > 0)
        {
            itemCounts[item]--;
            UpdateSlotUI(GetSlotForItem(item));
        }
        else
        {
            Debug.LogWarning($"アイテム {item.name} は使用できません（残り: 0）");
        }
    }

    /// <summary>
    /// スロットの UI を更新
    /// </summary>
    private void UpdateSlotUI(InventorySlot slot)
    {
        if (slot != null && slot.assignedItem != null)
        {
            int count = itemCounts[slot.assignedItem];
            slot.itemCountText.text = count.ToString();
            slot.itemCountText.enabled = true;

            // アイテム数が0の場合は半透明、それ以外は不透明に設定
            //slot.itemIcon.color = new Color(1, 1, 1, count > 0 ? 1f : 0.5f);
            slot.itemIcon.color = new Color(1, 0, 0, 0.5f); // 赤色で半透明に設定
        }
        
    }

    /// <summary>
    /// 指定アイテムに対応するスロットを取得
    /// </summary>
    private InventorySlot GetSlotForItem(Item item)
    {
        foreach (var slot in slots)
        {
            if (slot.assignedItem == item)
            {
                return slot;
            }
        }
        return null;
    }

    /// <summary>
    /// 現在選択中のスロットインデックスを取得
    /// </summary>
    public int GetSelectedSlotIndex()
    {
        return selectedSlotIndex;
    }

    /// <summary>
    /// 現在選択中のアイテムを取得
    /// </summary>
    public Item GetSelectedItem()
    {
        if (selectedSlotIndex >= 0 && selectedSlotIndex < slots.Count)
        {
            return slots[selectedSlotIndex].assignedItem;
        }
        return null;
    }
}

[System.Serializable]
public class Item
{
    public string name;  // アイテム名
    public Sprite icon;  // アイテム画像
}