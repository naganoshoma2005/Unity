/*using UnityEngine;

public class HideSpecificObject : MonoBehaviour
{
    public GameObject targetObject; // 非表示にしたいオブジェクト

    void Start()
    {
        // 特定のオブジェクトを非表示にする
        targetObject.SetActive(false);
    }
    public void Update()
    {
        if(targetObject != false)
        {
            targetObject.SetActive(true);
        }
    }
}*/

/*/using UnityEngine;

public class HideSpecificObject : MonoBehaviour
{
    public Manager manager;
    public GameObject targetObject; // 非表示にしたいオブジェクト
    public int targetnumber;        // 表示にするためにチェックするタグ

    void Start()
    {
        manager = FindFirstObjectByType<Manager>();
        // 特定のオブジェクトを最初に非表示にする
        targetObject.SetActive(false);
    }

    private void Update()
    {
        if (manager != null)
        {
            Debug.LogError("manager null!!!");

            if (manager.ObjectNumber == targetnumber)
            {
                targetObject.SetActive(true);
            }
            else
            {
                targetObject.SetActive(false);
            }
        }
    }
}*/



