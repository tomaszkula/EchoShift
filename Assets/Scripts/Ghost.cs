using Game;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public EchoData echoData { get; private set; } = new EchoData();
    public EchoFrameData currentFrame { get; private set; } = null;

    private void Update()
    {
        if(echoData?.frames?.Count < 1)
        {
            return;
        }

        float offset = echoData.playTime - echoData.recordTime;
        if(echoData.frames[0].time < GameManager.Instance.GameTime - offset)
        {
            currentFrame = echoData.frames[0];
            echoData.frames.RemoveAt(0);
        }
    }

    public void Play(EchoData echoData)
    {
        this.echoData = echoData;
    }
}
