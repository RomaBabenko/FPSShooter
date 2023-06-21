using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _aimPositionCamera, _normalPositionCamera;
    [SerializeField] private Transform _aimPositionTracking, _normalPositionTracking;
    [SerializeField] private GameObject _camera, _trackingPoint;
    [SerializeField] private CameraInfo _cameraInfo;
    [SerializeField] private Transform _target;
    private float _mouseX;
    private float _mouseY;

    public void MovingAndRotatingBehindObject()
    {
        _mouseX += Input.GetAxis("Mouse X") * _cameraInfo.rotationSpeed;
        _mouseY -= Input.GetAxis("Mouse Y") * _cameraInfo.rotationSpeed;
        _mouseY = Mathf.Clamp(_mouseY, -90f, 90f);
        Quaternion rotation = Quaternion.Euler(_mouseY, _mouseX, 0f);
        Vector3 desiredPosition = _target.position + _cameraInfo.offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _cameraInfo.smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        transform.rotation = rotation;
    }

    public void MoveAimPosition()
    {
        _camera.transform.position = Vector3.MoveTowards(_aimPositionCamera.position,_normalPositionCamera.position, _cameraInfo.moveSpeed * Time.deltaTime);
        _trackingPoint.transform.position = _aimPositionTracking.position;
    }

    public void MoveNormalPosition()
    {
        _camera.transform.position = Vector3.MoveTowards(_normalPositionCamera.position, _aimPositionCamera.position, _cameraInfo.moveSpeed * Time.deltaTime);
        _trackingPoint.transform.position = _normalPositionTracking.position;
    }
}