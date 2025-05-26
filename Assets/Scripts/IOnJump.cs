using System;

namespace Game
{
    public interface IOnJump
    {
        Action OnJump { get; set; }
    }
}