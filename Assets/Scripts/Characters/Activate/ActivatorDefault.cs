using System;
using UnityEngine;

public class ActivatorDefault : MonoBehaviour, IActivator
{
    private IActivatable iActivatable = null;

    public event Action OnActivate = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        if(collision.TryGetComponent(out IOwner iOwner))
        {
            target = iOwner.Owner;
        }

        if (this.iActivatable == null
            && target.TryGetComponent(out IActivatable iActivatable))
        {
            this.iActivatable = iActivatable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        if (collision.TryGetComponent(out IOwner iOwner))
        {
            target = iOwner.Owner;
        }

        if (target.TryGetComponent(out IActivatable iActivatable) &&
            iActivatable == this.iActivatable)
        {
            this.iActivatable = null;
        }
    }

    public void Activate()
    {
        iActivatable?.Activate();

        OnActivate?.Invoke();
    }
}
