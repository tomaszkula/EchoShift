using UnityEngine;

public class ParentMover : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask collisionLayerMask = default;

    [Header("References")]
    [SerializeField] private Transform newParent = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionTarget = collision.gameObject;
        if (collision.gameObject.TryGetComponent(out IOwner ownerComponent))
            collisionTarget = ownerComponent.Owner;

        if ((collisionLayerMask & (1 << collisionTarget.layer)) == 0)
            return;

        collisionTarget.transform.SetParent(newParent);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collisionTarget = collision.gameObject;
        if (collision.gameObject.TryGetComponent(out IOwner ownerComponent))
            collisionTarget = ownerComponent.Owner;

        if ((collisionLayerMask & (1 << collisionTarget.layer)) == 0)
            return;

        collisionTarget.transform.SetParent(null);
    }
}
