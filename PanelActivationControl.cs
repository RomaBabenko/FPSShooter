using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelActivationControl : MonoBehaviour
{
    [SerializeField] private GameObject _pauseGameMenu;
    [SerializeField] private GameObject _winPlayerRound;
    [SerializeField] private GameObject _winEnemyRound;
    public bool _isPaused = false;

    public void WinPlayer()
    {
        _winPlayerRound.SetActive(true);
    }

    public void WinEnemy()
    {
        _winEnemyRound.SetActive(true);
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isPaused)
        {
            _pauseGameMenu.SetActive(true);
            Time.timeScale = 0f;
            _isPaused = true;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPaused)
        {
            _pauseGameMenu.SetActive(false);
            Time.timeScale = 1f;
            _isPaused = false;
            Cursor.visible = false;
        }
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("menu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
