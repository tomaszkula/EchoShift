using UnityEngine;

public class Interactor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<IInteractable>()?.Interact();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<IInteractable>()?.DeInteract();
    }
}
