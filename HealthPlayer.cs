using TMPro;
using UnityEngine;

public class HealthPlayer : CharacterHealth
{
    [SerializeField] private TextMeshProUGUI _healthText;

    public override void TakeDamage(int damage)
    {
        _health -= damage;
        _healthText.text = "" + _health;
    }

    public override void Death()
    {
        if (_health <= 0)
        {
            _panelActivationControl.WinEnemy();
            _winCounter.EnemyWin();
            _animator.SetBool("Death", true);
            _health = 0;
            _mainCamera.SetActive(false);
            _secondCamera.SetActive(true);
            _healthText.text = "" + _health;
            _isDeath = true;
        }
    }
}
