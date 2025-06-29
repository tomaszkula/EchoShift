using UnityEngine;

public class Lever : MonoBehaviour, IActivatable
{
    [Header("Settings")]
    [SerializeField] private bool isActivatedOnStart = false;
    [SerializeField] private Interactable target = null;

    [Header("References")]
    [SerializeField] private Animator animator = null;
    [SerializeField] new private BoxCollider2D collider = null;

    private bool isActivated = false;

    private const string ANIMATOR_IS_ACTIVATED_BOOL_KEY = "IsActivated";

    private void Awake()
    {
        if (isActivatedOnStart)
        {
            isActivated = true;
            animator.SetBool(ANIMATOR_IS_ACTIVATED_BOOL_KEY, true);
            
        }
        else
        {
            isActivated = false;
            animator.SetBool(ANIMATOR_IS_ACTIVATED_BOOL_KEY, false);
        }
    }

    private void Start()
    {
        if(isActivated)
            target?.Interact();
        else
            target?.Deinteract();
    }

    public void Activate()
    {
        ToggleActivation();
    }

    public void Deactivate()
    {
        ToggleActivation();
    }

    private void ToggleActivation()
    {
        isActivated = !isActivated;

        if (isActivated)
        {
            animator.SetBool(ANIMATOR_IS_ACTIVATED_BOOL_KEY, true);

            target?.Interact();
        }
        else
        {
            animator.SetBool(ANIMATOR_IS_ACTIVATED_BOOL_KEY, false);

            target?.Deinteract();
        }
    }
}
