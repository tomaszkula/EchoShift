using System;

public interface IFace
{
    Direction FaceDirection { get; set; }
    event Action<Direction> OnFaceDirectionChanged;
}
