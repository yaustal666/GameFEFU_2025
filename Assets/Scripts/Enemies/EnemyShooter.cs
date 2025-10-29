using UnityEngine;
using UnityEngine.Pool;

public class EnemyShooter : MonoBehaviour, IDamagable
{
    private IObjectPool<Projectile> projectilePool;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float fireCooldown = 0f;
    [SerializeField] private float fireRate = 1f;

    private float nextTimeShoot = 1f;

    [Header("Projectile Pool Settings")]
    [SerializeField] private bool collectionCheck = false;
    [SerializeField] private int defaultCapacity = 8;
    [SerializeField] private int maxSize = 16;

    [SerializeField] private Health _health;

    private void Awake()
    {
        projectilePool = new ObjectPool<Projectile>(CreateProjectile, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, 8, 16);
    }

    private void Start()
    {
        _health.OutOfHealth += OnOutOfHealth;    
    }

    private void OnOutOfHealth()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(shootPosition.position, -shootPosition.right, detectionRange);
        Debug.DrawRay(shootPosition.position, -shootPosition.right * detectionRange, Color.red, 0.2f);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {

            if (Time.time > nextTimeShoot)
            {
                Shoot();
                nextTimeShoot = Time.time + 1f / fireRate;
            }
        }
    }

    private void FixedUpdate()
    {
        //if (Time.time > nextTimeShoot)
        //{
        //    Shoot();
        //    nextTimeShoot = Time.time + 1f;
        //}
    }

    private void Shoot()
    {
        Projectile projectile = projectilePool.Get();

        if (projectile == null) return;

        projectile.transform.SetPositionAndRotation(shootPosition.position, shootPosition.rotation);
        projectile.Shoot(2f);
        projectile.Deactivate();
    }

    private Projectile CreateProjectile()
    {
        Projectile projectileInstance = Instantiate(projectilePrefab).GetComponent<Projectile>();
        projectileInstance.ObjectPool = projectilePool;
        return projectileInstance;
    }

    private void OnGetFromPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }

    public void TakeDamage(Damage damage)
    {
        _health.GetDamage(damage.damage);
    }
}