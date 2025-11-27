/*
using System.Collections;
using UnityEngine;

public class PlayerTransformation : MonoBehaviour
{
    [Header("モデルの設定")]
    public GameObject normalModel; // 通常時のモデル

    [Header("変身時間")]
    public float transformationDuration = 10.0f;

    private Coroutine transformationCoroutine;
    private GameObject currentActiveModel; // 現在アクティブなモデルを記憶

    void Start()
    {
        // 最初は通常モデルがアクティブ
        currentActiveModel = normalModel;
    }

    // アイテム側から呼び出される公開メソッド
    public void StartTransformation(GameObject modelToTransformInto)
    {
        if (transformationCoroutine != null)
        {
            StopCoroutine(transformationCoroutine);
        }
        transformationCoroutine = StartCoroutine(TransformSequence(modelToTransformInto));
    }

    private IEnumerator TransformSequence(GameObject targetModel)
    {
        // 1. 現在のモデルを非表示にする
        currentActiveModel.SetActive(false);

        // 2. 新しいモデル（変身先）を表示する
        targetModel.SetActive(true);
        currentActiveModel = targetModel; // 現在のモデルを更新
        Debug.Log(targetModel.name + " に変身！");

        // 3. 指定時間待つ
        yield return new WaitForSeconds(transformationDuration);

        // 4. 変身後のモデルを非表示にする
        currentActiveModel.SetActive(false);

        // 5. 通常モデルに戻す
        normalModel.SetActive(true);
        currentActiveModel = normalModel; // 現在のモデルを通常に戻す
        Debug.Log("元の姿に戻った。");

        transformationCoroutine = null;
    }
}*/
/*
using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class PlayerTransformation : MonoBehaviour
{
    // ...（他の変数は変更なし）...
    [Header("Cinemachine")]
    public CinemachineCamera MainCamera;
    [Header("モデルの設定")]
    public GameObject normalModel;
    [Header("変身設定")]
    public float transformationDuration = 10.0f;
    public float verticalOffset = 0.2f; // ★変身時にY座標をずらす量

    private Coroutine transformationCoroutine;
    private GameObject currentActiveModel;

    void Start()
    {
        currentActiveModel = normalModel;
    }

    public void StartTransformation(GameObject modelToActivate)
    {
        if (transformationCoroutine != null)
        {
            StopCoroutine(transformationCoroutine);
        }
        transformationCoroutine = StartCoroutine(TransformSequence(modelToActivate));
    }

    private IEnumerator TransformSequence(GameObject targetModel)
    {
        // --- 変身開始 ---

        // 1. 現在の位置と回転を記憶
        Vector3 position = currentActiveModel.transform.position;
        Quaternion rotation = currentActiveModel.transform.rotation;

        // 2. 古いモデルを非表示にする
        currentActiveModel.SetActive(false);

        // 3. 変身先モデルの位置と回転を設定
        // ★Y座標からオフセット分だけ引いた位置に設定
        targetModel.transform.position = new Vector3(position.x, position.y - verticalOffset, position.z);
        targetModel.transform.rotation = rotation;

        // 4. 新しいモデルを表示し、操作を有効にする
        targetModel.SetActive(true);
        currentActiveModel = targetModel;
        MoveFish fishController = currentActiveModel.GetComponent<MoveFish>();
        if (fishController != null)
        {
            Debug.Log("taNakaakakakkakakaka");
            fishController.enabled = true;
        }

        // 5. カメラのターゲットを更新
        if (MainCamera != null)
        {
            MainCamera.Follow = currentActiveModel.transform;
            MainCamera.LookAt = currentActiveModel.transform;
        }

        // --- 時間待機 ---
        yield return new WaitForSeconds(transformationDuration);

        // --- 変身解除 ---

        // 1. 現在の位置と回転を記憶
        position = currentActiveModel.transform.position;
        rotation = currentActiveModel.transform.rotation;

        // 2. 変身後モデルを非表示・無効化
        if (fishController != null)
        {
            fishController.enabled = false;
        }
        currentActiveModel.SetActive(false);

        // 3. 通常モデルの位置と回転を設定
        // ★元の高さに戻すため、Y座標にオフセット分を足す
        normalModel.transform.position = new Vector3(position.x, position.y + verticalOffset, position.z);
        normalModel.transform.rotation = rotation;

        // 4. 通常モデルを表示
        Debug.Log("adaadadaadaad");
        normalModel.SetActive(true);
        currentActiveModel = normalModel;

        // 5. カメラのターゲットを更新
        if (MainCamera != null)
        {
            MainCamera.Follow = currentActiveModel.transform;
            MainCamera.LookAt = currentActiveModel.transform;
        }

        transformationCoroutine = null;
    }
}*/
/*
using UnityEngine;
using Unity.Cinemachine;

public class PlayerTransformation : MonoBehaviour
{
    [Header("Cinemachine")]
    public CinemachineCamera MainCamera;

    [Header("変身設定")]
    public float transformationDuration = 10.0f;
    public float verticalOffset = 0.2f;

    private GameObject normalModel;
    private GameObject currentActiveModel;
    private MoveFish normalController; // ★通常時の操作スクリプトを管理

    void Start()
    {
        normalModel = this.gameObject;
        currentActiveModel = normalModel;
        
        // ★自分にアタッチされている通常コントローラーを取得
        normalController = GetComponent<MoveFish>();
    }

    public void StartTransformation(GameObject modelToActivate)
    {
        // --- 変身処理 ---
        
        // ★通常コントローラーを無効化
        if (normalController != null)
        {
            normalController.enabled = false;
        }

        Vector3 position = currentActiveModel.transform.position;
        Quaternion rotation = currentActiveModel.transform.rotation;
        currentActiveModel.SetActive(false);

        modelToActivate.transform.position = new Vector3(position.x, position.y - verticalOffset, position.z);
        modelToActivate.transform.rotation = rotation;

        modelToActivate.SetActive(true);
        currentActiveModel = modelToActivate;
        
        // 変身後コントローラーを有効化
        MoveFish fishController = currentActiveModel.GetComponent<MoveFish>();
        if (fishController != null)
        {
            fishController.enabled = true;
        }
        
        if (MainCamera != null)
        {
            MainCamera.Follow = currentActiveModel.transform;
            MainCamera.LookAt = currentActiveModel.transform;
        }

        TransformationTimer.Instance.StartTransformationTimer(this, transformationDuration);
    }

    public void RevertTransformation()
    {
        // --- 変身解除処理 ---
        MoveFish fishController = currentActiveModel.GetComponent<MoveFish>();
        if (fishController != null)
        {
            fishController.enabled = false;
        }

        Vector3 position = currentActiveModel.transform.position;
        Quaternion rotation = currentActiveModel.transform.rotation;
        currentActiveModel.SetActive(false);

        normalModel.transform.position = new Vector3(position.x, position.y + verticalOffset, position.z);
        normalModel.transform.rotation = rotation;
        
        normalModel.SetActive(true);
        currentActiveModel = normalModel;

        // ★通常コントローラーを有効化
        if (normalController != null)
        {
            normalController.enabled = true;
        }

        if (MainCamera != null)
        {
            MainCamera.Follow = currentActiveModel.transform;
            MainCamera.LookAt = currentActiveModel.transform;
        }
    }
}*/
/*
using UnityEngine;
using Unity.Cinemachine;

public class PlayerTransformation : MonoBehaviour
{
    [Header("Cinemachine")]
    public CinemachineCamera MainCamera;

    [Header("変身設定")]
    public float transformationDuration = 10.0f;
    public float verticalOffset = 0.2f;

    private GameObject normalModel;
    private GameObject currentActiveModel;
    private MoveFish normalController; // 通常時の操作スクリプトを管理

    void Start()
    {
        // このスクリプトがアタッチされているGameObject自身が通常モデル
        normalModel = this.gameObject;
        currentActiveModel = normalModel;
        
        // 自分にアタッチされている通常コントローラーを取得
        normalController = GetComponent<MoveFish>();
    }

    // 外部（アイテムなど）から呼び出される変身開始メソッド
    public void StartTransformation(GameObject modelToActivate)
    {
        // --- 変身処理 ---
        
        // 通常コントローラーを無効化
        if (normalController != null)
        {
            normalController.enabled = false;
        }

        Vector3 position = currentActiveModel.transform.position;
        Quaternion rotation = currentActiveModel.transform.rotation;
        currentActiveModel.SetActive(false);

        // 変身先モデルの位置を設定（垂直オフセット適用）
        modelToActivate.transform.position = new Vector3(position.x, position.y - verticalOffset, position.z);
        modelToActivate.transform.rotation = rotation;

        modelToActivate.SetActive(true);
        currentActiveModel = modelToActivate;
        
        // 変身後コントローラーを有効化
        MoveFish fishController = currentActiveModel.GetComponent<MoveFish>();
        if (fishController != null)
        {
            fishController.enabled = true;
        }
        
        if (MainCamera != null)
        {
            MainCamera.Follow = currentActiveModel.transform;
            MainCamera.LookAt = currentActiveModel.transform;
        }

        // 変身タイマーを開始 (TransformationTimerクラスが必要)
        TransformationTimer.Instance.StartTransformationTimer(this, transformationDuration);
    }

    // 変身解除メソッド（タイマーまたはゴール判定から呼び出される）
    public void RevertTransformation()
    {
        // ★追加: タイマーが動いている場合は停止する
        //   これにより、タイマー切れとゴール判定による二重解除を防ぐ
        if (TransformationTimer.Instance != null)
        {
            // TransformationTimerクラスにStopTransformationTimerメソッドが実装されていると仮定
            TransformationTimer.Instance.StopTransformationTimer();
        }

        // --- 変身解除処理 ---
        
        // 変身後コントローラーを無効化
        MoveFish fishController = currentActiveModel.GetComponent<MoveFish>();
        if (fishController != null)
        {
            fishController.enabled = false;
        }

        Vector3 position = currentActiveModel.transform.position;
        Quaternion rotation = currentActiveModel.transform.rotation;
        currentActiveModel.SetActive(false);

        // 通常モデルの位置を設定（垂直オフセットを元に戻す）
        normalModel.transform.position = new Vector3(position.x, position.y + verticalOffset, position.z);
        normalModel.transform.rotation = rotation;
        
        normalModel.SetActive(true);
        currentActiveModel = normalModel;

        // 通常コントローラーを有効化
        if (normalController != null)
        {
            normalController.enabled = true;
        }

        if (MainCamera != null)
        {
            MainCamera.Follow = currentActiveModel.transform;
            MainCamera.LookAt = currentActiveModel.transform;
        }
    }
}*/

