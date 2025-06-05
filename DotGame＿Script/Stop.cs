
using UnityEngine;
using Game;

public class Stop : MonoBehaviour
{
    public Weapon weapon;
    public EnemyMove enemyMove;
    private Animator animator;
    public Setting setting;

    // Start is called before the first frame update
    void Start()
    {
        // 子オブジェクトのAnimatorコンポーネントを取得
        animator = GetComponentInChildren<Animator>();

        if (gameObject.CompareTag("Trap1"))
        {
            weapon = new Weapon(1, 40, 100);
        }
        else if (gameObject.CompareTag("Trap2"))
        {
            weapon = new Weapon(2, 30, 1);
        }
        else if (gameObject.CompareTag("Trap"))
        {
            weapon = new Weapon(3, 60, 3);
        }
        else if (gameObject.CompareTag("Block"))
        {
            weapon = new Weapon(4, 400, 1);
        }

        // 常に攻撃アニメーションを再生する
        if (animator != null)
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            Debug.LogError("Animator component not found in children of " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage()
    {
        weapon.HP--;
        Debug.Log("Attack");

        // 攻撃アニメーションを再度トリガーする場合
        if (animator != null)
        {
            // isAttackingがtrueならアニメーションは再生され続ける
            animator.SetBool("isAttacking", true);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
       
        if (other.gameObject.CompareTag("Enemy"))
        {
          
            EnemyMove enemyMove = other.gameObject.GetComponent<EnemyMove>();

            //enemyMove.Move = false;
            enemyMove.Stay(this);
        }
    }
   public void Die()
    {
        animator.SetTrigger("Die");
    }
}

