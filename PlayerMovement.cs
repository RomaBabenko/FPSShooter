using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CameraFollow _cameraFollowScript;
    [SerializeField] private CharacterInfo _characterInfo;
    [SerializeField] private Transform _targetForPlayer;
    [SerializeField] private AudioSource _audioWalk;
    [SerializeField] private AudioSource _audioRun;
    [SerializeField] private GameObject _groundCheck;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _aim;
    private float _originalSpeed;

    public void SaveOriginalSpeed()
    {
        _originalSpeed = _characterInfo._moveSpeed;
    }

    public void Movement()
    {
        NormalMovement();
        TrackMouse();
        Jump();
        Aim();
        MovementAcceleration();
    }

    private void NormalMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveX = Mathf.Clamp(moveX, -1f, 1f);
        moveZ = Mathf.Clamp(moveZ, -1f, 1f);
        Vector3 movement = new Vector3(moveX, 0f, moveZ) * _characterInfo._moveSpeed * Time.deltaTime;
        transform.Translate(movement);
        _animator.SetFloat("horizontal", moveX);
        _animator.SetFloat("vertical", moveZ);
        Cursor.visible = false;
        AudioControl(movement);
    }

    private void TrackMouse()
    {
        if (_targetForPlayer != null)
        {
            Vector3 direction = _targetForPlayer.position - transform.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion horizontalRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Euler(0f, horizontalRotation.eulerAngles.y, 0f);
            }
        }
    }

    private void Aim()
    {
        if (Input.GetMouseButton(1) && !_characterInfo._isAccelerated)
        {
            _animator.SetBool("Aiming", true);
            _cameraFollowScript.MoveAimPosition();
            _aim.SetActive(true);
        }
        else
        {
            _animator.SetBool("Aiming", false);
            _cameraFollowScript.MoveNormalPosition();
            _aim.SetActive(false);
        }
    }

    private void Jump()
    {
        if (IsTouchingGround())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody.AddForce(Vector3.up * _characterInfo._jumpForce, ForceMode.Impulse);
                _animator.SetBool("Jump", true);
                Invoke("DisableJumpAnimation", 0.5f);
            }
        }
    }

    private bool IsTouchingGround()
    {
        float checkRadius = 0.1f;
        Collider[] colliders = Physics.OverlapSphere(_groundCheck.transform.position, checkRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                return true;
            }
        }
        return false;
    }

    private void DisableJumpAnimation()
    {
        _animator.SetBool("Jump", false);
    }

    private void MovementAcceleration()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _characterInfo._moveSpeed = _characterInfo._acceleratedMoveSpeed;
            _animator.SetBool("Run", true);
            _characterInfo._isAccelerated = true;
        }
        else
        {
            _characterInfo._moveSpeed = _originalSpeed;
            _animator.SetBool("Run", false);
            _characterInfo._isAccelerated = false;
        }
    }

    private void AudioControl(Vector3 movement)
    {
        if (movement.magnitude > 0.01f && !_audioWalk.isPlaying && !_characterInfo._isAccelerated)
        {
            _audioWalk.Play();
        }

        else if (movement.magnitude < 0.01f && _audioWalk.isPlaying || !IsTouchingGround())
        {
            _audioWalk.Stop();
        }

        if (movement.magnitude > 0.1f && !_audioRun.isPlaying && _characterInfo._isAccelerated)
        {
            _audioRun.Play();
            _audioWalk.Stop();
        }

        else if (movement.magnitude < 0.01f || !_characterInfo._isAccelerated && _audioRun.isPlaying || !IsTouchingGround())
        {
            _audioRun.Stop();
        }
    }
}
