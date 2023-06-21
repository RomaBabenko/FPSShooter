using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "ScriptableObjects/WeaponInfo")]
public class WeaponInfo : ScriptableObject
{
    public float _shootInterval;
    public float _range;
    public float _reloadTime;
    public int _maxAmmo;
    public int _maxAmmoReserve;
}
