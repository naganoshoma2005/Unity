using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    public float playerMaxHP = 100;
    public float playerHPBar;
    [SerializeField] Slider slider;
    [SerializeField] private PlayerHP playerHP;

    void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            playerHP = GetComponent<PlayerHP>();
            slider.value = playerMaxHP;
            playerHPBar = playerHP.currentHealth;
        }
    }

    private void Update()
    {
        playerHPBar = playerHP.currentHealth;
        slider.value = playerHPBar;
    }
}