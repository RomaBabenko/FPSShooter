using UnityEngine;
using UnityEngine.AI;

public class HealthEnemy : CharacterHealth
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }

    public override void Death()
    {
        if (_health <= 0)
        {
            _panelActivationControl.WinPlayer();
            _winCounter.PlayerWin();
            _navMeshAgent.enabled = false;
            _animator.SetBool("Death", true);
            _health = 0;
            _mainCamera.SetActive(false);
            _secondCamera.SetActive(true);
            _isDeath = true;
        }
    }
}
