using System;
using UnityEngine;

public class ActivatorDefault : MonoBehaviour, IActivator
{
    private IActivatable iActivatable = null;

    public event Action OnActivate = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out iActivatable);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IActivatable activatable) && activatable == iActivatable)
        {
            iActivatable = null;
        }
    }

    public void Activate()
    {
        iActivatable?.Activate();

        OnActivate?.Invoke();
    }
}
