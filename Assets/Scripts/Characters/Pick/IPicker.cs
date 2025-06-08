using System;

public interface IPicker
{
    void Pick(IPickable iPickable);
    event Action<IPickable> OnPicked;
}
