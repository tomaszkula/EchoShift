using UnityEngine;

public class PressurePlate : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private Animator animator = null;

    private const string ANIMATOR_INTERACT_BOOL_KEY = "IsPressed";

    public void Interact()
    {
        animator.SetBool(ANIMATOR_INTERACT_BOOL_KEY, true);
    }

    public void DeInteract()
    {
        animator.SetBool(ANIMATOR_INTERACT_BOOL_KEY, false);
    }
}