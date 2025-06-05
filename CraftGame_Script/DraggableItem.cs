using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public SlotManager.ItemType itemType; // アイテムの種類
    public Image itemImage;
    public Sprite itemSprite; // アイテムの画像

    private Transform originalParent;
    private Vector3 originalPosition;

    public void Start()
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
        //transform.SetParent(transform.root);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
      
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
      　 ReturnToOriginalPosition(); // 位置も元に戻す
    }

    public void ReturnToOriginalPosition()
    {
        Debug.Log("adafadsfafaafaafaf");
        transform.position = originalPosition;
    }
}