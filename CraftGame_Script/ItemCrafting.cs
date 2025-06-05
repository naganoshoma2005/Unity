
using UnityEngine;
using System.Collections.Generic;

public class ItemCrafting : MonoBehaviour
{
    public PlayerInventory playerInventory; // PlayerInventoryへの参照
    public Dictionary<string, Dictionary<string, int>> craftingRecipes = new Dictionary<string, Dictionary<string, int>>(); // レシピを公開

    void Start()
    {
        InitializeCraftingRecipes();
    }

    private void InitializeCraftingRecipes()
    {
        craftingRecipes["ポーション"] = new Dictionary<string, int>
        {
            {"薬草", 2}
        };

        craftingRecipes["木の斧"] = new Dictionary<string, int>
        {
            {"木材", 4},
        };

        craftingRecipes["鉄の剣"] = new Dictionary<string, int>
        {
            {"鉄", 5},
            {"木材", 1}
        };
    }

    public bool CraftItem(string itemName)
    {
        if (!craftingRecipes.ContainsKey(itemName))
        {
            Debug.Log("このアイテムはクラフトできません。");
            return false;
        }

        var requiredMaterials = craftingRecipes[itemName];

        foreach (var material in requiredMaterials)
        {
            if (!playerInventory.inventory.ContainsKey(material.Key) || playerInventory.inventory[material.Key] < material.Value)
            {
                Debug.Log($"{material.Key}が足りません！");
                return false;
            }
        }

        foreach (var material in requiredMaterials)
        {
            playerInventory.inventory[material.Key] -= material.Value;
            if (playerInventory.inventory[material.Key] <= 0)
            {
                playerInventory.inventory.Remove(material.Key);
            }
        }

        playerInventory.AddCraftItem(itemName, 1);
        Debug.Log($"{itemName}をクラフトしました！");
        return true;
    }
}
