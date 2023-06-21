using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] private AudioSource _audio—hoice;
    [SerializeField] private GameObject[] _weapons;
    [SerializeField] private BoneMove _boneMoveScript;
    private int _currentWeapon = 0;

    public void ChoosingFirstWeapon()
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (i == _currentWeapon)
                _weapons[i].SetActive(true);
            else
                _weapons[i].SetActive(false);
        }
    }

    public void SelectWeaponClicking()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
            _boneMoveScript.OtherGunSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
            _boneMoveScript.OtherGunSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(2);
            _boneMoveScript.PistolSelected();
        }
    }

    private void SwitchWeapon(int index)
    {
        _weapons[_currentWeapon].SetActive(false);
        _currentWeapon = index;
        _weapons[_currentWeapon].SetActive(true);
        _audio—hoice.Play();
    }
}
