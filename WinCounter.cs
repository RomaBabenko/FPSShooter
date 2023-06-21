using TMPro;
using UnityEngine;

public class WinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _winPlayerText;
    [SerializeField] private TextMeshProUGUI _winEnemyText;
    private int _playerWin;
    private int _enemyWin;
    private const string PlayerWinKey = "PlayerWin";
    private const string EnemyWinKey = "EnemyWin";

    private void Start()
    {
        _playerWin = PlayerPrefs.GetInt(PlayerWinKey, 0);
        _enemyWin = PlayerPrefs.GetInt(EnemyWinKey, 0);
        UpdateUI();
    }

    public void PlayerWin()
    {
        _playerWin++;
        CheckWinCondition();
        UpdateUI();
        Cursor.visible = true;
    }

    public void EnemyWin()
    {
        _enemyWin++;
        CheckWinCondition();
        UpdateUI();
        Cursor.visible = true;
    }

    private void CheckWinCondition()
    {
        if (_playerWin >= 10 || _enemyWin >= 10)
        {
            _playerWin = 0;
            _enemyWin = 0;
        }
    }

    private void UpdateUI()
    {
        _winPlayerText.text = _playerWin.ToString();
        _winEnemyText.text = _enemyWin.ToString();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(PlayerWinKey, _playerWin);
        PlayerPrefs.SetInt(EnemyWinKey, _enemyWin);
        PlayerPrefs.Save();
    }
}
