using UnityEngine;

public class Interactable : MonoBehaviour
{
    private IInteractable iInteractable = null;

    private void Awake()
    {
        iInteractable = GetComponent<IInteractable>();
    }

    public void Interact()
    {
        iInteractable?.Interact();
    }

    public void Deinteract()
    {
        iInteractable?.Deinteract();
    }
}
