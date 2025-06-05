
using UnityEngine;
using UnityEngine.UI;

public class SwipeImage : MonoBehaviour
{
    [SerializeField] private Image[] images; // スワイプする画像の配列
    private int currentIndex = 0; // 現在表示している画像のインデックス
    private Vector2 startTouchPosition; // タッチの開始位置

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position; // タッチの開始位置を記録
                    break;

                case TouchPhase.Ended:
                    Vector2 endTouchPosition = touch.position; // タッチの終了位置
                    float swipeDistance = endTouchPosition.x - startTouchPosition.x; // スワイプの距離

                    if (swipeDistance > 50) // 右にスワイプ
                    {
                        ShowPreviousImage();
                    }
                    else if (swipeDistance < -50) // 左にスワイプ
                    {
                        ShowNextImage();
                    }
                    break;
            }
        }
    }

    private void ShowNextImage()
    {
        currentIndex++;
        if (currentIndex >= images.Length)
        {
            currentIndex = 0; // 最初の画像に戻る
        }
        UpdateImageDisplay();
    }

    private void ShowPreviousImage()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = images.Length - 1; // 最後の画像に戻る
        }
        UpdateImageDisplay();
    }

    private void UpdateImageDisplay()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(i == currentIndex); // 現在の画像だけを表示
        }
    }

}