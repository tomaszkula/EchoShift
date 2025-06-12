using System;
using UnityEngine;

public class PickerDefault : MonoBehaviour, IPicker
{
    public event Action<IPickable> OnPicked = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        if(target.TryGetComponent(out IOwner iOwner))
        {
            target = iOwner.Owner;
        }

        if(target.TryGetComponent(out IPickable iPickable))
        {
            Pick(iPickable);
        }
    }

    public void Pick(IPickable iPickable)
    {
        iPickable.Pick(this);

        OnPicked?.Invoke(iPickable);
    }
}
