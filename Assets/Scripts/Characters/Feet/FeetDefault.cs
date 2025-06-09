using UnityEngine;

public class FeetDefault : MonoBehaviour, IFeet
{
    [Header("Settings")]
    [SerializeField] private LayerMask groundLayer = default;
    [SerializeField] private float groundCheckRadius = 0.2f;

    [Header("References")]
    [SerializeField] private Transform feet = null;

    public Transform Feet => feet;

    private void OnDrawGizmos()
    {
        if (feet == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(feet.position, groundCheckRadius);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(feet.position, groundCheckRadius, groundLayer);
    }
}
