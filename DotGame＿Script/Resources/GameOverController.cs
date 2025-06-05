using UnityEngine;
public class GameOverController : MonoBehaviour
{
    private bool isGameover = false;

    public GameOverController gameOverController;

    private void Start()
    {
        if (gameOverController == null)
        {
            gameOverController = FindFirstObjectByType<GameOverController>();
            if (gameOverController == null)
            {
                Debug.LogError("GameOverController not found in the scene!");
            }
        }
    }

        void Update()
    {
        if (isGameover)
            return;
    }
    public void GameOver()
    {
        GameoverOptions gameoverOptions = new GameoverOptions();
        GameoverUI.Show(gameoverOptions);

        isGameover = true;
    }
}