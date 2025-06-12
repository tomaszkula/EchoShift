using UnityEngine;

public class Elevator : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private bool defaultIIsMoving = true;
    [SerializeField] private float speed = 1f;

    [Header("References")]
    [SerializeField] new private BoxCollider2D collider = null;
    [SerializeField] private Transform startPosition = null;
    [SerializeField] private Transform endPosition = null;
    [SerializeField] private Transform elevatorPlatform = null;

    private Vector3 startPos = Vector3.zero;
    private Vector3 endPos = Vector3.zero;
    private float timer = 0f;

    public bool IsMoving { get; private set; }

    private void Awake()
    {
        startPos = startPosition.position;
        endPos = endPosition.position;
    }

    private void Start()
    {
        IsMoving = defaultIIsMoving;
        elevatorPlatform.position = startPos;
    }

    private void Update()
    {
        Move();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPosition.position, endPosition.position);
        Gizmos.DrawSphere(startPosition.position, 0.1f);
        Gizmos.DrawSphere(endPosition.position, 0.1f);
    }

    public void Interact()
    {
        ToggleMovement();
    }

    public void Deinteract()
    {
        ToggleMovement();
    }

    private void ToggleMovement()
    {
        IsMoving = !IsMoving;
    }

    private void Move()
    {
        if (!IsMoving)
            return;

        timer += Time.deltaTime * speed;

        float t = Mathf.PingPong(timer, 1f);
        Vector3 targetPosition = Vector3.Lerp(startPos, endPos, t);
        elevatorPlatform.position = targetPosition;
    }
}