using UnityEngine;
using Unity.Cinemachine;

public class PlayerTransformation : MonoBehaviour
{
    [Header("Cinemachine")]
    public CinemachineCamera MainCamera;

    [Header("変身設定")]
    public float transformationDuration = 10.0f;
    // verticalOffsetは魚ごとのFishTransformationDataから取得するため削除

    private GameObject normalModel;
    private GameObject currentActiveModel;
    private MoveFish normalController;

    // 現在アクティブな変身のオフセット値を保存する変数
    private float activeVerticalOffset = 0f; 

    void Start()
    {
        // このスクリプトがアタッチされているGameObject自身が通常モデル
        normalModel = this.gameObject;
        currentActiveModel = normalModel;
        
        // 自分にアタッチされている通常コントローラーを取得
        normalController = GetComponent<MoveFish>();
    }

    // 外部（アイテムなど）から呼び出される変身開始メソッド
    public void StartTransformation(GameObject modelToActivate)
    {
        // --- 変身処理 ---
        
        // 1. 変身先モデルから専用のオフセット値を取得し、保存する
        FishTransformationData fishData = modelToActivate.GetComponent<FishTransformationData>();
        if (fishData != null)
        {
            activeVerticalOffset = fishData.verticalOffset;
        } 
        else
        {
            activeVerticalOffset = 0f; // データがない場合はオフセットなし
        }

        // 通常コントローラーを無効化
        if (normalController != null)
        {
            normalController.enabled = false;
        }

        Vector3 position = currentActiveModel.transform.position;
        Quaternion rotation = currentActiveModel.transform.rotation;
        currentActiveModel.SetActive(false);

        // 変身先モデルの位置を設定（保存したオフセット適用）
        modelToActivate.transform.position = new Vector3(position.x, position.y - activeVerticalOffset, position.z);
        modelToActivate.transform.rotation = rotation;

        modelToActivate.SetActive(true);
        currentActiveModel = modelToActivate;
        
        // 変身後コントローラーを有効化
        MoveFish fishController = currentActiveModel.GetComponent<MoveFish>();
        if (fishController != null)
        {
            fishController.enabled = true;
        }
        
        if (MainCamera != null)
        {
            MainCamera.Follow = currentActiveModel.transform;
            MainCamera.LookAt = currentActiveModel.transform;
        }

        // 変身タイマーを開始 (TransformationTimerクラスが必要)
        if (TransformationTimer.Instance != null)
        {
            TransformationTimer.Instance.StartTransformationTimer(this, transformationDuration);
        }
    }

