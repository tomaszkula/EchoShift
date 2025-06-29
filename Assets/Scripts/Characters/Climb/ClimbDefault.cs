using System;
using System.Collections.Generic;
using UnityEngine;

public class ClimbDefault : MonoBehaviour, IClimb
{
    [Header("Settings")]
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private float applyClimbingSpeed = 10f;

    public bool IsClimbing { get; private set; }
    private List<Ladder> ladders = new List<Ladder>();
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
        if (iTriggerable is not Ladder ladder)
            return;

        ladders.Add(ladder);

        RefreshClimbingMovement();
    }

    private void OnUntriggered(ITriggerable iTriggerable)
    {
        if (iTriggerable is not Ladder ladder)
            return;

        ladders.Remove(ladder);

        RefreshClimbingMovement();
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
        rigidbody.linearVelocity = Vector2.MoveTowards(rigidbody.linearVelocity, climbVelocity, applyClimbingSpeed * Time.fixedDeltaTime);

        OnClimb?.Invoke(climbDirection);
    }

    private void RefreshClimbingMovement()
    {
        if (ladders.Count > 0)
        {
            IsClimbing = true;
            rigidbody.gravityScale = 0f;
        }
        else
        {
            IsClimbing = false;
            rigidbody.gravityScale = 1f;
            StopClimbing();
        }
    }

    private void StopClimbing()
    {
        //Vector2 noVelocity = Vector2.zero;
        //rigidbody.linearVelocity = noVelocity;

        //OnClimb?.Invoke(noVelocity);

        OnClimb?.Invoke(rigidbody.linearVelocity);
    }
}
