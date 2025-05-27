using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EchoData
{
    public Vector3 recordPosition = Vector3.zero;
    public float recordStartTime = 0f;
    public float recordStopTime = 0f;
    public float playStartTime = 0f;
    public List<EchoFrameData> frames = new List<EchoFrameData>();
}

[Serializable]
public class EchoFrameData
{
    public float time = 0f;
    public Vector2 moveDirection = Vector2.zero;
    public bool isJumping = false;
}
