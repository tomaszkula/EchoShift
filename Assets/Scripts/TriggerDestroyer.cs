using UnityEngine;

public class TriggerDestroyer : MonoBehaviour
{
    private IOwner iOwner = null;

    private void Awake()
    {
        iOwner = GetComponent<IOwner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionCollider = collision.gameObject;
        if(collisionCollider.TryGetComponent(out IOwner ownerComponent))
        {
            collisionCollider = ownerComponent.Owner;
        }

        if (collisionCollider == iOwner.Owner)
            return;

        Destroy(gameObject);
    }
}
