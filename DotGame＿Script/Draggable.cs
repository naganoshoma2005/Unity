
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    //ドラッグする
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector3 initialPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

    }
    void Start()
    {
        // 初期位置を保存
        initialPosition = transform.position;
    }

    // ドラッグの開始時
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("---> OnBeginDrag");
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false; // ドロップ先での判定を有効にするため無効化
    }

    // ドラッグ中の処理
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // マウスの動きに合わせてImageの位置を移動
    }

    // ドラッグ終了時
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("---> OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true; // 再びドロップ可能に
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

        // クリックした位置にRayを飛ばす
        RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);
        // 何かに当たったかどうかをチェック
        if (hit.collider != null)
        {
            GameObject selectobj = hit.collider.gameObject;
            Shooter shooter = selectobj.GetComponent<Shooter>();
            if(shooter != null)
            {
                shooter.Power();
                
            }

        }
        // マウス追尾が終わった後、一度だけ初期位置に戻す
        transform.position = initialPosition;
    }

    // ドロップ時の処理
    public void OnDrop(PointerEventData eventData)
    {
        
        Debug.Log("---> OnDrop");
        // ドラッグされていたオブジェクトを取得
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            Debug.Log("ドロップされたオブジェクト: " + draggedObject.name);
            // ここで取得したGameObjectに対して処理を追加できます
            // 例えば、ドロップされたオブジェクトの位置を変更するなど
         
        }
       }
}