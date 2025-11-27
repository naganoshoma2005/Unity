using UnityEngine;

// 変身先の魚のモデルにアタッチして、魚ごとのオフセット値を設定するためのスクリプト
public class FishTransformationData : MonoBehaviour
{
    [Tooltip("変身時に適用する垂直オフセット値。この魚特有の値。")]
    public float verticalOffset = 0.2f; 
}