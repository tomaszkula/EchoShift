using UnityEngine;

public class TriggerDestroyer : MonoBehaviour
{
    private IOwner iOwner = null;
    private IPooledObject iPooledObject = null;

    private void Awake()
    {
        iOwner = GetComponent<IOwner>();
        iPooledObject = GetComponent<IPooledObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionTarget = collision.gameObject;
        if(collisionTarget.TryGetComponent(out IOwner ownerComponent))
            collisionTarget = ownerComponent.Owner;

        if (collisionTarget == iOwner.Owner)
            return;

        if(iPooledObject != null)
        {
            iPooledObject.Pool.Release(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }   
    }
}
