/*
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class CollisionCounter : MonoBehaviour
{
    public Text countText;
    public Text countText2;

    // ゲームオブジェクトの参照
    public GameObject player;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject player5;
    public GameObject ally1;
    public GameObject ally2;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject ball;
    public GameObject Camera;

    // スタート位置・向きのプライベート変数
    private Vector3 playerStartPos;
    private Quaternion playerStartRot;
    private Vector3 player2StartPos;
    private Quaternion player2StartRot;
    private Vector3 player3StartPos;
    private Quaternion player3StartRot;
    private Vector3 player4StartPos;
    private Quaternion player4StartRot;
    private Vector3 player5StartPos;
    private Quaternion player5StartRot;
    private Vector3 ally1StartPos;
    private Quaternion ally1StartRot;
    private Vector3 ally2StartPos;
    private Quaternion ally2StartRot;
    private Vector3 enemy1StartPos;
    private Quaternion enemy1StartRot;
    private Vector3 enemy2StartPos;
    private Quaternion enemy2StartRot;
    private Vector3 enemy3StartPos;
    private Quaternion enemy3StartRot;
    private Vector3 CameraStartPos;
    private Quaternion CameraStartRot;
    private Vector3 ballStartPos;

    // CountdownTimerからアクセスできるようにpublicにする
    public int scoreTeam1 = 0; 
    public int scoreTeam2 = 0;

    void Start()
    {
        // --- 初期位置と向きを記録 ---
        if (player != null)
        {
            playerStartPos = player.transform.position;
            playerStartRot = player.transform.rotation;
        }

        if (player2 != null)
        {
            player2StartPos = player2.transform.position;
            player2StartRot = player2.transform.rotation;
        }

        if (player3 != null)
        {
            player3StartPos = player3.transform.position;
            player3StartRot = player3.transform.rotation;
        }

        if (player4 != null)
        {
            player4StartPos = player4.transform.position;
            player4StartRot = player4.transform.rotation;
        }
        
        if (player5 != null)
        {
            player5StartPos = player5.transform.position;
            player5StartRot = player5.transform.rotation;
        }
        
        if (ball != null)
        {
            ballStartPos = ball.transform.position;
        }

        if (enemy1 != null)
        {
            enemy1StartPos = enemy1.transform.position;
            enemy1StartRot = enemy1.transform.rotation;
        }
        
        if (enemy2 != null)
        {
            enemy2StartPos = enemy2.transform.position;
            enemy2StartRot = enemy2.transform.rotation;
        }
        
        if (enemy3 != null)
        {
            enemy3StartPos = enemy3.transform.position;
            enemy3StartRot = enemy3.transform.rotation;
        }
        
        if (ally1 != null)
        {
            ally1StartPos = ally1.transform.position;
            ally1StartRot = ally1.transform.rotation;
        }
        
        if (ally2 != null)
        {
            ally2StartPos = ally2.transform.position;
            ally2StartRot = ally2.transform.rotation;
        }
        
        if (Camera != null)
        {
            CameraStartPos = Camera.transform.position;
            CameraStartRot = Camera.transform.rotation;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        bool scored = false;
        
        if (collision.gameObject.CompareTag("Wall")) // ゴール1 → プレイヤーチームの得点
        {
            scoreTeam1++;
            if (countText != null) countText.text = scoreTeam1.ToString();
            scored = true;
        }
        else if (collision.gameObject.CompareTag("Wall2")) // ゴール2 → 敵チームの得点
        {
            scoreTeam2++;
            if (countText2 != null) countText2.text = scoreTeam2.ToString();
            scored = true;
        }

        if (scored)
        {
            ResetPositions();
        }
    }

    // すべてのオブジェクトを初期位置にリセットするメソッド
    void ResetPositions()
    {
        ResetObject(player, playerStartPos, playerStartRot);
        ResetObject(player2, player2StartPos, player2StartRot);
        ResetObject(player3, player3StartPos, player3StartRot);
        ResetObject(player4, player4StartPos, player4StartRot);
        ResetObject(player5, player5StartPos, player5StartRot);
        ResetObject(ball, ballStartPos, Quaternion.identity); // ボールは向きをリセットしない
        ResetObject(enemy1, enemy1StartPos, enemy1StartRot);
        ResetObject(enemy2, enemy2StartPos, enemy2StartRot);
        ResetObject(enemy3, enemy3StartPos, enemy3StartRot);
        ResetObject(ally1, ally1StartPos, ally1StartRot);
        ResetObject(ally2, ally2StartPos, ally2StartRot);
        
        // カメラは他のオブジェクトと異なるため個別に処理
        if (Camera != null)
        {
            Camera.transform.position = CameraStartPos;
            Camera.transform.rotation = CameraStartRot;
            Rigidbody cameraRb = Camera.GetComponent<Rigidbody>();
            if (cameraRb != null)
            {
                cameraRb.linearVelocity = Vector3.zero;
                cameraRb.angularVelocity = Vector3.zero;
            }
        }
    }

    // オブジェクトをリセットするヘルパー関数
    void ResetObject(GameObject obj, Vector3 pos, Quaternion rot)
    {
        if (obj == null) return;

        obj.transform.position = pos;
        obj.transform.rotation = rot;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}*/
