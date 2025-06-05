
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RecipeBookUI : MonoBehaviour
{
    public ItemCrafting itemCrafting; // クラフト機能の参照
    public Text recipeNameText; // アイテム名を表示するテキスト
    public Image recipeIcon; // アイテムのアイコン
    public Text requiredMaterialsText; // 必要アイテム数を表示するテキスト
    public Button craftButton; // クラフトボタン
    public Button nextButton; // 次のページボタン
    public Button prevButton; // 前のページボタン

    private List<string> recipeList = new List<string>(); // クラフト可能なアイテムリスト
    private int currentPage = 0; // 現在のページ番号

    void Start()
    {
        recipeList.Add("ポーション");
        recipeList.Add("木の斧");
        recipeList.Add("鉄の剣");

        UpdateRecipePage();
        
        craftButton.onClick.AddListener(CraftCurrentItem);
        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PrevPage);
    }

    private void UpdateRecipePage()
    {
        if (recipeList.Count == 0) return;

        string currentItem = recipeList[currentPage];
        recipeNameText.text = currentItem;
        Debug.Log(currentItem);
        recipeIcon.sprite = GetItemSprite(currentItem);

        // 必要素材のテキストを更新
        requiredMaterialsText.text = GetRequiredMaterialsText(currentItem);

        prevButton.interactable = (currentPage > 0);
        nextButton.interactable = (currentPage < recipeList.Count - 1);
    }

    private void NextPage()
    {
        if (currentPage < recipeList.Count - 1)
        {
            currentPage++;
            UpdateRecipePage();
        }
    }

    private void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateRecipePage();
        }
    }

    private void CraftCurrentItem()
    {
        string currentItem = recipeList[currentPage];
        bool success = itemCrafting.CraftItem(currentItem);

        if (success)
        {
            Debug.Log($"{currentItem} のクラフト成功！");
        }
        else
        {
            Debug.Log($"{currentItem} のクラフト失敗！（素材不足）");
        }
    }

    private string GetRequiredMaterialsText(string itemName)
    {
        if (!itemCrafting.craftingRecipes.ContainsKey(itemName))
        {
            return "レシピがありません";
        }

        var requiredMaterials = itemCrafting.craftingRecipes[itemName];
        string materialsText = "必要アイテム:\n";
        foreach (var material in requiredMaterials)
        {
            materialsText += $"{material.Key} x {material.Value}\n";
        }
        return materialsText;
    }

    private Sprite GetItemSprite(string itemName)
    {
        return null; // アイコン取得処理を後で追加
    }
}
