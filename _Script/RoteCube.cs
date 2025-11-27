using UnityEngine;

public class RoteCube : MonoBehaviour
{
    // 公開変数として回転速度を定義
    // UnityのInspectorウィンドウから値を変更できるようになる
    public float rotationSpeedX = 50f;
    public float rotationSpeedY = 50f;
    public float rotationSpeedZ = 50f;

    // フレームごとに呼ばれる関数
    void Update()
    {
        // 現在のTransformコンポーネントを取得
        Transform cubeTransform = this.transform;

        // X軸周りの回転を適用
        // Time.deltaTimeは、前回のフレームからの経過時間
        // これを使うことで、フレームレートに依存しないスムーズな回転が可能になる
        cubeTransform.Rotate(Vector3.right, rotationSpeedX * Time.deltaTime, Space.World);

        // Y軸周りの回転を適用
        cubeTransform.Rotate(Vector3.up, rotationSpeedY * Time.deltaTime, Space.World);

        // Z軸周りの回転を適用
        cubeTransform.Rotate(Vector3.forward, rotationSpeedZ * Time.deltaTime, Space.World);
    }
}
