using System;

public interface IActivator
{
    void Activate();
    event Action OnActivate;
}
