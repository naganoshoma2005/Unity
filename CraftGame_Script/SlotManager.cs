/*
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public string itemName;        // スロットに関連付けるアイテム名
    public Image itemImage;        // スロットの中央に表示するアイテムの画像
    public Text quantityText;      // スロットの右下に表示する個数
    public GameObject itemUI;      // スロット全体のUI（常に表示される）

    private Color normalColor = Color.white;        // 通常のカラー（不透明）
    private Color semiTransparentColor = new Color(1, 1, 1, 0.5f); // 半透明のカラー



    // アイテムの情報を更新するメソッド
    public void UpdateSlot(Sprite itemSprite, int quantity)
    {
        // アイテム画像を更新
        if (itemImage != null)
        {
            if (itemSprite != null)
            {
                itemImage.sprite = itemSprite; // アイテム画像を設定
                itemImage.enabled = true;      // 画像を有効化
            }
            else
            {
                itemImage.enabled = true;     // アイテムがない場合は非表示  //本来はfalse
            }
        }

        // 個数を右下に表示
        if (quantityText != null)
        {
            quantityText.text = quantity > 0 ? quantity.ToString() : ""; // 個数を表示
        }

        // 個数が0の場合に半透明にする
        if (itemUI != null)
        {
            var uiColor = quantity > 0 ? normalColor : semiTransparentColor;
            SetUIAlpha(itemUI, uiColor); // UI全体の透明度を更新

        }
    }

    // UI全体の透明度を設定するヘルパーメソッド
    private void SetUIAlpha(GameObject uiObject, Color color)
    {
        // 子オブジェクトにあるすべてのImageを取得して透明度を適用
        foreach (var image in uiObject.GetComponentsInChildren<Image>())
        {
            image.color = color;
        }

        // 子オブジェクトにあるすべてのTextを取得して透明度を適用
        foreach (var text in uiObject.GetComponentsInChildren<Text>())
        {
            var originalColor = text.color;
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, color.a);
        }
    }
}*/


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotManager : MonoBehaviour, IDropHandler
{
    public enum ItemType { None, Weapon, Equipment, Other } // アイテムの種類

    public string itemName;
    public Image itemImage;
    public Text quantityText;
    public GameObject itemUI;

    public ItemType acceptedType; // 受け入れ可能なアイテムタイプ

    private Color normalColor = Color.white;
    private Color semiTransparentColor = new Color(1, 1, 1, 0.5f);

    public void UpdateSlot(Sprite itemSprite, int quantity)
    {
        if (itemImage != null)
        {
            if (itemSprite != null)
            {
                itemImage.sprite = itemSprite;
                itemImage.enabled = true;
            }
            else
            {
                itemImage.enabled = false;
            }
        }

        if (quantityText != null)
        {
            quantityText.text = quantity > 0 ? quantity.ToString() : "";
        }

        if (itemUI != null)
        {
            var uiColor = quantity > 0 ? normalColor : semiTransparentColor;
            SetUIAlpha(itemUI, uiColor);
        }
    }

    private void SetUIAlpha(GameObject uiObject, Color color)
    {
        foreach (var image in uiObject.GetComponentsInChildren<Image>())
        {
            image.color = color;
        }

        foreach (var text in uiObject.GetComponentsInChildren<Text>())
        {
            var originalColor = text.color;
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, color.a);
        }
    }

    // ドラッグされたアイテムをドロップする処理
    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem draggedItem = eventData.pointerDrag?.GetComponent<DraggableItem>();
        if (draggedItem != null)
        {
          
            // ドロップ先のスロットが受け入れ可能かチェック
            if (draggedItem.itemType == acceptedType)
            {
                // アイテムを受け入れる処理（アイテム更新など）
                UpdateSlot(draggedItem.itemSprite, 1);
                Destroy(draggedItem.gameObject); // ドラッグ元を削除（必要に応じて変更）
            }
            else
            {
                // 受け入れ不可の場合、元の位置に戻す
                draggedItem.ReturnToOriginalPosition();
            }
        }
    }
}
