using UnityEngine;

public class EnemyWeapon : Weapon
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private HealthPlayer _healthScript;

    public override void PullTrigger()
    {
        if (_isReloading)
        {
            return;
        }

        if (Time.time >= _lastShootTime + _shootInterval && CurrentAmmo > 0)
        {
            Shoot();
            _lastShootTime = Time.time;
            _fireEffect.Play();
            _bulletEffect.Play();
            _audioShot.Play();
        }

        else
        {
            if (CurrentAmmo == 0)
            {
                Reload();
            }
        }
    }

    public override void Shoot()
    {
        if (CurrentAmmo > 0)
        {
            Ray ray = new Ray(_firePoint.position, _firePoint.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _range, ~_ignoreLayer))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag("Player"))
                {
                    CreateDecal(hit.point, hit.normal, hit.collider.transform, _decalHuman);
                    _healthScript.TakeDamage(Damage);
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
                Debug.Log("Попал в объект: " + hitObject.name);
            }
            CurrentAmmo--;
            Damage = Random.Range(20, 51);
        }
    }
}
