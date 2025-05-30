using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;

    private float timer = 0f;

    private IPooledObject iPooledObject = null;
    private Rigidbody2D rigidbody = null;

    private void Awake()
    {
        iPooledObject = GetComponent<IPooledObject>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        iPooledObject.OnGet += OnPooledObjectGet;
    }

    private void Update()
    {
        Move();
        CheckLifeime();
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
        rigidbody.linearVelocity = transform.right * speed;
    }

    private void CheckLifeime()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            iPooledObject.Pool.Release(gameObject);
        }
    }
}
