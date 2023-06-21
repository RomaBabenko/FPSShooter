using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] protected PanelActivationControl _panelActivationControl;
    [SerializeField] protected WinCounter _winCounter;
    [SerializeField] protected GameObject _mainCamera, _secondCamera;
    [SerializeField] protected Animator _animator;
    [SerializeField] protected int _health;
    public bool _isDeath = false;

    public virtual void TakeDamage(int damage)
    {
        _health -= damage;
    }

    public virtual void Death()
    {
        if (_health <= 0)
        {
            _animator.SetBool("Death", true);
            _health = 0;
            _mainCamera.SetActive(false);
            _secondCamera.SetActive(true);
            _isDeath = true;
        }
    }
}
