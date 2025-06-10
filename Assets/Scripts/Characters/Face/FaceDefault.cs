using System;
using UnityEngine;

public class FaceDefault : MonoBehaviour, IFace
{
    [Header("Settings")]
    [SerializeField] private Direction defaultDirection = Direction.Right;

    private IMove iMove = null;
    private IClimb iClimb = null;

    public Direction FaceDirection { get; set; } = Direction.Right;

    public event Action<Direction> OnFaceDirectionChanged = null;

    private void Awake()
    {
        iMove = GetComponent<IMove>();
        iClimb = GetComponent<IClimb>();

        FaceDirection = defaultDirection;
    }

    private void Start()
    {
        OnFaceDirectionChanged?.Invoke(FaceDirection);
    }

    private void OnEnable()
    {
        if (iMove != null)
            iMove.OnMove += OnMove;

        if (iClimb != null)
            iClimb.OnClimb += OnClimb;
    }

    private void OnDisable()
    {
        if (iMove != null)
            iMove.OnMove -= OnMove;

        if (iClimb != null)
            iClimb.OnClimb -= OnClimb;
    }

    private void OnMove(Vector2 direction)
    {
        RefreshFace(direction);
    }

    private void OnClimb(Vector2 direction)
    {
        RefreshFace(direction);
    }

    private void RefreshFace(Vector2 direction)
    {
        if (direction.x > 0)
        {
            FaceDirection = Direction.Right;
            OnFaceDirectionChanged?.Invoke(FaceDirection);
        }
        else if (direction.x < 0)
        {
            FaceDirection = Direction.Left;
            OnFaceDirectionChanged?.Invoke(FaceDirection);
        }
    }
}
