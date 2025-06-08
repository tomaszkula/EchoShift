using System;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public EchoFrameData currentFrame { get; private set; } = null;

    public event Action<EchoFrameData> onFrameUpdated = null;

    public void SetEchoFrameData(EchoFrameData data)
    {
        currentFrame = data;

        onFrameUpdated?.Invoke(currentFrame);
    }
}
