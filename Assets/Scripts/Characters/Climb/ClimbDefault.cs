using System;
using UnityEngine;

public class ClimbDefault : MonoBehaviour, IClimb
{
    [Header("Settings")]
    [SerializeField] private float climbSpeed = 5f;

    public bool IsClimbing { get; private set; }
    private ITriggerable iTriggerable = null;
    private Vector2 climbDirection = Vector2.zero;

    private ITriggerer iTriggerer = null;
    new private Rigidbody2D rigidbody = null;

    public event Action<Vector2> OnClimb = null;

    private void Awake()
    {
        iTriggerer = GetComponent<ITriggerer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        iTriggerer.OnTriggered += OnTriggered;
        iTriggerer.OnUntriggered += OnUntriggered;
    }

    private void FixedUpdate()
    {
        ApplyClimbing();
    }

    private void OnDisable()
    {
        iTriggerer.OnTriggered -= OnTriggered;
        iTriggerer.OnUntriggered -= OnUntriggered;
    }

    private void OnTriggered(ITriggerable iTriggerable)
    {
        if (iTriggerable is not Ladder)
            return;

        IsClimbing = true;
        this.iTriggerable = iTriggerable;

        rigidbody.gravityScale = 0f;
    }

    private void OnUntriggered(ITriggerable iTriggerable)
    {
        if (iTriggerable != this.iTriggerable)
            return;

        IsClimbing = false;
        this.iTriggerable = null;

        rigidbody.gravityScale = 1f;

        StopClimbing();
    }

    public void Climb(Vector2 direction)
    {
        climbDirection = direction;
    }

    private void ApplyClimbing()
    {
        if (!IsClimbing)
            return;

        Vector2 climbVelocity = climbDirection * climbSpeed;
        rigidbody.linearVelocity = climbVelocity;

        OnClimb?.Invoke(climbDirection);
    }

    private void StopClimbing()
    {
        Vector2 noVelocity = Vector2.zero;
        rigidbody.linearVelocity = noVelocity;

        OnClimb?.Invoke(noVelocity);
    }
}
