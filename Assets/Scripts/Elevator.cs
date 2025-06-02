using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 1f;

    [Header("References")]
    [SerializeField] private Transform startPosition = null;
    [SerializeField] private Transform endPosition = null;
    [SerializeField] private Transform elevatorPlatform = null;

    private Vector3 startPos = Vector3.zero;
    private Vector3 endPos = Vector3.zero;

    private void Awake()
    {
        startPos = startPosition.position;
        endPos = endPosition.position;
    }

    private void Start()
    {
        elevatorPlatform.position = startPos;
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = Vector3.Lerp(startPos, endPos, Mathf.PingPong(Time.time * speed, 1f));
        elevatorPlatform.position = targetPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(startPosition.position, endPosition.position);
        Gizmos.DrawSphere(startPosition.position, 0.1f);
        Gizmos.DrawSphere(endPosition.position, 0.1f);
    }
}
