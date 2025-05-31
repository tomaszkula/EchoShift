using UnityEngine;

public class ActivatorInput : MonoBehaviour
{
    private bool isActivateRequested = false;

    private IActivator iActivator = null;

    private void Awake()
    {
        iActivator = GetComponent<IActivator>();
    }

    private void OnEnable()
    {
        ManagersController.Instance.GetManager<PlayerInputManager>().OnActivate += OnActivateAction;
    }

    private void Update()
    {
        Activate();
    }

    private void OnDisable()
    {
        ManagersController.Instance.GetManager<PlayerInputManager>().OnActivate -= OnActivateAction;
    }

    private void OnActivateAction()
    {
        isActivateRequested = true;
    }

    private void Activate()
    {
        if (!isActivateRequested)
            return;

        isActivateRequested = false;

        iActivator?.Activate();
    }
}
