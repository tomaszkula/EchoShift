using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;

    private float timer = 0f;

    private Rigidbody2D rigidbody = null;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        CheckLifeime();
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
            Destroy(gameObject);
        }
    }
}
