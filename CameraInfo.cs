using UnityEngine;

[CreateAssetMenu(fileName = "CameraInfo", menuName = "ScriptableObjects/CameraInfo")]
public class CameraInfo : ScriptableObject
{
    public float smoothSpeed;
    public float rotationSpeed;
    public float moveSpeed;
    public Vector3 offset = new Vector3(0f, 0f, 0f);
}
