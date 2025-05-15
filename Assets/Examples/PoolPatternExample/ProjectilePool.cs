using UnityEngine;

public class ProjectilePool : ObjectPooler<Projectile>
{
    [SerializeField]
    private int _projectileCount = 10;

    private void OnEnable()
    {
        Projectile.OnLifeTimeComplete += ReleaseObject;
        Projectile.OnProjectileHit += ReleaseObject;
    }

    private void OnDisable()
    {
        Projectile.OnLifeTimeComplete -= ReleaseObject;
        Projectile.OnProjectileHit -= ReleaseObject;
    }

    private void Awake()
    {
        InitializePool(_projectileCount);
    }


}
