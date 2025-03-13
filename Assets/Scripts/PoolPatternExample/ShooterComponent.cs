using UnityEngine;

public class ShooterComponent : MonoBehaviour
{


    [SerializeField]
    private float _fireRate = 0.5f;
    private float _fireRateTimer;

    private ObjectPooler<Projectile> _projectilePool;


    private void Awake()
    {
        TryGetComponent(out _projectilePool);
    }

    // Update is called once per frame
    void Update()
    {
        // se la pool è nulla non fare nulla
        if (_projectilePool == null) return;

        if (_fireRateTimer > 0)
        {
            _fireRateTimer -= Time.deltaTime;
            return;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Projectile projectile = _projectilePool.GetObject(true);
            //do stuff with projectile...
            _fireRateTimer = _fireRate;
        }
    }
}
