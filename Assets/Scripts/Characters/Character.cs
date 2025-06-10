using UnityEngine;

public class Character : MonoBehaviour
{
    public IMove IMove { get; private set; }
    public IJump IJump { get; private set; }
    public IShoot IShoot { get; private set; }
    public IClimb IClimb { get; private set; }
    public IActivator IActivator { get; private set; }

    public IFace IFace { get; private set; }

    private void Awake()
    {
        IMove = GetComponent<IMove>();
        IJump = GetComponent<IJump>();
        IShoot = GetComponent<IShoot>();
        IClimb = GetComponent<IClimb>();
        IActivator = GetComponent<IActivator>();

        IFace = GetComponent<IFace>();
    }
}