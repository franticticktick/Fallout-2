using UnityEngine;

public class WeaponObject : MonoBehaviour
{

    [SerializeField]
    private MuzzleEffectFlash muzzleEffectFlash;

    [SerializeField]
    private ShootEffect shootEffect;

    public MuzzleEffectFlash MuzzleEffectFlash { get => muzzleEffectFlash; }
    public ShootEffect ShootEffect { get => shootEffect; }
}
