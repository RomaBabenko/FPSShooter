using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour
{
    [SerializeField] private CharacterInfo _characterInfo;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private EnemyWeapon _weaponEnemy;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _spawnAreaSize;
    private Vector3 _targetPosition;

    public void EnemyMovement()
    {
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomTarget();
        }

        if (DetectPlayer(_characterInfo._detectionRange, _characterInfo._detectionAngle))
        {
            MoveTowardsPlayer();
            RotateTowardsPlayer();
            _weaponEnemy.PullTrigger();
        }

        if (DetectPlayer(_characterInfo._secondDetectionRange, _characterInfo._detectionAngle))
        {
            StopMovement();
            RotateTowardsPlayer();
            _weaponEnemy.PullTrigger();
        }
    }

    private void MoveTowardsPlayer()
    {
        _navMeshAgent.SetDestination(_player.position);
        _navMeshAgent.isStopped = false;
        _animator.SetBool("idle", false);
    }

    private void StopMovement()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.velocity = Vector3.zero;
        _animator.SetBool("idle", true);
    }

    private void RotateTowardsPlayer()
    {
        Vector3 playerDirection = _player.position - transform.position;
        playerDirection.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, _characterInfo._rotationSpeed * Time.deltaTime);
    }

    public void SetRandomTarget()
    {
        float x = Random.Range(_spawnArea.position.x - _spawnAreaSize.x / 2f, _spawnArea.position.x + _spawnAreaSize.x / 2f);
        float z = Random.Range(_spawnArea.position.z - _spawnAreaSize.z / 2f, _spawnArea.position.z + _spawnAreaSize.z / 2f);
        _targetPosition = new Vector3(x, 0f, z);
        _navMeshAgent.SetDestination(_targetPosition);
    }


    private bool DetectPlayer(float detectionRange, float detectionAngle)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Vector3 playerDirection = collider.transform.position - transform.position;
                playerDirection.y = 0f;

                if (Vector3.Angle(transform.forward, playerDirection) < detectionAngle)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _characterInfo._detectionRange);
        Vector3 leftBoundary = Quaternion.Euler(0f, -_characterInfo._detectionAngle / 2f, 0f) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0f, _characterInfo._detectionAngle / 2f, 0f) * transform.forward;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * _characterInfo._detectionRange);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * _characterInfo._detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _characterInfo._secondDetectionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * _characterInfo._secondDetectionRange);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * _characterInfo._secondDetectionRange);
    }

    private void OnDrawGizmos()
    {
        if (_spawnArea != null)
        {
            Gizmos.color = Color.yellow;
            Vector3 spawnAreaPosition = _spawnArea.position;
            Gizmos.DrawWireCube(spawnAreaPosition, _spawnAreaSize);
        }
    }
}
