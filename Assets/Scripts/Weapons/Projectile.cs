using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ProjectileData projectileData = null;

    private float timer = 0f;

    private IAttacker iAttacker = null;
    private IPooledObject iPooledObject = null;
    new private Rigidbody2D rigidbody = null;

    public ProjectileData ProjectileData => projectileData;

    private void Awake()
    {
        iAttacker = GetComponent<IAttacker>();
        iPooledObject = GetComponent<IPooledObject>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        iPooledObject.OnGet += OnPooledObjectGet;
    }

    private void Update()
    {
        CheckLifeime();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        if (target.TryGetComponent(out IOwner iOwner))
            target = iOwner.Owner;

        if (target == gameObject)
            return;

        if (target == iAttacker.Attacker)
            return;

        if(target.TryGetComponent(out IDamageable iDamageable))
        {
            iDamageable.Damage(projectileData.DamageType, projectileData.Damage);
        }
    }

    private void OnDisable()
    {
        iPooledObject.OnGet -= OnPooledObjectGet;
    }

    private void OnPooledObjectGet(GameObject go)
    {
        rigidbody.linearVelocity = Vector2.zero;
        timer = 0f;
    }

    private void Move()
    {
        rigidbody.linearVelocity = transform.right * projectileData.Speed;
    }

    private void CheckLifeime()
    {
        timer += Time.deltaTime;
        if (timer >= projectileData.Lifetime)
        {
            iPooledObject.Pool.Release(gameObject);
        }
    }
}
