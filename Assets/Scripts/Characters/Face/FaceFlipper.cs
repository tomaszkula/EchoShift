using UnityEngine;

public class FaceFlipper : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool flipOnRight = false;

    [Header("References")]
    [SerializeField] private SpriteRenderer characterSR = null;

    private IFace iFace = null;

    private void Awake()
    {
        iFace = GetComponent<IFace>();
    }

    private void OnEnable()
    {
        if (iFace != null)
        {
            iFace.OnFaceDirectionChanged += OnFaceDirectionChanged;
        }
    }

    private void OnDisable()
    {
        if (iFace != null)
        {
            iFace.OnFaceDirectionChanged -= OnFaceDirectionChanged;
        }
    }

    private void OnFaceDirectionChanged(Direction direction)
    {
        characterSR.flipX = direction switch
        {
            Direction.Right => flipOnRight,
            Direction.Left => !flipOnRight,
            _ => characterSR.flipX
        };
    }
}