    // 変身解除メソッド（タイマーまたはゴール判定から引数なしで呼び出される）
    public void RevertTransformation()
    {
        // タイマーが動いている場合は停止する
        if (TransformationTimer.Instance != null)
        {
            TransformationTimer.Instance.StopTransformationTimer();
        }

        // --- 変身解除処理 ---
        
        // 変身後コントローラーを無効化
        MoveFish fishController = currentActiveModel.GetComponent<MoveFish>();
        if (fishController != null)
        {
            fishController.enabled = false;
        }

        Vector3 position = currentActiveModel.transform.position;
        Quaternion rotation = currentActiveModel.transform.rotation;
        currentActiveModel.SetActive(false);

        // 通常モデルの位置を設定（保存したオフセットを元に戻す）
        normalModel.transform.position = new Vector3(position.x, position.y + activeVerticalOffset, position.z);
        normalModel.transform.rotation = rotation;
        
        normalModel.SetActive(true);
        currentActiveModel = normalModel;

        // 通常コントローラーを有効化
        if (normalController != null)
        {
            normalController.enabled = true;
        }

        if (MainCamera != null)
        {
            MainCamera.Follow = currentActiveModel.transform;
            MainCamera.LookAt = currentActiveModel.transform;
        }
        
        // オフセット値をリセット
        activeVerticalOffset = 0f;
    }
}