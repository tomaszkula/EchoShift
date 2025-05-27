using Game;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public EchoFrameData currentFrame { get; private set; } = null;

    public void SetEchoFrameData(EchoFrameData data)
    {
        currentFrame = data;
    }
}
