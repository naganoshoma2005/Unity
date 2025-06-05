using UnityEngine;
using UnityEngine.UI;

public class EnemyHPController : MonoBehaviour
{
    public float enemyMaxHP = 50;
    public float enemyHPBar;
    [SerializeField] Slider slider;
    [SerializeField] private EnemyHealth enemyHealth;

    void Start()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            enemyHealth = GetComponent<EnemyHealth>();
            slider.value = enemyMaxHP;
            enemyHPBar = enemyHealth.currentHealth;
        }
    }

    private void Update()
    {
        enemyHPBar = enemyHealth.currentHealth;
        slider.value = enemyHPBar;
    }
}