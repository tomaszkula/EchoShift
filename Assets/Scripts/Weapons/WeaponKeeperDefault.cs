using System;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponKeeperDefault : MonoBehaviour, IWeaponKeeper
{
    [Header("Settings")]
    [SerializeField] private WeaponData defaultWeaponData = null;

    private WeaponData weaponData = null;
    public WeaponData WeaponData
    {
        get => weaponData;
        private set
        {
            weaponData = value;
            OnWeaponChanged?.Invoke(weaponData);
        }
    }

    public Vector3 BarrelPosition => WeaponInHand != null ? WeaponInHand.transform.TransformPoint(WeaponData.WihMufflePosition) : Vector3.zero;

    public GameObject WeaponInHand { get; private set; }

    private IHand iHand = null;

    public event Action<WeaponData> OnWeaponChanged = null;

    private void Awake()
    {
        iHand = GetComponent<IHand>();
    }

    private void OnEnable()
    {
        if (iHand != null)
            iHand.OnHandChanged += OnHandChanged;
    }

    private void Start()
    {
        WeaponData = defaultWeaponData;
    }

    private void OnDrawGizmos()
    {
        if (WeaponData == null || WeaponInHand == null)
            return;

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(WeaponInHand.transform.TransformPoint(WeaponData.WihMufflePosition), WeaponData.WihMuffleGizmoRadius);
    }

    private void OnDisable()
    {
        if (iHand != null)
            iHand.OnHandChanged -= OnHandChanged;
    }

    private void OnHandChanged(Transform hand)
    {
        if (WeaponData == null || WeaponInHand == null)
            return;

        WeaponInHand.transform.SetParent(hand);
        WeaponInHand.transform.localPosition = WeaponData.WihPivot;
        WeaponInHand.transform.localRotation = Quaternion.identity;
    }

    public void SetWeapon(WeaponData weapon)
    {
        if(WeaponData != null)
        {
            if (WeaponInHand != null)
            {
                ObjectsPoolType weaponInHandOPT = WeaponData.WihOPT;
                ObjectPool<GameObject> weaponInHandOP = Manager.Instance.GetManager<ObjectsPoolsManager>().GetPool(weaponInHandOPT);
                weaponInHandOP.Release(WeaponInHand);
            }
        }

        WeaponData = weapon;

        if (WeaponData != null)
        {
            ObjectsPoolType weaponInHandOPT = WeaponData.WihOPT;
            ObjectPool<GameObject> weaponInHandOP = Manager.Instance.GetManager<ObjectsPoolsManager>().GetPool(weaponInHandOPT);
            WeaponInHand = weaponInHandOP.Get();
            WeaponInHand.transform.SetParent(iHand.Hand);
            WeaponInHand.transform.localPosition = WeaponData.WihPivot;
            WeaponInHand.transform.localRotation = Quaternion.identity;
        }
    }
}
