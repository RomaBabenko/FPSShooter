using UnityEngine;

[CreateAssetMenu(fileName = "CharacterInfo", menuName = "ScriptableObjects/CharacterInfo")]
public class CharacterInfo : ScriptableObject
{
    [Header("Player")]
    public float _moveSpeed;
    public float _jumpForce;
    public float _acceleratedMoveSpeed;
    public bool _isAccelerated;
    [Header("Enemy")]
    public float _rotationSpeed;
    public float _detectionRange;
    public float _detectionAngle;
    public float _secondDetectionRange;
}
