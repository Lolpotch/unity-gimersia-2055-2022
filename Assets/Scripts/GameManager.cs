using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject _gameOverCanvas;

    private bool _isGameOver = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // Ensure the game runs at normal speed when starting
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (_isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void GameOver()
    {
        // Show the Game Over UI
        _gameOverCanvas.SetActive(true);

        _isGameOver = true;

        // Pause the game
        Time.timeScale = 0f;
    }

    private void RestartGame()
    {
        // Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
