using Game;
using System;
using UnityEngine;

public class FaceDefault : MonoBehaviour, IFace
{
    [Header("Settings")]
    [SerializeField] private Direction defaultDirection = Direction.Right;

    private IOnMove onMove = null;

    public Direction FaceDirection { get; private set; } = Direction.Right;

    public event Action<Direction> OnFaceDirectionChanged = null;

    private void Awake()
    {
        onMove = GetComponent<IOnMove>();
    }

    private void Start()
    {
        FaceDirection = defaultDirection;
        OnFaceDirectionChanged?.Invoke(FaceDirection);
    }

    private void OnEnable()
    {
        if (onMove != null)
        {
            onMove.OnMove += OnMove;
        }
    }

    private void OnDisable()
    {
        if (onMove != null)
        {
            onMove.OnMove -= OnMove;
        }
    }

    private void OnMove(Vector2 moveDirection)
    {
        if (moveDirection.x > 0)
        {
            FaceDirection = Direction.Right;
            OnFaceDirectionChanged?.Invoke(FaceDirection);
        }
        else if (moveDirection.x < 0)
        {
            FaceDirection = Direction.Left;
            OnFaceDirectionChanged?.Invoke(FaceDirection);
        }
    }
}