/*
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class CollisionCounter : MonoBehaviour
{
    public Text countText;
    public Text countText2;

    // ★追加: PlayerTransformationスクリプトへの参照
    public PlayerTransformation playerTransformation;

    // ゲームオブジェクトの参照
    public GameObject player;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject player5;
    public GameObject ally1;
    public GameObject ally2;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject ball;
    public GameObject Camera;

    // スタート位置・向きのプライベート変数
    private Vector3 playerStartPos;
    private Quaternion playerStartRot;
    private Vector3 player2StartPos;
    private Quaternion player2StartRot;
    private Vector3 player3StartPos;
    private Quaternion player3StartRot;
    private Vector3 player4StartPos;
    private Quaternion player4StartRot;
    private Vector3 player5StartPos;
    private Quaternion player5StartRot;
    private Vector3 ally1StartPos;
    private Quaternion ally1StartRot;
    private Vector3 ally2StartPos;
    private Quaternion ally2StartRot;
    private Vector3 enemy1StartPos;
    private Quaternion enemy1StartRot;
    private Vector3 enemy2StartPos;
    private Quaternion enemy2StartRot;
    private Vector3 enemy3StartPos;
    private Quaternion enemy3StartRot;
    private Vector3 CameraStartPos;
    private Quaternion CameraStartRot;
    private Vector3 ballStartPos;

    // CountdownTimerからアクセスできるようにpublicにする
    public int scoreTeam1 = 0; 
    public int scoreTeam2 = 0;

    void Start()
    {
        // --- 初期位置と向きを記録 ---
        if (player != null)
        {
            playerStartPos = player.transform.position;
            playerStartRot = player.transform.rotation;
        }

        if (player2 != null)
        {
            player2StartPos = player2.transform.position;
            player2StartRot = player2.transform.rotation;
        }

        if (player3 != null)
        {
            player3StartPos = player3.transform.position;
            player3StartRot = player3.transform.rotation;
        }

        if (player4 != null)
        {
            player4StartPos = player4.transform.position;
            player4StartRot = player4.transform.rotation;
        }
        
        if (player5 != null)
        {
            player5StartPos = player5.transform.position;
            player5StartRot = player5.transform.rotation;
        }
        
        if (ball != null)
        {
            ballStartPos = ball.transform.position;
        }

        if (enemy1 != null)
        {
            enemy1StartPos = enemy1.transform.position;
            enemy1StartRot = enemy1.transform.rotation;
        }
        
        if (enemy2 != null)
        {
            enemy2StartPos = enemy2.transform.position;
            enemy2StartRot = enemy2.transform.rotation;
        }
        
        if (enemy3 != null)
        {
            enemy3StartPos = enemy3.transform.position;
            enemy3StartRot = enemy3.transform.rotation;
        }
        
        if (ally1 != null)
        {
            ally1StartPos = ally1.transform.position;
            ally1StartRot = ally1.transform.rotation;
        }
        
        if (ally2 != null)
        {
            ally2StartPos = ally2.transform.position;
            ally2StartRot = ally2.transform.rotation;
        }
        
        if (Camera != null)
        {
            CameraStartPos = Camera.transform.position;
            CameraStartRot = Camera.transform.rotation;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        bool scored = false;
        
        if (collision.gameObject.CompareTag("Wall")) // ゴール1 → プレイヤーチームの得点
        {
            scoreTeam1++;
            if (countText != null) countText.text = scoreTeam1.ToString();
            scored = true;
        }
        else if (collision.gameObject.CompareTag("Wall2")) // ゴール2 → 敵チームの得点
        {
            scoreTeam2++;
            if (countText2 != null) countText2.text = scoreTeam2.ToString();
            scored = true;
        }

        if (scored)
        {
            // ★追加: ゴール時に変身を解除する
            if (playerTransformation != null)
            {
                playerTransformation.RevertTransformation();
            }

            ResetPositions();
        }
    }

    // すべてのオブジェクトを初期位置にリセットするメソッド
    void ResetPositions()
    {
        ResetObject(player, playerStartPos, playerStartRot);
        ResetObject(player2, player2StartPos, player2StartRot);
        ResetObject(player3, player3StartPos, player3StartRot);
        ResetObject(player4, player4StartPos, player4StartRot);
        ResetObject(player5, player5StartPos, player5StartRot);
        ResetObject(ball, ballStartPos, Quaternion.identity); // ボールは向きをリセットしない
        ResetObject(enemy1, enemy1StartPos, enemy1StartRot);
        ResetObject(enemy2, enemy2StartPos, enemy2StartRot);
        ResetObject(enemy3, enemy3StartPos, enemy3StartRot);
        ResetObject(ally1, ally1StartPos, ally1StartRot);
        ResetObject(ally2, ally2StartPos, ally2StartRot);
        
        // カメラは他のオブジェクトと異なるため個別に処理
        if (Camera != null)
        {
            Camera.transform.position = CameraStartPos;
            Camera.transform.rotation = CameraStartRot;
            Rigidbody cameraRb = Camera.GetComponent<Rigidbody>();
            if (cameraRb != null)
            {
                cameraRb.linearVelocity = Vector3.zero;
                cameraRb.angularVelocity = Vector3.zero;
            }
        }
    }

    // オブジェクトをリセットするヘルパー関数
    void ResetObject(GameObject obj, Vector3 pos, Quaternion rot)
    {
        if (obj == null) return;

        obj.transform.position = pos;
        obj.transform.rotation = rot;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class CollisionCounter : MonoBehaviour
{
    public Text countText;
    public Text countText2;

    // PlayerTransformationスクリプトへの参照 (Inspectorで設定が必要)
    public PlayerTransformation playerTransformation;

    // ゲームオブジェクトの参照
    public GameObject player;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject player5;
    public GameObject ally1;
    public GameObject ally2;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject ball;
    public GameObject Camera;

    // スタート位置・向きのプライベート変数
    private Vector3 playerStartPos;
    private Quaternion playerStartRot;
    private Vector3 player2StartPos;
    private Quaternion player2StartRot;
    private Vector3 player3StartPos;
    private Quaternion player3StartRot;
    private Vector3 player4StartPos;
    private Quaternion player4StartRot;
    private Vector3 player5StartPos;
    private Quaternion player5StartRot;
    private Vector3 ally1StartPos;
    private Quaternion ally1StartRot;
    private Vector3 ally2StartPos;
    private Quaternion ally2StartRot;
    private Vector3 enemy1StartPos;
    private Quaternion enemy1StartRot;
    private Vector3 enemy2StartPos;
    private Quaternion enemy2StartRot;
    private Vector3 enemy3StartPos;
    private Quaternion enemy3StartRot;
    private Vector3 CameraStartPos;
    private Quaternion CameraStartRot;
    private Vector3 ballStartPos;

    // CountdownTimerからアクセスできるようにpublicにする
    public int scoreTeam1 = 0; 
    public int scoreTeam2 = 0;

    void Start()
    {
        // --- 初期位置と向きを記録 ---
        if (player != null)
        {
            playerStartPos = player.transform.position;
            playerStartRot = player.transform.rotation;
        }

        if (player2 != null)
        {
            player2StartPos = player2.transform.position;
            player2StartRot = player2.transform.rotation;
        }

        if (player3 != null)
        {
            player3StartPos = player3.transform.position;
            player3StartRot = player3.transform.rotation;
        }

        if (player4 != null)
        {
            player4StartPos = player4.transform.position;
            player4StartRot = player4.transform.rotation;
        }
        
        if (player5 != null)
        {
            player5StartPos = player5.transform.position;
            player5StartRot = player5.transform.rotation;
        }
        
        if (ball != null)
        {
            ballStartPos = ball.transform.position;
        }

        if (enemy1 != null)
        {
            enemy1StartPos = enemy1.transform.position;
            enemy1StartRot = enemy1.transform.rotation;
        }
        
        if (enemy2 != null)
        {
            enemy2StartPos = enemy2.transform.position;
            enemy2StartRot = enemy2.transform.rotation;
        }
        
        if (enemy3 != null)
        {
            enemy3StartPos = enemy3.transform.position;
            enemy3StartRot = enemy3.transform.rotation;
        }
        
        if (ally1 != null)
        {
            ally1StartPos = ally1.transform.position;
            ally1StartRot = ally1.transform.rotation;
        }
        
        if (ally2 != null)
        {
            ally2StartPos = ally2.transform.position;
            ally2StartRot = ally2.transform.rotation;
        }
        
        if (Camera != null)
        {
            CameraStartPos = Camera.transform.position;
            CameraStartRot = Camera.transform.rotation;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        bool scored = false;
        
        if (collision.gameObject.CompareTag("Wall")) // ゴール1 → プレイヤーチームの得点
        {
            scoreTeam1++;
            if (countText != null) countText.text = scoreTeam1.ToString();
            scored = true;
        }
        else if (collision.gameObject.CompareTag("Wall2")) // ゴール2 → 敵チームの得点
        {
            scoreTeam2++;
            if (countText2 != null) countText2.text = scoreTeam2.ToString();
            scored = true;
        }

        if (scored)
        {
            // ゴール時に変身を解除する
            if (playerTransformation != null)
            {
                playerTransformation.RevertTransformation();
            }

            ResetPositions();
        }
    }

    // すべてのオブジェクトを初期位置にリセットするメソッド
    void ResetPositions()
    {
        ResetObject(player, playerStartPos, playerStartRot);
        ResetObject(player2, player2StartPos, player2StartRot);
        ResetObject(player3, player3StartPos, player3StartRot);
        ResetObject(player4, player4StartPos, player4StartRot);
        ResetObject(player5, player5StartPos, player5StartRot);
        ResetObject(ball, ballStartPos, Quaternion.identity); // ボールは向きをリセットしない
        ResetObject(enemy1, enemy1StartPos, enemy1StartRot);
        ResetObject(enemy2, enemy2StartPos, enemy2StartRot);
        ResetObject(enemy3, enemy3StartPos, enemy3StartRot);
        ResetObject(ally1, ally1StartPos, ally1StartRot);
        ResetObject(ally2, ally2StartPos, ally2StartRot);
        
        // カメラは他のオブジェクトと異なるため個別に処理
        if (Camera != null)
        {
            Camera.transform.position = CameraStartPos;
            Camera.transform.rotation = CameraStartRot;
            Rigidbody cameraRb = Camera.GetComponent<Rigidbody>();
            if (cameraRb != null)
            {
                cameraRb.linearVelocity = Vector3.zero;
                cameraRb.angularVelocity = Vector3.zero;
            }
        }
    }

    // オブジェクトをリセットするヘルパー関数
    void ResetObject(GameObject obj, Vector3 pos, Quaternion rot)
    {
        if (obj == null) return;

        obj.transform.position = pos;
        obj.transform.rotation = rot;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}