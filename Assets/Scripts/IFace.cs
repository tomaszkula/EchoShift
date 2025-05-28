using System;

public interface IFace
{
    Direction FaceDirection { get; }
    event Action<Direction> OnFaceDirectionChanged;
}
