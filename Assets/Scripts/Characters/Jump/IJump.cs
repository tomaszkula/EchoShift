using System;

public interface IJump
{
    void Jump();
    event Action OnJump;
}