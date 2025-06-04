
/*using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public GameObject overline;
    public int END = 0;
    public float Speed;
    public int HP = 50;  // 敵のHPを追加
    public int moneyReward = 100; // 敵を倒したときの報酬金額

    public bool Move = true;
    public float border = 0.5f;

    private Animator animator; // Animatorコンポーネントの参照

    private Stop Blocker; // null;

    // Start is called before the first frame update
    void Start()
    {
        // 敵にアタッチされたAnimatorコンポーネントを取得
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - overline.transform.position.x <= border)
        {
            GameOver();
        }

        if (END == 0 && Move)
        {
            Mover();
        }

        //停まっている状態なら
        if (!Move)
        {
            if (Blocker == null)
                Move = true;
        }

        // Moveがtrueなら走るアニメーションに設定
        animator.SetBool("isRunning", Move);
    }

    private void Mover()
    {
        transform.Translate(Speed * Time.deltaTime, 0, 0);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("kawakami");
        if (!Move)
        {
            if (other.gameObject.CompareTag("END"))
            {
                Debug.LogError("gameover");
                GameOver();
            }
            else if (other.gameObject.CompareTag("Block") ||
                     other.gameObject.CompareTag("Trap") ||
                     other.gameObject.CompareTag("Trap1") ||
                     other.gameObject.CompareTag("Trap2"))
            {
                HandleStopComponent(other);
            }

            // 新しいタグに対するダメージ処理を追加
            else if (other.gameObject.CompareTag("DamageTrap"))
            {
                Debug.Log("Enemy took damage!");
                TakeDamage(50); // ここでダメージ値を設定
            }
        }

        // 攻撃アニメーションを呼び出す
        HandleAttackAnimation();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("hit!");
        Move = true;
    }

    private void HandleStopComponent(Collider2D other)
    {
        Stop stop = other.gameObject.GetComponent<Stop>();
        if (stop != null)
        {
            // 攻撃アニメーションを呼び出す
            HandleAttackAnimation();
            stop.Damage();
            if (stop.weapon.HP <= 0)
            {
                Debug.Log("ooooooooooo");
                Destroy(other.gameObject);
                Move = true;
            }
        }
    }

    private void HandleAttackAnimation()
    {
        Debug.Log("UYORHKDTIJDITJ");
        // 攻撃アニメーションを再生
        animator.SetTrigger("Attack");
        Move = false;
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        Destroy(gameObject);
    }

    public void Die()
    {
        // プレイヤーのステータスにお金を追加
        PlayerStats.Instance.AddMoney(moneyReward);

        // 死亡アニメーションを再生
        animator.SetTrigger("Die");

        // アニメーション再生後に敵オブジェクトを破壊（必要に応じて遅延を追加）
        Destroy(gameObject, 60f); // アニメーションを再生するために遅延を追加
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    public void Stay(Stop blocker)
    {
        Blocker = blocker;
        Move = false;
    }
}*/


using UnityEngine;  
using System.Collections;


public class EnemyMove : MonoBehaviour
{
    public SpawnEnemy se;
    public Manager manager;
    public GameObject overline;
    public int END = 0;
    public float Speed;
    public int HP = 100;  // 敵のHPを追加
    public int moneyReward = 100; // 敵を倒したときの報酬金額
    public GameObject summonPrefab; // 召喚するオブジェクトのプレハブ
    public bool Move = true;
    public bool slow = true;
    public bool isGameOver = false;
    public float border = 0.5f;

    

    private Animator animator; // Animatorコンポーネントの参照

    private Stop Blocker; // null;

    // Start is called before the first frame update
    void Start()
    {
        // 敵にアタッチされたAnimatorコンポーネントを取得
        animator = gameObject.GetComponent<Animator>();
        manager = FindFirstObjectByType<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - overline.transform.position.x <= border && !isGameOver)
        {    
            isGameOver = true;
            GameOver();
        }

        if (END == 0 && Move)
        {
            Mover();
        }

        //停まっている状態なら
        if (!Move)
        {
            if (Blocker == null)
                Move = true;
        }

        // Moveがtrueなら走るアニメーションに設定
        animator.SetBool("isRunning", Move);
    }

    private void Mover()
    {
        transform.Translate(Speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DamageTrap"))
        {
            Debug.Log("Enemy took damage from DamageTrap!");
            TakeDamage(10); // ここでダメージ値を設定
        }

        else if (other.gameObject.CompareTag("Debuff") && slow == true)
        {
            StartCoroutine(Debuff());
        }
    }

    private IEnumerator Debuff()
    {
        slow = false;
        Debug.Log("Debuff applied: Speed reduced by 0.2f");
        Speed -= 0.3f;


        yield return new WaitForSeconds(2f);
        Speed += 0.3f;
        Debug.Log("Debuff ended: Speed restored");
        slow = true;

    }



    void OnTriggerStay2D(Collider2D other)
    {
      
        if (!Move)
        {
            if (other.gameObject.CompareTag("END"))
            {
                Debug.LogError("gameover");
                GameOver();
            }
             if (other.gameObject.CompareTag("Block") ||
                     other.gameObject.CompareTag("Trap") ||
                     other.gameObject.CompareTag("Trap1") ||
                     other.gameObject.CompareTag("Trap2"))
            {
                HandleStopComponent(other);
            }

            // 攻撃アニメーションを呼び出す
            HandleAttackAnimation();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("OnTriggerExit2D called with " + collider.gameObject.name);
        Move = true;
    }

    private void HandleStopComponent(Collider2D other)
    {
        Stop stop = other.gameObject.GetComponent<Stop>();
        if (stop != null)
        {
            // 攻撃アニメーションを呼び出す
            HandleAttackAnimation();
            stop.Damage();
            if (stop.weapon.HP <= 0)
            {
                Debug.Log("Stop component destroyed");
                stop.setting.set = true;
                
                Destroy(other.gameObject);
                Move = true;
            }
        }
    }

    private void HandleAttackAnimation()
    {
        Debug.Log("Attack animation triggered");
        // 攻撃アニメーションを再生
        animator.SetTrigger("Attack");
        Move = false;
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            Debug.Log("GameOver");
            manager.overLine();
            isGameOver = false;
        }
    }

    public void Die()
    {
        Move = false;
        se.CountUp(this);
        se.RemoveMover(this);
        // プレイヤーのステータスにお金を追加
        PlayerStats.Instance.AddMoney(moneyReward);

        // 死亡アニメーションを再生
        animator.SetTrigger("Die");
        

        // アニメーション再生後に敵オブジェクトを破壊（必要に応じて遅延を追加）
        Destroy(gameObject,1f); // アニメーションを再生するために遅延を追加
                                  // 召喚するオブジェクトを生成
        GameObject summonedObject = Instantiate(summonPrefab, transform.position, transform.rotation);
        DestroyEffect destroyEffect = summonedObject.GetComponent<DestroyEffect>();
        if (destroyEffect != null)
        {
            destroyEffect.SE = se;
        }
        // 1秒後に召喚したオブジェクトを破壊する
        Destroy(summonedObject, 1f);

    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy takes " + damage + " damage");
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    public void Stay(Stop blocker)
    {
        Blocker = blocker;
        Move = false;
    }

}