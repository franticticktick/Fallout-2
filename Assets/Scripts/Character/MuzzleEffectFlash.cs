using UnityEngine;

public class MuzzleEffectFlash : MonoBehaviour
{
    [SerializeField]
    private float delay;

    public float Delay { get => delay; }

    private void Start()
    {
        GetComponent<ParticleSystem>().Stop();
    }
}
