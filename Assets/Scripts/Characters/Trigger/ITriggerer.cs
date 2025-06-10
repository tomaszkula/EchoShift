using System;

public interface ITriggerer
{
    void Trigger(ITriggerable iTriggerable);
    void Untrigger(ITriggerable iTriggerable);
    event Action<ITriggerable> OnTriggered;
    event Action<ITriggerable> OnUntriggered;
}
