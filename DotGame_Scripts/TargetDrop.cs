using UnityEngine;
using UnityEngine.EventSystems;

public class TargetDrop : MonoBehaviour, IDropHandler
{

    //ドラッグされる

    public void OnDrop(PointerEventData eventData)
    {

        Debug.Log("--->  DropTarget OnDrop");
        // ドラッグされていたオブジェクトを取得
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject != null)
        {
            Debug.Log("ドロップされたオブジェクト: " + draggedObject.name);
            // 必要な処理をここに追加
            if(draggedObject.CompareTag("PowerUp"))
            {
                Shooter shooter = GetComponent<Shooter>();
                shooter.fireInterval /= 2;

            }

            /* MySpped ms = draggedObject.GetComponent<MySpped>();
            if (ms != null)
                Debug.Log("----> " + ms.speed); */
        }

    }
}