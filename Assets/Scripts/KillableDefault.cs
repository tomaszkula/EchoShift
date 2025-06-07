using UnityEngine;

public class KillableDefault : MonoBehaviour, IKillable
{
    private IPooledObject iPooledObject = null;

    private void Awake()
    {
        iPooledObject = GetComponent<IPooledObject>();
    }

    public void Kill()
    {
        if (iPooledObject != null)
        {
            iPooledObject.Pool.Release(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
