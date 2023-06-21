using UnityEngine;

public class BoneMove : MonoBehaviour
{
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _pointPistol;
    [SerializeField] private Transform _pointOtherGun;

    public void PistolSelected()
    {
        _leftHand.position = _pointPistol.position;
    }

    public void OtherGunSelected()
    {
        _leftHand.position = _pointOtherGun.position;
    }
}
