using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour
{

    public GameObject gameoverline;
    public GameObject[] spawnpoint;
    public GameObject[] enemy;
    public int spawn;
    public float spawnInterval = 20f; // 敵をスポーンする間隔を設定
    public int Destoroyenemy;

    public List<EnemyMove> Enemies = new List<EnemyMove>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < spawn; i++)
        {
            Spawn();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Spawn()
    {
        int randomIndex = Random.Range(0, spawnpoint.Length);
        int randomIndex2 = Random.Range(0, enemy.Length);

        if (randomIndex < spawnpoint.Length)
        {
            GameObject spawnedEnemy = Instantiate(enemy[randomIndex2], spawnpoint[randomIndex].transform.position, Quaternion.Euler(0, 180, 0));
            EnemyMove enemyMove = spawnedEnemy.GetComponent<EnemyMove>();
            if(enemyMove!=null)
            {
                enemyMove.overline = gameoverline;
                enemyMove.se = this;

                Enemies.Add(enemyMove);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Enemies.Count == 0)
        {
            GameClear();
        }

    }
   public void GameClear()
    {
        SceneManager.LoadScene("Clearpanel");
    }
    public void RemoveMover(EnemyMove enemyMove)
    {
        Enemies.Remove(enemyMove);  // リストから削除
    }
    public void CountUp(EnemyMove m)
    {

        Debug.Log("-----> CountUp");
        List<EnemyMove> newList = new List<EnemyMove>();
        foreach (EnemyMove temoMover in Enemies)
        {
            if (temoMover != m)
            {
                newList.Add(temoMover);
            }
        }
        Enemies = new List<EnemyMove>();
        Enemies = newList;


        //myScore += m.enemyStats.Score;

    }

}
