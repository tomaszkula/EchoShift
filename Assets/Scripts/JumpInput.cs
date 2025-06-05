using UnityEngine;

public class JumpInput : MonoBehaviour
{
    private bool isJumpRequested = false;

    private IJump iJump = null;

    private void Awake()
    {
        iJump = GetComponent<IJump>();
    }

    private void OnEnable()
    {
        Manager.Instance.GetManager<InputsManager>().OnJump += OnJumpAction;
    }

    private void Update()
    {
        Jump();
    }

    private void OnDisable()
    {
        Manager.Instance.GetManager<InputsManager>().OnJump -= OnJumpAction;
    }

    private void OnJumpAction()
    {
        isJumpRequested = true;
    }

    private void Jump()
    {
        if (!isJumpRequested)
            return;

        isJumpRequested = false;

        iJump?.Jump();
    }
}