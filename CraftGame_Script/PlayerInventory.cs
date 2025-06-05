
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public Dictionary<string, int> craftInventry = new Dictionary<string, int>();
    public GameObject inventoryUI; // インベントリUI
    private bool isInventoryOpen = false;
    public Sprite[] sprite;
    public SlotManager[] slots; // スロットを管理する配列
    public ItemCrafting itemCrafting;//ItemCrafting スクリプトの参照

    public bool IsInventoryOpen => isInventoryOpen;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }
    public void AddCraftItem(string itemName, int quantity)
    {
        // アイテムをインベントリに追加
        if (craftInventry.ContainsKey(itemName))
        {
            craftInventry[itemName] += quantity;
        }
        else
        {
            craftInventry[itemName] = quantity;
        }
        Debug.Log($"{itemName}を{quantity}個取得！ 現在の数: {craftInventry[itemName]}");

        GameLogManager.instance.AddLog($"{itemName}を{quantity}個取得！ 現在の数: {craftInventry[itemName]}");/////

        // スロットを更新
        UpdateInventoryUI();
    }
    public void AddItem(string itemName, int quantity)
    {
        // アイテムをインベントリに追加
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName] += quantity;
        }
        else
        {
            inventory[itemName] = quantity;
        }
        Debug.Log($"{itemName}を{quantity}個取得！ 現在の数: {inventory[itemName]}");

        GameLogManager.instance.AddLog($"{itemName}を{quantity}個取得！ 現在の数: {inventory[itemName]}");/////

        // スロットを更新
        UpdateInventoryUI();
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            PauseGame();
            UpdateInventoryUI(); // 開いたときにスロットを更新
        }
        else
        {
            ResumeGame();
        }
    }

    private void UpdateInventoryUI()
    {
        int index = 0;

        foreach (var item in inventory)
        {
            if (index < slots.Length)
            {
                slots[index].itemName = item.Key; // スロットにアイテム名を設定
                Sprite itemSprite = GetItemSprite(item.Key); // アイテム名に対応するスプライトを取得
                slots[index].UpdateSlot(itemSprite, item.Value); // スロットを更新
                index++;
            }
        }

        // 残りのスロットを空にする
        for (int i = index; i < slots.Length; i++)
        {
            slots[i].itemName = null; // アイテム名をクリア
            slots[i].UpdateSlot(null, 0); // 空の状態にリセット
        }
    }

    // アイテム名に対応するスプライトを取得する仮のメソッド
    private Sprite GetItemSprite(string itemName)
    {
        // 実際の実装では、アイテム名に基づいてスプライトを返すロジックを追加してください
        if (itemName == "木材")
        {
            return sprite[0];
        }
        if (itemName == "薬草")
        {
            return sprite[1];
        }
        if (itemName == "石")
        {
            return sprite[2];
        }
        if (itemName == "ゴーレムの残骸")
        {
            return sprite[3];
        }

        return null; // 仮のスプライト（未実装）
    }


    private void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
