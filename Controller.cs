using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private PanelActivationControl _panelActivationControl;
    [SerializeField] private PlayerMovement _playerMovementScript;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private WeaponSwitching _weaponSwitching;
    [SerializeField] private HealthPlayer _healthPlayerScript;
    [SerializeField] private HealthEnemy _healthEnemyScript;
    [SerializeField] private AIEnemy _aIEnemy;

    private void Start()
    {
        _playerMovementScript.SaveOriginalSpeed();
        _weaponSwitching.ChoosingFirstWeapon();
        _aIEnemy.SetRandomTarget();
    }

    private void Update()
    {
        if (_healthPlayerScript._isDeath || _healthEnemyScript._isDeath)
        {
            Cursor.visible = true;
            return;
        }

        if (!_healthEnemyScript._isDeath || !_healthPlayerScript._isDeath)
        {
            _healthEnemyScript.Death();
            _healthPlayerScript.Death();
        }

        _panelActivationControl.Pause();

        if (!_panelActivationControl._isPaused)
        {
            _weaponSwitching.SelectWeaponClicking();
            _playerMovementScript.Movement();
            _cameraFollow.MovingAndRotatingBehindObject();
            _aIEnemy.EnemyMovement();
        }
    }
}