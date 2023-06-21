using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject _decalVood, _decalHuman, _decalSand, _decalRock1, _decalRock2;
    [SerializeField] protected ParticleSystem _fireEffect, _bulletEffect;
    [SerializeField] private TextMeshProUGUI _ammoReserveText, _ammoText;
    [SerializeField] protected AudioSource _audioShot, _audioReload;
    [SerializeField] protected HealthEnemy _healthEnemyScript;
    [SerializeField] private CharacterInfo _characterInfo;
    [SerializeField] protected Animator _fireAnimator;
    [SerializeField] protected LayerMask _ignoreLayer;
    [SerializeField] protected float _shootInterval;
    [SerializeField] protected float _range;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private int _maxAmmoReserve;
    public int CurrentAmmo;
    public int AmmoReserve;
    public int Damage { get; set; }
    protected float _lastShootTime;
    protected bool _isReloading;

    private void Start()
    {
        CurrentAmmo = _maxAmmo;
        AmmoReserve = _maxAmmoReserve;
    }

    public virtual void PullTrigger()
    {
        if (_isReloading)
        {
            return;
        }

        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButton(0) && Time.time >= _lastShootTime + _shootInterval && !_characterInfo._isAccelerated && CurrentAmmo > 0)
            {
                Shoot();
                _lastShootTime = Time.time;
                _fireAnimator.SetBool("Fire", true);
                _fireEffect.Play();
                _bulletEffect.Play();
                _audioShot.Play();
            }

            else
            {
                if (CurrentAmmo == 0 || !Input.GetMouseButton(0))
                {
                    _fireAnimator.SetBool("Fire", false);
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        AmmoDisplay();
    }

    public virtual void Shoot()
    {
        if (CurrentAmmo > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _range, ~_ignoreLayer))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag("Enemy") || hitObject.CompareTag("Player"))
                {
                    CreateDecal(hit.point, hit.normal, hit.collider.transform, _decalHuman);
                    _healthEnemyScript.TakeDamage(Damage);
                }

                if (hitObject.CompareTag("Box"))
                {
                    CreateDecal(hit.point, hit.normal, hit.collider.transform, _decalVood);
                }

                if (hitObject.CompareTag("Ground"))
                {
                    CreateDecal(hit.point, hit.normal, hit.collider.transform, _decalSand);
                }

                if (hitObject.CompareTag("Rock1"))
                {
                    CreateDecal(hit.point, hit.normal, hit.collider.transform, _decalRock1);
                }

                if (hitObject.CompareTag("Rock2"))
                {
                    CreateDecal(hit.point, hit.normal, hit.collider.transform, _decalRock2);
                }
            }
            CurrentAmmo--;
            Damage = Random.Range(20, 51);
        }
    }

    protected void CreateDecal(Vector3 position, Vector3 normal, Transform parent, GameObject decalPrefab)
    {
        GameObject decal = Instantiate(decalPrefab, position, Quaternion.LookRotation(normal));
        decal.transform.SetParent(parent);
        decal.transform.forward = -normal;
        Destroy(decal, 5f);
    }

    protected void Reload()
    {
        if (CurrentAmmo < _maxAmmo && !_isReloading)
        {
            int roundsNeeded = _maxAmmo - CurrentAmmo;
            int roundsAvailable = Mathf.Min(roundsNeeded, AmmoReserve);

            if (roundsAvailable > 0)
            {
                _isReloading = true;
                AmmoReserve -= roundsAvailable;
                CurrentAmmo += roundsAvailable;
                Invoke("FinishReload", _reloadTime);
            }
            else
            {
                _isReloading = false;
            }
            _fireAnimator.SetBool("Reload", true);
            _audioReload.Play();
        }
    }

    private void FinishReload()
    {
        if (AmmoReserve < _maxAmmo)
        {
            CurrentAmmo += AmmoReserve;
        }
        else
        {
            CurrentAmmo = _maxAmmo;
        }
        _isReloading = false;
        _fireAnimator.SetBool("Reload", false);
    }

    protected void AmmoDisplay()
    {
        if (!_isReloading)
        {
            _ammoText.text = "" + CurrentAmmo;
            _ammoReserveText.text = "" + AmmoReserve;
        }
    }
}
